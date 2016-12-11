namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class MathRegister8Instruction : SingleCycleInstruction
    {
        public MathRegister8Instruction(CpuZ80 cpu) 
            : base(cpu)
        { }

        protected override void OnLastCycleFirstM()
        {
            var reg = ExecutionEngine.Opcode.Definition.Register8FromZ;
            Cpu.Alu.DoAccumulatorMath(ExecutionEngine.Opcode.Definition.MathOperationFromY, Registers[reg]);
        }
    }
}
