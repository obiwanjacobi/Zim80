namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class MathDirectInstruction : ReadParametersInstruction
    {
        public MathDirectInstruction(Die die) 
            : base(die)
        { }

        protected override void OnLastCycleLastM()
        {
            base.OnLastCycleLastM();

            var mathOp = ExecutionEngine.Opcode.Definition.MathOperationFromY;
            Die.Alu.DoAccumulatorMath(mathOp, InstructionM2.Data.Value);
        }
    }
}
