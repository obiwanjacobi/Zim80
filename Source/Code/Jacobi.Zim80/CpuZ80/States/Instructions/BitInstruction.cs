namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class BitInstruction : SingleCycleInstruction
    {
        public BitInstruction(Die die) 
            : base(die)
        { }

        protected override void OnLastCycleFirstM()
        {
            var bit = ExecutionEngine.Opcode.Definition.Y;
            var reg = ExecutionEngine.Opcode.Definition.Register8FromZ;

            var regValue = Registers[reg];

            Die.Alu.TestBit(bit, regValue);
        }
    }
}
