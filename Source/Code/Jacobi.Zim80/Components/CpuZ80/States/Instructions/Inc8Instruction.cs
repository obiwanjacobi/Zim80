namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class Inc8Instruction : SingleCycleInstruction
    {
        public Inc8Instruction(Die die)
            : base(die)
        { }

        protected override void OnLastCycleFirstM()
        {
            switch (ExecutionEngine.Opcode.Definition.Register8FromY)
            {
                case Opcodes.Register8Table.B:
                    Registers.PrimarySet.B =
                        Die.Alu.Inc8(Registers.PrimarySet.B);
                    break;
                case Opcodes.Register8Table.C:
                    Registers.PrimarySet.C =
                        Die.Alu.Inc8(Registers.PrimarySet.C);
                    break;
                case Opcodes.Register8Table.D:
                    Registers.PrimarySet.D =
                        Die.Alu.Inc8(Registers.PrimarySet.D);
                    break;
                case Opcodes.Register8Table.E:
                    Registers.PrimarySet.E =
                        Die.Alu.Inc8(Registers.PrimarySet.E);
                    break;
                case Opcodes.Register8Table.H:
                    if (!ExecuteShiftedInstructionHi())
                        Registers.PrimarySet.H =
                            Die.Alu.Inc8(Registers.PrimarySet.H);
                    break;
                case Opcodes.Register8Table.L:
                    if (!ExecuteShiftedInstructionLo())
                        Registers.PrimarySet.L =
                            Die.Alu.Inc8(Registers.PrimarySet.L);
                    break;
                case Opcodes.Register8Table.A:
                    Registers.PrimarySet.A =
                        Die.Alu.Inc8(Registers.PrimarySet.A);
                    break;
            }
        }

        private bool ExecuteShiftedInstructionHi()
        {
            if (ExecutionEngine.Opcode.IsIX)
            {
                Registers.GetIX().SetHi(
                        Die.Alu.Inc8(Registers.GetIX().GetHi()));
                return true;
            }
            if (ExecutionEngine.Opcode.IsIY)
            {
                Registers.GetIY().SetHi(
                        Die.Alu.Inc8(Registers.GetIY().GetHi()));
                return true;
            }

            return false;
        }

        private bool ExecuteShiftedInstructionLo()
        {
            if (ExecutionEngine.Opcode.IsIX)
            {
                Registers.GetIX().SetLo(
                        Die.Alu.Inc8(Registers.GetIX().GetLo()));
                return true;
            }
            if (ExecutionEngine.Opcode.IsIY)
            {
                Registers.GetIY().SetLo(
                        Die.Alu.Inc8(Registers.GetIY().GetLo()));
                return true;
            }

            return false;
        }
    }
}
