namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class NegateInstruction : SingleCycleInstruction
    {
        public NegateInstruction(CpuZ80 cpu) 
            : base(cpu)
        { }

        protected override void OnLastCycleFirstM()
        {
            Registers.A = Cpu.Alu.Negate(Registers.A);
        }
    }
}
