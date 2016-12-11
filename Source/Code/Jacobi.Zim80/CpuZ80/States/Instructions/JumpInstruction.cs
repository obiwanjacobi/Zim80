using Jacobi.Zim80.CpuZ80.Opcodes;

namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class JumpInstruction : ReadParametersInstruction
    {
        public JumpInstruction(CpuZ80 cpu)
            : base(cpu)
        { }

        protected override void OnLastCycleLastM()
        {
            if (IsConditionMet())
            {
                Registers.PC = OpcodeByte.MakeUInt16(InstructionM2.Data, InstructionM3.Data);
            }
        }

        protected virtual bool IsConditionMet()
        {
            if (ExecutionEngine.Opcode.Definition.Z == 2)
            {
                return Registers.FlagFromOpcodeY(ExecutionEngine.Opcode.Definition.Y);
            }
            else if (ExecutionEngine.Opcode.Definition.Z == 3)
            {
                // jp (no condition)
                return true;
            }

            throw Errors.AssignedToIllegalOpcode();
        }
    }
}
