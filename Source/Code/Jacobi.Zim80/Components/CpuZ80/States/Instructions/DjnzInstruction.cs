namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class DjnzInstruction : JumpRelativeInstruction
    {
        public DjnzInstruction(Die die)
            : base(die)
        { }

        protected override void ProcessLastCycleOfM1()
        {
            // NOTE: docs say no flags are affected...
            Registers.PrimarySet.B =
                Die.Alu.Dec8(Registers.PrimarySet.B);

            base.ProcessLastCycleOfM1();
        }

        protected override bool IsConditionMet()
        {
            return !Registers.PrimarySet.Flags.Z;
        }
    }
}
