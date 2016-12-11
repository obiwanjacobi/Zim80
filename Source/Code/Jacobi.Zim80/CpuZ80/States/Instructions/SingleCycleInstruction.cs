namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal abstract class SingleCycleInstruction : Instruction
    {
        protected SingleCycleInstruction(CpuZ80 cpu)
            : base(cpu)
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
