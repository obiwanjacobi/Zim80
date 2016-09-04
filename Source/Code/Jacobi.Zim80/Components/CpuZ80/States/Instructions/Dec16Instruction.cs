using Jacobi.Zim80.Components.CpuZ80.Opcodes;

namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class Dec16Instruction : SingleCycleInstruction
    {
        public Dec16Instruction(Die die)
            : base(die)
        { }

        protected override void OnLastCycleFirstM()
        {
            switch (ExecutionEngine.Opcode.Definition.Register16FromP)
            {
                case Register16Table.BC:
                    Registers.PrimarySet.BC =
                        Die.Alu.Dec16(Registers.PrimarySet.BC);
                    break;
                case Register16Table.DE:
                    Registers.PrimarySet.DE =
                        Die.Alu.Dec16(Registers.PrimarySet.DE);
                    break;
                case Register16Table.HL:
                    if (!ExecuteShiftedInstruction())
                        Registers.PrimarySet.HL =
                            Die.Alu.Dec16(Registers.PrimarySet.HL);
                    break;
                case Register16Table.SP:
                    Registers.SP =
                        Die.Alu.Dec16(Registers.SP);
                    break;
            }
        }

        private bool ExecuteShiftedInstruction()
        {
            if (ExecutionEngine.Opcode.Definition.IsIX)
            {
                // ix
                Registers.IX = Die.Alu.Dec16(Registers.IX);
                return true;
            }

            if (ExecutionEngine.Opcode.Definition.IsIY)
            {
                // iy
                Registers.IY = Die.Alu.Dec16(Registers.IY);
                return true;
            }

            return false;
        }
    }
}
