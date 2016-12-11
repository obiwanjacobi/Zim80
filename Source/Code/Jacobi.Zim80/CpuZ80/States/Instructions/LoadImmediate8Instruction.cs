using Jacobi.Zim80.CpuZ80.Opcodes;
using System;

namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class LoadImmediate8Instruction : ReadParametersInstruction
    {
        public LoadImmediate8Instruction(CpuZ80 cpu)
            : base(cpu)
        { }

        protected override void OnLastCycleLastM()
        {
            var ob = ExecutionEngine.MultiCycleOpcode.GetParameter(0);
            var register = ExecutionEngine.Opcode.Definition.Register8FromY;

            if (ExecutionEngine.Opcode.Definition.IsIX ||
                ExecutionEngine.Opcode.Definition.IsIY)
            {
                switch (ExecutionEngine.Opcode.Definition.Register8FromY)
                {
                    case Register8Table.H:
                        ExecuteShiftedInstructionHi(ob.Value);
                        break;
                    case Register8Table.L:
                        ExecuteShiftedInstructionLo(ob.Value);
                        break;
                }
            }
            else
            {
                Registers[register] = ob.Value;
            }
        }

        private bool ExecuteShiftedInstructionHi(byte value)
        {
            if (ExecutionEngine.Opcode.Definition.IsIX)
            {
                Registers.GetIX().SetHi(value);
                return true;
            }
            if (ExecutionEngine.Opcode.Definition.IsIY)
            {
                Registers.GetIY().SetHi(value);
                return true;
            }

            return false;
        }

        private bool ExecuteShiftedInstructionLo(byte value)
        {
            if (ExecutionEngine.Opcode.Definition.IsIX)
            {
                Registers.GetIX().SetLo(value);
                return true;
            }
            if (ExecutionEngine.Opcode.Definition.IsIY)
            {
                Registers.GetIY().SetLo(value);
                return true;
            }

            return false;
        }
    }
}
