namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class NegateInstruction : SingleCycleInstruction
    {
        public NegateInstruction(Die die) 
            : base(die)
        { }

        protected override void OnLastCycleFirstM()
        {
            Registers.A = Die.Alu.Negate(Registers.A);
        }
    }
}
