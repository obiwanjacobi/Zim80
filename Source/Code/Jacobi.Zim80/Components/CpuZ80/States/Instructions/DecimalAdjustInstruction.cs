namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class DecimalAdjustInstruction : SingleCycleInstruction
    {
        public DecimalAdjustInstruction(Die die) 
            : base(die)
        { }

        protected override void OnLastCycleFirstM()
        {
            switch (ExecutionEngine.Opcode.Definition.Y)
            {
                case 4: //daa
                    DecimalAdjustAccumulator();
                    break;
                case 5: //cpl
                    ComplementAccumulator();
                    break;
                default:
                    throw Errors.AssignedToIllegalOpcode();
            }
        }

        private void DecimalAdjustAccumulator()
        {
            Registers.A = Die.Alu.DecimalAdjust(Registers.A);
        }

        private void ComplementAccumulator()
        {
            Registers.A = (byte)~Registers.A;
            Registers.Flags.H = true;
            Registers.Flags.N = true;
        }
    }
}
