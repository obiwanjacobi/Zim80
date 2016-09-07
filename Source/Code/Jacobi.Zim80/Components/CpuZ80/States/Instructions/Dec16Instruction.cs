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
            var register = ExecutionEngine.Opcode.Definition.Register16FromP;

            if (register == Register16Table.SP)
            {
                Registers.SP = Die.Alu.Dec16(Registers.SP);
            }
            else if (!ExecuteShiftedInstruction())
            {
                Registers.PrimarySet[register] =
                        Die.Alu.Dec16(Registers.PrimarySet[register]);
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
