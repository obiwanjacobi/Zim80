namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class SetInstruction : SingleCycleInstruction
    {
        public SetInstruction(CpuZ80 cpu) 
            : base(cpu)
        { }

        protected override void OnLastCycleFirstM()
        {
            var bit = ExecutionEngine.Opcode.Definition.Y;
            var reg = ExecutionEngine.Opcode.Definition.Register8FromZ;

            Registers[reg] = Cpu.Alu.SetBit(bit, Registers[reg]);
        }
    }
}
