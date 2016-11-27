namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class DjnzInstruction : JumpRelativeInstruction
    {
        public DjnzInstruction(Die die)
            : base(die)
        { }

        protected override void OnLastCycleFirstM()
        {
            // NOTE: docs say no flags are affected...
            Registers.B =
                Die.Alu.Dec8(Registers.B);

            base.OnLastCycleFirstM();
        }

        protected override bool IsConditionMet()
        {
            return !Registers.Flags.Z;
        }
    }
}
