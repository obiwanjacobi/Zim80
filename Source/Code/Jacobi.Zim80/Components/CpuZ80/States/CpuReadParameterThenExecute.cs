using Jacobi.Zim80.Components.CpuZ80.States.Instructions;

namespace Jacobi.Zim80.Components.CpuZ80.States
{
    // bytes: [ext1], CB, [d], [opcode] are all fetched in M1
    internal class CpuReadParameterThenExecute : CpuExecute
    {
        private CpuState _currentPart;
        private bool _opcodeWasRead;

        public CpuReadParameterThenExecute(Die die)
            : base(die, createInstruction: false)
        {
            // turn off refresh logic - not a true M1 cycle.
            RefreshEnabled = false;

            // read d-offset that comes before the opcode
            _currentPart = new ReadT3InstructionPart(Die, MachineCycleNames.M1);
        }

        public override void OnClock(DigitalLevel level)
        {
            if (_currentPart != null)
                _currentPart.OnClock(level);

            base.OnClock(level);
        }

        protected override void OnClockPos()
        {
            base.OnClockPos();

            if (ExecutionEngine.Cycles.CycleName == CycleNames.T4 &&
                ExecutionEngine.Cycles.MachineCycle == MachineCycleNames.M1 &&
                _opcodeWasRead)
            {
                // CpuExecute takes over from here
                CreateInstruction();
            }
        }

        protected override void OnClockNeg()
        {
            base.OnClockNeg();

            if (ExecutionEngine.Cycles.CycleName == CycleNames.T3 &&
                ExecutionEngine.Cycles.MachineCycle == MachineCycleNames.M1)
            {
                if (!_opcodeWasRead)
                {
                    // read instruction opcode
                    _currentPart = new ReadT3InstructionPart(Die, MachineCycleNames.M1);
                    ExecutionEngine.Cycles.Continue(5);
                    _opcodeWasRead = true;
                }
                else
                {
                    // done reading opcode
                    _currentPart = null;
                }
            }
        }
    }
}
