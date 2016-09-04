namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal abstract class MultiCycleInstruction : Instruction
    {
        private CpuState _currentPart;

        public MultiCycleInstruction(Die die)
            : base(die)
        { }

        protected abstract CpuState GetInstructionPart(MachineCycleNames machineCycle);

        public override void OnClock(DigitalLevel level)
        {
            if (_currentPart != null)
                _currentPart.OnClock(level);

            base.OnClock(level);
        }

        protected override void OnClockNeg()
        {
            base.OnClockNeg();

            if (_currentPart != null &&
                _currentPart.IsComplete)
            {
                OnInstructionPartCompleted(_currentPart);
            }

            if (ExecutionEngine.Cycles.IsLastCycle)
            {
                if (ExecutionEngine.Cycles.IsMachineCycle1)
                {
                    OnLastCycleFirstM();
                    SetNextInstructionPart();
                }
                else if (ExecutionEngine.Cycles.IsLastMachineCycle)
                {
                    OnLastCycleLastM();
                    IsComplete = true;
                }
                else if (_currentPart.IsComplete)
                {
                    SetNextInstructionPart();
                }
            }
            else if (ExecutionEngine.Cycles.IsLastMachineCycle &&
                     ExecutionEngine.Cycles.CycleName == CycleNames.T1)
            {
                OnFirstCycleLastM();
            }
        }

        protected virtual void OnInstructionPartCompleted(CpuState completedPart)
        {
            if (completedPart == null ||
                !completedPart.IsComplete)
            {
                throw Errors.InstructionPartWasNotCompleted();
            }
        }

        protected override void OnLastCycleFirstM()
        { }

        protected virtual void OnFirstCycleLastM()
        { }

        protected virtual void OnLastCycleLastM()
        { }

        private void SetNextInstructionPart()
        {
            _currentPart = GetInstructionPart(ExecutionEngine.Cycles.MachineCycle + 1);

            if (_currentPart == null)
                throw Errors.NextInstructionPartIsNull();
        }
    }
}
