namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class SetInstruction : SingleCycleInstruction
    {
        public SetInstruction(Die die) 
            : base(die)
        { }

        protected override void OnLastCycleFirstM()
        {
            var bit = ExecutionEngine.Opcode.Definition.Y;
            var reg = ExecutionEngine.Opcode.Definition.Register8FromZ;

            Registers[reg] = Die.Alu.SetBit(bit, Registers[reg]);
        }
    }
}
