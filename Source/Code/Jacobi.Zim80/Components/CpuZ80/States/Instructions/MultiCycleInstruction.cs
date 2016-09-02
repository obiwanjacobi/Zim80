using System;

namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal abstract class MultiCycleInstruction : Instruction
    {
        private CpuState _currentPart;

        public MultiCycleInstruction(Die die)
            : base(die)
        {
            SetNextInstructionPart();
        }

        protected abstract CpuState GetInstructionPart(MachineCycleNames machineCycle);

        public override void OnClock(DigitalLevel level)
        {
            _currentPart.OnClock(level);
            base.OnClock(level);
        }

        protected override void OnClockNeg()
        {
            base.OnClockNeg();

            if (_currentPart.IsComplete)
            {
                OnInstructionPartCompleted(_currentPart);
            }

            if (ExecutionEngine.Cycles.IsLastCycle)
            {
                if (ExecutionEngine.Cycles.IsMachineCycle1)
                {
                    OnLastCycleFirstM();
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
                throw new InvalidOperationException("InstructionPart was not completed!");
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
                throw new InvalidOperationException("GetInstructionPart returned null.");
        }
    }
}
