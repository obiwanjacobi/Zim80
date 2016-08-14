using System;

namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal abstract class IndirectRegisterInstruction : MultiByteInstruction
    {
        public IndirectRegisterInstruction(Die die)
            : base(die)
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
                    return Registers.PrimarySet.BC;
                case 1:
                    return Registers.PrimarySet.DE;
            }

            throw new InvalidOperationException();
        }
    }
}
