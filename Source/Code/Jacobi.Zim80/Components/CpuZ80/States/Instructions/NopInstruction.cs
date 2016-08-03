using System;

namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class NopInstruction : SingleByteInstruction
    {
        public NopInstruction(ExecutionEngine executionEngine)
            : base(executionEngine)
        { }

        protected override void OnExecute()
        {
            // no operation
        }
    }
}
