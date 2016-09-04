using Jacobi.Zim80.Components.CpuZ80.Opcodes;

namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class Inc16Instruction : SingleCycleInstruction
    {
        public Inc16Instruction(Die die)
            : base(die)
        { }

        protected override void OnLastCycleFirstM()
        {
            switch (ExecutionEngine.Opcode.Definition.Register16FromP)
            {
                case Register16Table.BC:
                    Registers.PrimarySet.BC =
                        Die.Alu.Inc16(Registers.PrimarySet.BC);
                    break;
                case Register16Table.DE:
                    Registers.PrimarySet.DE =
                        Die.Alu.Inc16(Registers.PrimarySet.DE);
                    break;
                case Register16Table.HL:
                    if (!ExecuteShiftedInstruction())
                        Registers.PrimarySet.HL =
                            Die.Alu.Inc16(Registers.PrimarySet.HL);
                    break;
                case Register16Table.SP:
                    Registers.SP =
                        Die.Alu.Inc16(Registers.SP);
                    break;
            }
        }

        private bool ExecuteShiftedInstruction()
        {
            if (ExecutionEngine.Opcode.Definition.IsIX)
            {
                Registers.IX = Die.Alu.Inc16(Registers.IX);
                return true;
            }
            if (ExecutionEngine.Opcode.Definition.IsIY)
            {
                Registers.IY = Die.Alu.Inc16(Registers.IY);
                return true;
            }

            return false;
        }
    }
}
