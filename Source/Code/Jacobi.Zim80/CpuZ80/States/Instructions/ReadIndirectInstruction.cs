namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal abstract class ReadIndirectInstruction : ReadParametersInstruction
    {
        public ReadIndirectInstruction(CpuZ80 cpu)
            : base(cpu)
        { }

        protected ushort GetAddress()
        {
            var z = ExecutionEngine.Opcode.Definition.Z;
            var y = ExecutionEngine.Opcode.Definition.Y;
            var x = ExecutionEngine.Opcode.Definition.X;
            var q = ExecutionEngine.Opcode.Definition.Q;
            var p = ExecutionEngine.Opcode.Definition.P;

            // TODO: check z -for other instructions
            if (x == 0 && z == 2)
            {
                switch (p)
                {
                    case 0:
                        return Registers.BC;
                    case 1:
                        return Registers.DE;
                    default:
                        throw Errors.AssignedToIllegalOpcode();
                }
            }

            var isCB = ExecutionEngine.Opcode.Definition.Ext1 == 0xCB ||
                       ExecutionEngine.Opcode.Definition.Ext2 == 0xCB;

            // See OpcodeDefinition.Definitions
            if ( ((y == 6 && (z == 4 || z == 5 || z == 6) || x == 1)) ||
                 ((x == 1 || x == 2) && z == 6) ||
                 (isCB && (x == 0 && z == 6))
                 )
            {
                return GetHLOrIXIY();
            }

            throw Errors.AssignedToIllegalOpcode();
        }
    }
}
