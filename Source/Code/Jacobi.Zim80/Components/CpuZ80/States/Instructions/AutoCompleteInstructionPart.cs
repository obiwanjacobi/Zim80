namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class AutoCompleteInstructionPart : MachineCycleState
    {
        public AutoCompleteInstructionPart(ExecutionEngine executionEngine, MachineCycleNames activeMachineCycle) 
            : base(executionEngine, activeMachineCycle)
        { }

        protected override void OnClockNeg()
        {
            if (IsActive &&
                ExecutionEngine.Cycles.IsLastCycle)
            {
                IsComplete = true;
            }
        }
    }
}
