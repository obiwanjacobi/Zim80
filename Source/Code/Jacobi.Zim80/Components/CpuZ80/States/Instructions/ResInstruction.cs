namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class ResInstruction : SingleCycleInstruction
    {
        public ResInstruction(Die die) 
            : base(die)
        { }

        protected override void OnLastCycleFirstM()
        {
            var bit = ExecutionEngine.Opcode.Definition.Y;
            var reg = ExecutionEngine.Opcode.Definition.Register8FromZ;

            Registers[reg] = Die.Alu.ResetBit(bit, Registers[reg]);
        }
    }
}
