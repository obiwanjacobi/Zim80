using System;

namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal abstract class IndirectRegisterInstruction : MultiByteInstruction
    {
        public IndirectRegisterInstruction(ExecutionEngine executionEngine) 
            : base(executionEngine)
        { }

        protected UInt16 GetAddress()
        {
            var z = ExecutionEngine.Opcode.Definition.Z;
            var q = ExecutionEngine.Opcode.Definition.Q;
            var p = ExecutionEngine.Opcode.Definition.P;

            // TODO: check z -for other instructions

            switch (p)
            {
                case 0:
                    return ExecutionEngine.Die.Registers.PrimarySet.BC;
                case 1:
                    return ExecutionEngine.Die.Registers.PrimarySet.DE;
            }

            throw new InvalidOperationException();
        }
    }
}
