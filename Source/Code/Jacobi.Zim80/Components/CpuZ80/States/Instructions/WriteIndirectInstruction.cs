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

            throw Errors.AssignedToIllegalOpcode();
        }
    }
}
