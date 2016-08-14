namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class Dec8Instruction : SingleByteInstruction
    {
        public Dec8Instruction(Die die)
            : base(die)
        { }

        protected override void OnExecute()
        {
            switch (ExecutionEngine.Opcode.Definition.Register8FromY)
            {
                case Opcodes.Register8Table.B:
                    Registers.PrimarySet.B =
                        Die.Alu.Dec8(Registers.PrimarySet.B);
                    break;
                case Opcodes.Register8Table.C:
                    Registers.PrimarySet.C =
                        Die.Alu.Dec8(Registers.PrimarySet.C);
                    break;
                case Opcodes.Register8Table.D:
                    Registers.PrimarySet.D =
                        Die.Alu.Dec8(Registers.PrimarySet.D);
                    break;
                case Opcodes.Register8Table.E:
                    Registers.PrimarySet.E =
                        Die.Alu.Dec8(Registers.PrimarySet.E);
                    break;
                case Opcodes.Register8Table.H:
                    Registers.PrimarySet.H =
                        Die.Alu.Dec8(Registers.PrimarySet.H);
                    break;
                case Opcodes.Register8Table.L:
                    Registers.PrimarySet.L =
                        Die.Alu.Dec8(Registers.PrimarySet.L);
                    break;
                case Opcodes.Register8Table.A:
                    Registers.PrimarySet.A =
                        Die.Alu.Dec8(Registers.PrimarySet.A);
                    break;
            }
        }
    }
}
