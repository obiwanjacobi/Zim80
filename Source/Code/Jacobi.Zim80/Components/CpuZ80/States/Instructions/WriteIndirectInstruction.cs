namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal abstract class WriteIndirectInstruction : ReadParametersInstruction
    {
        public WriteIndirectInstruction(Die die)
            : base(die)
        { }

        protected ushort GetAddress()
        {
            var z = ExecutionEngine.Opcode.Definition.Z;
            var y = ExecutionEngine.Opcode.Definition.Y;
            var x = ExecutionEngine.Opcode.Definition.X;
            var p = ExecutionEngine.Opcode.Definition.P;

            if (x == 0 && z == 2)
            {
                switch (p)
                {
                    case 0:
                        return Registers.PrimarySet.BC;
                    case 1:
                        return Registers.PrimarySet.DE;
                }
            }

            if (x == 1 && y == 6)
            {
                return GetHLOrIXIY();
            }

            throw Errors.AssignedToIllegalOpcode();
        }

        protected byte GetValue()
        {
            var y = ExecutionEngine.Opcode.Definition.Y;
            var x = ExecutionEngine.Opcode.Definition.X;

            var value = Registers.PrimarySet.A;

            if (x == 1 && y == 6)
            {
                var reg = ExecutionEngine.Opcode.Definition.Register8FromZ;
                value = Registers.PrimarySet[reg];
            }

            return value;
        }
    }
}
