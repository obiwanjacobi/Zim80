namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class DjnzInstruction : JumpRelativeInstruction
    {
        public DjnzInstruction(CpuZ80 cpu)
            : base(cpu)
        { }

        protected override void OnLastCycleFirstM()
        {
            // NOTE: docs say no flags are affected...
            Registers.B =
                Cpu.Alu.Dec8(Registers.B);

            base.OnLastCycleFirstM();
        }

        protected override bool IsConditionMet()
        {
            return !Registers.Flags.Z;
        }
    }
}
