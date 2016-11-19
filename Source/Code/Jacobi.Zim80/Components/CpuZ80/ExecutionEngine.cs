using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using Jacobi.Zim80.Components.CpuZ80.States;
using System;

namespace Jacobi.Zim80.Components.CpuZ80
{
    internal class ExecutionEngine
    {
        private readonly Die _die;
        private readonly CycleCounter _cycles = new CycleCounter();
        private readonly OpcodeBuilder _opcodeBuilder = new OpcodeBuilder();
        private readonly InterruptManager _interruptManager;
        private CpuState _state;
        private CpuStates _currentState;

        public ExecutionEngine(Die die)
        {
            _die = die;
            _interruptManager = new InterruptManager(_die);

            StartFetch();

            // the engine that drives it all
            _die.Clock.OnChanged += Clock_OnChanged;
        }

        public event EventHandler<InstructionExecutedEventArgs> InstructionExecuted;

        public Die Die { get { return _die; } }

        public InterruptManager InterruptManager { get { return _interruptManager; } }

        public CycleCounter Cycles { get { return _cycles; } }

        public bool AddOpcodeByte(OpcodeByte opcodeByte)
        {
            var valid = _opcodeBuilder.Add(opcodeByte);

            if (valid)
                _cycles.OpcodeDefinition = _opcodeBuilder.Opcode.Definition;

            return valid;
        }

        public bool IsOpcodeComplete
        {
            get { return _opcodeBuilder.IsDone; }
        }

        public Opcode Opcode
        {
            get { return _opcodeBuilder.Opcode; }
        }

        public SingleByteOpcode SingleCycleOpcode
        {
            get { return _opcodeBuilder.Opcode as SingleByteOpcode; }
        }

        public MultiByteOpcode MultiCycleOpcode
        {
            get { return _opcodeBuilder.Opcode as MultiByteOpcode; }
        }

        public void SetPcOnAddressBus()
        {
            SetAddressBus(_die.Registers.PC);
            _die.Registers.PC++;
        }

        public void SetRefreshOnAddressBus()
        {
            SetAddressBus(_die.Registers.IR);

            _die.Registers.IncrementR();
        }

        public void SetAddressBus(UInt16 address)
        {
            _die.AddressBus.Write(new BusData16(address));
        }

        private void Clock_OnChanged(object sender, DigitalLevelChangedEventArgs e)
        {
            _cycles.OnClock(e.Level);
            _interruptManager.OnClock(e.Level);
            _state.OnClock(e.Level);

            if (_state.IsComplete)
            {
                SwitchToNextState();
            }
        }

        internal void NotifyInstructionExecuted()
        {
            InstructionExecuted?.Invoke(this, new InstructionExecutedEventArgs(Opcode));
        }

        private void SwitchToNextState()
        {
            switch (_currentState)
            {
                case CpuStates.Fetch:
                    if (!ContinueFetch())
                        StartExecute();
                    break;
                case CpuStates.Execute:
                    if (!AcceptInterrupt())
                        StartFetch();
                    break;
                case CpuStates.Interrupt:
                    StartFetch();
                    break;
            }
        }

        private void StartFetch()
        {
            _state = new CpuFetch(_die);
            _currentState = CpuStates.Fetch;
            _opcodeBuilder.Clear();
            _cycles.Reset();
        }

        private bool ContinueFetch()
        {
            if (_cycles.IsLastCycle &&
                _cycles.OpcodeDefinition == null)
            {
                if (_opcodeBuilder.HasReversedOffsetParameterOrder)
                {
                    _state = new CpuReadParameterThenExecute(_die);
                    _currentState = CpuStates.Execute;
                    _cycles.Continue(3);
                }
                else
                {
                    _state = new CpuFetch(_die);
                    _cycles.Continue();
                }
                return true;
            }

            return false;
        }

        private void StartExecute()
        {
            _state = new CpuExecute(_die);
            _currentState = CpuStates.Execute;
        }

        private bool AcceptInterrupt()
        {
            var interrupt = _interruptManager.PopInterrupt();
            if (interrupt != null)
            {
                _state = interrupt;
                _currentState = CpuStates.Interrupt;

                // prep engine state
                _opcodeBuilder.Clear();
                _cycles.Reset();
                _cycles.OpcodeDefinition = interrupt.Definition;
                return true;
            }

            return false;
        }

        internal enum CpuStates
        {
            Fetch,
            Execute,
            Interrupt,
            Wait,
            Halt,
            BusRequest,
            Reset,
        }
    }
}
