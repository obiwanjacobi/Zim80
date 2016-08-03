using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class WriteIndirectRegisterInstruction : SingleByteInstruction
    {
        public WriteIndirectRegisterInstruction(ExecutionEngine executionEngine) 
            : base(executionEngine)
        { }

        protected override void OnExecute()
        {
            throw new NotImplementedException();
        }
    }
}
