namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class Dec8Instruction : SingleCycleInstruction
    {
        public Dec8Instruction(Die die)
            : base(die)
        { }

        protected override void OnLastCycleFirstM()
        {
            var register = ExecutionEngine.Opcode.Definition.Register8FromY;

            if (ExecutionEngine.Opcode.Definition.IsIX ||
                ExecutionEngine.Opcode.Definition.IsIY)
            {
                switch (register)
                {
                    case Opcodes.Register8Table.H:
                        ExecuteShiftedInstructionHi();
                        break;
                    case Opcodes.Register8Table.L:
                        ExecuteShiftedInstructionLo();
                        break;
                }
            }
            else
            {
                Registers[register] =
                        Die.Alu.Dec8(Registers[register]);
            }
        }

        private bool ExecuteShiftedInstructionHi()
        {
            if (ExecutionEngine.Opcode.Definition.IsIX)
            {
                Registers.GetIX().SetHi(
                        Die.Alu.Dec8(Registers.GetIX().GetHi()));
                return true;
            }
            if (ExecutionEngine.Opcode.Definition.IsIY)
            {
                Registers.GetIY().SetHi(
                        Die.Alu.Dec8(Registers.GetIY().GetHi()));
                return true;
            }

            return false;
        }

        private bool ExecuteShiftedInstructionLo()
        {
            if (ExecutionEngine.Opcode.Definition.IsIX)
            {
                Registers.GetIX().SetLo(
                        Die.Alu.Dec8(Registers.GetIX().GetLo()));
                return true;
            }
            if (ExecutionEngine.Opcode.Definition.IsIY)
            {
                Registers.GetIY().SetLo(
                        Die.Alu.Dec8(Registers.GetIY().GetLo()));
                return true;
            }

            return false;
        }
    }
}
