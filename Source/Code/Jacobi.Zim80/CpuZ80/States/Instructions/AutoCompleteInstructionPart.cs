namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class AutoCompleteInstructionPart : MachineCycleState
    {
        private CycleNames? _lastCycle;

        public AutoCompleteInstructionPart(Die die, MachineCycleNames activeMachineCycle) 
            : base(die, activeMachineCycle)
        { }

        public AutoCompleteInstructionPart(Die die, MachineCycleNames activeMachineCycle, CycleNames lastCycle)
            : base(die, activeMachineCycle)
        {
            _lastCycle = lastCycle;
        }

        protected override void OnClockNeg()
        {
            if (IsActive && 
                ExecutionEngine.Cycles.IsLastCycle)
            {
                if (_lastCycle.HasValue &&
                    ExecutionEngine.Cycles.CycleName < _lastCycle.Value)
                {
                    throw Errors.CycleCountMismatch();
                }

                IsComplete = true;
            }
        }

        
    }
}
