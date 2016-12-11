namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class MathDirectInstruction : ReadParametersInstruction
    {
        public MathDirectInstruction(CpuZ80 cpu) 
            : base(cpu)
        { }

        protected override void OnLastCycleLastM()
        {
            base.OnLastCycleLastM();

            var mathOp = ExecutionEngine.Opcode.Definition.MathOperationFromY;
            Cpu.Alu.DoAccumulatorMath(mathOp, InstructionM2.Data.Value);
        }
    }
}
