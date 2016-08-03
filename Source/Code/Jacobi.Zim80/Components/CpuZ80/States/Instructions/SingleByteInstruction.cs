namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal abstract class SingleByteInstruction : Instruction
    {
        protected SingleByteInstruction(ExecutionEngine executionEngine)
            : base(executionEngine)
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
