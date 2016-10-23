namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class CarryFlagInstruction : SingleCycleInstruction
    {
        public CarryFlagInstruction(Die die) 
            : base(die)
        { }

        protected override void OnLastCycleFirstM()
        {
            switch (ExecutionEngine.Opcode.Definition.Y)
            {
                case 6:
                    SetCarryFlag();
                    break;
                case 7:
                    ComplementCarryFlag();
                    break;
                default:
                    throw Errors.AssignedToIllegalOpcode();
            }
        }

        private void ComplementCarryFlag()
        {
            Registers.PrimarySet.Flags.H = Registers.PrimarySet.Flags.C;
            Registers.PrimarySet.Flags.C = !Registers.PrimarySet.Flags.C;
            Registers.PrimarySet.Flags.N = false;
        }

        private void SetCarryFlag()
        {
            Registers.PrimarySet.Flags.C = true;
            Registers.PrimarySet.Flags.H = false;
            Registers.PrimarySet.Flags.N = false;
        }
    }
}
