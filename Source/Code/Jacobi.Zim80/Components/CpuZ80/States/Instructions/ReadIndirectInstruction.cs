namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal abstract class ReadIndirectInstruction : ReadParametersInstruction
    {
        public ReadIndirectInstruction(Die die)
            : base(die)
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
                        return Registers.PrimarySet.BC;
                    case 1:
                        return Registers.PrimarySet.DE;
                    default:
                        throw Errors.AssignedToIllegalOpcode();
                }
            }

            // See OpcodeDefinition.Definitions
            if ( ((y == 6 && (z == 4 || z == 5 || z == 6) || x == 1)) ||
                 ((x == 1 || x == 2) && z == 6)
                 // CB: (x == 0 || x == 1 || x== 2 || x== 3) && z == 6
                 )
            {
                if (ExecutionEngine.Opcode.Definition.IsIX)
                {
                    if (ExecutionEngine.Opcode.Definition.d)
                        return Alu.Add(Registers.IX, (sbyte)ExecutionEngine.MultiCycleOpcode.GetParameter(0).Value);
                    return Registers.IX;
                }
                if (ExecutionEngine.Opcode.Definition.IsIY)
                {
                    if (ExecutionEngine.Opcode.Definition.d)
                        return Alu.Add(Registers.IY, (sbyte)ExecutionEngine.MultiCycleOpcode.GetParameter(0).Value);
                    return Registers.IY;
                }

                return Registers.PrimarySet.HL;
            }

            throw Errors.AssignedToIllegalOpcode();
        }
    }
}
