using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using System;

namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class LoadImmediate16Instruction : ReadParametersInstruction
    {
        public LoadImmediate16Instruction(Die die)
            : base(die)
        { }

        protected override void OnLastCycleLastM()
        {
            var lsb = ExecutionEngine.MultiCycleOpcode.GetParameter(0);
            var msb = ExecutionEngine.MultiCycleOpcode.GetParameter(1);
            var value = OpcodeByte.MakeUInt16(lsb, msb);

            if (ExecuteShiftedInstruction(value))
                return;

            var register = ExecutionEngine.Opcode.Definition.Register16FromP;
            if (register == Register16Table.SP)
            {
                Registers.SP = value;
            }
            else
            {
                Registers.PrimarySet[register] = value;
            }
        }

        private bool ExecuteShiftedInstruction(ushort value)
        {
            if (ExecutionEngine.Opcode.Definition.IsIX)
            {
                Registers.IX = value;
                return true;
            }
            if (ExecutionEngine.Opcode.Definition.IsIY)
            {
                Registers.IY = value;
                return true;
            }

            return false;
        }
    }
}
