namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class ExAFAFInstruction : SingleByteInstruction
    {
        public ExAFAFInstruction(ExecutionEngine executionEngine) 
            : base(executionEngine)
        { }

        protected override void OnExecute()
        {
            // swap AF/AF'
            var af = ExecutionEngine.Die.Registers.AlternateSet.AF;
            ExecutionEngine.Die.Registers.AlternateSet.AF = 
                ExecutionEngine.Die.Registers.PrimarySet.AF;
            ExecutionEngine.Die.Registers.PrimarySet.AF = af;
        }
    }
}