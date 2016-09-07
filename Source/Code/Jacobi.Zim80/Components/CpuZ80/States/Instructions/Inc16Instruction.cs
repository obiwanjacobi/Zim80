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
            var register = ExecutionEngine.Opcode.Definition.Register16FromP;

            if (register == Register16Table.SP)
            {
                Registers.SP = Die.Alu.Inc16(Registers.SP);
            }
            else if (!ExecuteShiftedInstruction())
            {
                Registers.PrimarySet[register] =
                        Die.Alu.Inc16(Registers.PrimarySet[register]);
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
