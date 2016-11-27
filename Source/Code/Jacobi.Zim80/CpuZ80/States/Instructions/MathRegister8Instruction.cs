namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class MathRegister8Instruction : SingleCycleInstruction
    {
        public MathRegister8Instruction(Die die) 
            : base(die)
        { }

        protected override void OnLastCycleFirstM()
        {
            var reg = ExecutionEngine.Opcode.Definition.Register8FromZ;
            Die.Alu.DoAccumulatorMath(ExecutionEngine.Opcode.Definition.MathOperationFromY, Registers[reg]);
        }
    }
}
