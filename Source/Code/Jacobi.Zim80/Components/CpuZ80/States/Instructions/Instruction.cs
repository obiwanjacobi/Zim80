namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal abstract class Instruction : CpuState
    {
        protected Instruction(ExecutionEngine executionEngine) 
            : base(executionEngine)
        { }

        protected abstract void OnExecute();
    }
}
