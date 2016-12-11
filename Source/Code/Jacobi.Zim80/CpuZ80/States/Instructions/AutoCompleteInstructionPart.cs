namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class AutoCompleteInstructionPart : MachineCycleState
    {
        private CycleNames? _lastCycle;

        public AutoCompleteInstructionPart(CpuZ80 cpu, MachineCycleNames activeMachineCycle) 
            : base(cpu, activeMachineCycle)
        { }

        public AutoCompleteInstructionPart(CpuZ80 cpu, MachineCycleNames activeMachineCycle, CycleNames lastCycle)
            : base(cpu, activeMachineCycle)
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
