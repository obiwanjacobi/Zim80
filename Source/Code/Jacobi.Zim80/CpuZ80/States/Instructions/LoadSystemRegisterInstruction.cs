namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class LoadSystemRegisterInstruction : SingleCycleInstruction
    {
        public LoadSystemRegisterInstruction(CpuZ80 cpu) 
            : base(cpu)
        { }

        protected override void OnLastCycleFirstM()
        {
            switch (ExecutionEngine.Opcode.Definition.Y)
            {
                case 0: //ld I, A
                    LoadIntAcc();
                    break;
                case 1: //ld R, A
                    LoadRefAcc();
                    break;
                case 2: //ld A, I
                    LoadAccInt();
                    break;
                case 3: //ld A, R
                    LoadAccRef();
                    break;
                default:
                    throw Errors.AssignedToIllegalOpcode();
            }
        }

        private void LoadIntAcc()
        {
            Registers.I = Registers.A;
        }

        private void LoadRefAcc()
        {
            Registers.R = Registers.A;
        }

        private void LoadAccInt()
        {
            Registers.A = Registers.I;
            SetFlags(Registers.I);
        }

        private void LoadAccRef()
        {
            Registers.A = Registers.R;
            SetFlags(Registers.R);
        }

        private void SetFlags(byte value)
        {
            Registers.Flags.S = Alu.IsNegative(value);
            Registers.Flags.Z = Alu.IsZero(value);
            Registers.Flags.H = false;
            Registers.Flags.PV = Registers.Interrupt.IFF2;
            Registers.Flags.N = false;
        }
    }
}
