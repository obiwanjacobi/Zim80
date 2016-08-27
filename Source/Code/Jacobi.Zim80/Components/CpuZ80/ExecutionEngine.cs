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
        private CpuState _state;
        private CpuStates _currentState;

        public ExecutionEngine(Die die)
        {
            _die = die;
            _state = new CpuFetch(_die);
            _currentState = CpuStates.Fetch;

            // the engine that drives it all
            _die.Clock.OnChanged += Clock_OnChanged;
        }

        public Die Die { get { return _die; } }

        public event EventHandler<InstructionExecutedEventArgs> InstructionExecuted;

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
            SetAddressBus(_die.Registers.R);
        }

        public void SetAddressBus(UInt16 address)
        {
            _die.AddressBus.Write(new BusData16(address));
        }

        private void Clock_OnChanged(object sender, DigitalLevelChangedEventArgs e)
        {
            _cycles.OnClock(e.Level);
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
                    _state = new CpuExecute(_die);
                    _currentState = CpuStates.Execute;
                    break;
                case CpuStates.Execute:
                    _state = new CpuFetch(_die);
                    _currentState = CpuStates.Fetch;
                    _opcodeBuilder.Clear();
                    _cycles.Reset();
                    break;
            }
        }

        internal enum CpuStates
        {
            Fetch,
            Execute
        }
    }
}
