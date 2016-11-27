namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class RstInstruction : PushInstruction
    {
        public RstInstruction(Die die) 
            : base(die)
        { }

        protected override void OnLastCycleFirstM()
        {
            Registers.PC = (ushort)(ExecutionEngine.Opcode.Definition.Y * 0x08);
        }

        protected override byte GetRegisterLowValue()
        {
            return Registers.GetPC().GetLo();
        }

        protected override byte GetRegisterHighValue()
        {
            return Registers.GetPC().GetHi();
        }
    }
}
