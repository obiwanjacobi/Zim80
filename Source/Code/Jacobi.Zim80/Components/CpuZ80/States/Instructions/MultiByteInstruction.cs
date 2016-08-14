using System;

namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal abstract class MultiByteInstruction : Instruction
    {
        private CpuState _currentPart;

        public MultiByteInstruction(Die die)
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

            if (ExecutionEngine.Cycles.IsLastCycle)
            {
                if (ExecutionEngine.Cycles.IsLastMachineCycle)
                {
                    OnExecute();
                    IsComplete = true;
                }
                else if (_currentPart.IsComplete)
                    SetNextInstructionPart();
            }
        }

        // for write operations
        protected void OnClockNegWrite()
        {
            base.OnClockNeg();

            if (ExecutionEngine.Cycles.IsLastMachineCycle &&
                ExecutionEngine.Cycles.CycleName == CycleNames.T1)
            {
                OnExecute();
            }

            if (ExecutionEngine.Cycles.IsLastCycle)
            {
                if (ExecutionEngine.Cycles.IsLastMachineCycle)
                {
                    IsComplete = true;
                }
                else if (_currentPart.IsComplete)
                    SetNextInstructionPart();
            }
        }

        private void SetNextInstructionPart()
        {
            _currentPart = GetInstructionPart(ExecutionEngine.Cycles.MachineCycle + 1);
        }

        // helper for derived classes that implement GetInstructionPart
        protected static void ThrowInvalidMachineCycle(MachineCycleNames machineCycle)
        {
            throw new InvalidOperationException("Invalid machine cycle: " + machineCycle);
        }
    }
}
