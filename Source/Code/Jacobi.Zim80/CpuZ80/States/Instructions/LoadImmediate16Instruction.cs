using Jacobi.Zim80.CpuZ80.Opcodes;
using System;

namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class LoadImmediate16Instruction : ReadParametersInstruction
    {
        public LoadImmediate16Instruction(Die die)
            : base(die)
        { }

        protected override void OnLastCycleLastM()
        {
            ThrowIfNoParametersFound();
            var lsb = ExecutionEngine.MultiCycleOpcode.GetParameter(0);
            var msb = ExecutionEngine.MultiCycleOpcode.GetParameter(1);
            var value = OpcodeByte.MakeUInt16(lsb, msb);

            if (ExecutionEngine.Opcode.Definition.IsIX)
                Registers.IX = value;
            else if (ExecutionEngine.Opcode.Definition.IsIY)
                Registers.IY = value;
            else
                Registers[ExecutionEngine.Opcode.Definition.Register16FromP] = value;
        }
    }
}
