namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal abstract class SingleCycleInstruction : Instruction
    {
        protected SingleCycleInstruction(Die die)
            : base(die)
        { }

        protected override void OnClockNeg()
        {
            base.OnClockNeg();

            if (ExecutionEngine.Cycles.IsLastCycle)
            {
                OnLastCycleFirstM();
                IsComplete = true;
            }
        }
    }
}
