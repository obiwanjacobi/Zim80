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
            Registers.Flags.H = Registers.Flags.C;
            Registers.Flags.C = !Registers.Flags.C;
            Registers.Flags.N = false;
        }

        private void SetCarryFlag()
        {
            Registers.Flags.C = true;
            Registers.Flags.H = false;
            Registers.Flags.N = false;
        }
    }
}
