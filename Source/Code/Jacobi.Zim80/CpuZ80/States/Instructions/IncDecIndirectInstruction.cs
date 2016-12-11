using Jacobi.Zim80.CpuZ80.Opcodes;

namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class IncDecIndirectInstruction : ReadIndirectInstruction
    {
        public IncDecIndirectInstruction(CpuZ80 cpu) 
            : base(cpu)
        { }

        private ReadT3InstructionPart _instructionM2;
        private WriteT3InstructionPart _instructionM3;

        protected override CpuState GetInstructionPart(MachineCycleNames machineCycle)
        {
            switch (machineCycle)
            {
                case MachineCycleNames.M2:
                    _instructionM2 = new ReadT3InstructionPart(Cpu, machineCycle, GetAddress());
                    return _instructionM2;
                case MachineCycleNames.M3:
                    _instructionM3 = new WriteT3InstructionPart(Cpu, machineCycle, GetAddress());
                    _instructionM3.Data = IncDecValue(_instructionM2.Data);
                    return _instructionM3;
                default:
                    throw Errors.InvalidMachineCycle(machineCycle);
            }
        }

        protected OpcodeByte IncDecValue(OpcodeByte value)
        {
            byte result;
            switch (ExecutionEngine.Opcode.Definition.Z)
            {
                case 4:  // INC (HL)
                    result = Cpu.Alu.Inc8(value.Value);
                    break;
                case 5:  // DEC (HL)
                    result = Cpu.Alu.Dec8(value.Value);
                    break;
                default:
                    throw Errors.AssignedToIllegalOpcode();
            }

            return new OpcodeByte(result);
        }
    }
}
