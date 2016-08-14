namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal abstract class SingleByteInstruction : Instruction
    {
        protected SingleByteInstruction(Die die)
            : base(die)
        { }

        protected override void OnClockPos()
        {
            base.OnClockPos();

            if (ExecutionEngine.Cycles.IsLastCycle)
            {
                OnExecute();
            }
        }

        protected override void OnClockNeg()
        {
            base.OnClockNeg();

            if (ExecutionEngine.Cycles.IsLastCycle)
            {
                IsComplete = true;
            }
        }
    }
}
