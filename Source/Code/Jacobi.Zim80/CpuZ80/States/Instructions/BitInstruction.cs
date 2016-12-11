namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class BitInstruction : SingleCycleInstruction
    {
        public BitInstruction(CpuZ80 cpu) 
            : base(cpu)
        { }

        protected override void OnLastCycleFirstM()
        {
            var bit = ExecutionEngine.Opcode.Definition.Y;
            var reg = ExecutionEngine.Opcode.Definition.Register8FromZ;

            var regValue = Registers[reg];

            Cpu.Alu.TestBit(bit, regValue);
        }
    }
}
