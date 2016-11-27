namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class HaltInstruction : Instruction
    {
        public HaltInstruction(Die die) 
            : base(die)
        { }

        protected override void OnClockNeg()
        {
            base.OnClockNeg();

            if (ExecutionEngine.Cycles.IsLastCycle)
                OnLastCycleFirstM();
        }

        protected override void OnLastCycleFirstM()
        {
            if (ExecutionEngine.InterruptManager.HasInterruptWaiting)
                IsComplete = true;
        }
    }
}
