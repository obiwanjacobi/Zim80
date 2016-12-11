namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class NopInstruction : SingleCycleInstruction
    {
        public NopInstruction(CpuZ80 cpu)
            : base(cpu)
        { }

        protected override void OnLastCycleFirstM()
        {
            // no operation
        }
    }
}
