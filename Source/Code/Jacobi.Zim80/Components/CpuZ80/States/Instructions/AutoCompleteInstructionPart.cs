namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class AutoCompleteInstructionPart : MachineCycleState
    {
        public AutoCompleteInstructionPart(Die die, MachineCycleNames activeMachineCycle) 
            : base(die, activeMachineCycle)
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
