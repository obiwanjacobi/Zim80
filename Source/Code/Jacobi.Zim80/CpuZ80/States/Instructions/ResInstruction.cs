namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class ResInstruction : SingleCycleInstruction
    {
        public ResInstruction(CpuZ80 cpu) 
            : base(cpu)
        { }

        protected override void OnLastCycleFirstM()
        {
            var bit = ExecutionEngine.Opcode.Definition.Y;
            var reg = ExecutionEngine.Opcode.Definition.Register8FromZ;

            Registers[reg] = Cpu.Alu.ResetBit(bit, Registers[reg]);
        }
    }
}
