using Jacobi.Zim80.CpuZ80.Opcodes;
using Jacobi.Zim80.CpuZ80.States;
using System;

namespace Jacobi.Zim80.CpuZ80
{
    internal class ExecutionEngine
    {
        private readonly CpuZ80 _cpu;
        private readonly CycleCounter _cycles = new CycleCounter();
        private readonly OpcodeBuilder _opcodeBuilder = new OpcodeBuilder();
        private readonly InterruptManager _interruptManager;
        private CpuState _state;
        private CpuStates _currentState;
        private ushort _instructionAddress;

        public ExecutionEngine(CpuZ80 cpu)
        {
            _cpu = cpu;
            _interruptManager = new InterruptManager(_cpu);

            StartFetch();

            // the engine that drives it all
            _cpu.Clock.OnChanged += Clock_OnChanged;
        }

        public event EventHandler<InstructionExecutedEventArgs> InstructionExecuted;

        public CpuZ80 Cpu { get { return _cpu; } }

        public InterruptManager InterruptManager { get { return _interruptManager; } }

        public CycleCounter Cycles { get { return _cycles; } }

        public bool AddOpcodeByte(OpcodeByte opcodeByte)
        {
            if (_opcodeBuilder.IsEmpty)
                _instructionAddress = (ushort)(Cpu.Registers.PC - 1);

            var valid = _opcodeBuilder.Add(opcodeByte);

            if (valid)
            {
                _opcodeBuilder.Opcode.Address = _instructionAddress;
                _cycles.OpcodeDefinition = _opcodeBuilder.Opcode.Definition;
            }

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
            SetAddressBus(_cpu.Registers.PC);
            _cpu.Registers.PC++;
        }

        public void SetRefreshOnAddressBus()
        {
            SetAddressBus(_cpu.Registers.IR);

            _cpu.Registers.IncrementR();
        }

        public void SetAddressBus(UInt16 address)
        {
            _cpu.Address.Write(new BusData16(address));
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
            InstructionExecuted?.Invoke(Cpu, new InstructionExecutedEventArgs(Opcode));
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
            _state = new CpuFetch(_cpu);
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
                    _state = new CpuReadParameterThenExecute(_cpu);
                    _currentState = CpuStates.Execute;
                    _cycles.Continue(3);
                }
                else
                {
                    _state = new CpuFetch(_cpu);
                    _cycles.Continue();
                }
                return true;
            }

            return false;
        }

        private void StartExecute()
        {
            _state = new CpuExecute(_cpu);
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
