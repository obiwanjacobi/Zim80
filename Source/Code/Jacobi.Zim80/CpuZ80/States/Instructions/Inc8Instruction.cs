namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class Inc8Instruction : SingleCycleInstruction
    {
        public Inc8Instruction(CpuZ80 cpu)
            : base(cpu)
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
                        Cpu.Alu.Inc8(Registers[register]);
            }
        }

        private bool ExecuteShiftedInstructionHi()
        {
            if (ExecutionEngine.Opcode.Definition.IsIX)
            {
                Registers.GetIX().SetHi(
                        Cpu.Alu.Inc8(Registers.GetIX().GetHi()));
                return true;
            }
            if (ExecutionEngine.Opcode.Definition.IsIY)
            {
                Registers.GetIY().SetHi(
                        Cpu.Alu.Inc8(Registers.GetIY().GetHi()));
                return true;
            }

            return false;
        }

        private bool ExecuteShiftedInstructionLo()
        {
            if (ExecutionEngine.Opcode.Definition.IsIX)
            {
                Registers.GetIX().SetLo(
                        Cpu.Alu.Inc8(Registers.GetIX().GetLo()));
                return true;
            }
            if (ExecutionEngine.Opcode.Definition.IsIY)
            {
                Registers.GetIY().SetLo(
                        Cpu.Alu.Inc8(Registers.GetIY().GetLo()));
                return true;
            }

            return false;
        }
    }
}
