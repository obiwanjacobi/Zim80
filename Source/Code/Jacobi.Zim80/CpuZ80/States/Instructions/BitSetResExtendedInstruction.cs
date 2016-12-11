namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class BitSetResExtendedInstruction : ExtendedInstruction
    {
        public BitSetResExtendedInstruction(CpuZ80 cpu) 
            : base(cpu)
        { }

        protected override void OnLastCycleLastM()
        {
            base.OnLastCycleLastM();

            if (ExecutionEngine.Opcode.Definition.X == 1)   //bit
            {
                var bit = ExecutionEngine.Opcode.Definition.Y;
                Cpu.Alu.TestBit(bit, GetValue());
            }
        }

        protected override byte ExecuteOperation(byte value)
        {
            var bit = ExecutionEngine.Opcode.Definition.Y;

            switch (ExecutionEngine.Opcode.Definition.X)
            {
                case 2: //res
                    return Cpu.Alu.ResetBit(bit, value);
                case 3: //set
                    return Cpu.Alu.SetBit(bit, value);
                default:
                    throw Errors.AssignedToIllegalOpcode();
            }
        }
    }
}
