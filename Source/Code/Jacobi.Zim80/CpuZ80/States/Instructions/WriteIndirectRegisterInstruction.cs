using Jacobi.Zim80.CpuZ80.Opcodes;

namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class WriteIndirectRegisterInstruction : WriteIndirectInstruction
    {
        private WriteT3InstructionPart _instructionPart;

        public WriteIndirectRegisterInstruction(CpuZ80 cpu)
            : base(cpu)
        { }

        protected override CpuState GetInstructionPart(MachineCycleNames machineCycle)
        {
            var hasExtension = ExecutionEngine.Opcode.Definition.HasExtension;
            switch (machineCycle)
            {
                case MachineCycleNames.M2:
                    if (!hasExtension)
                        return CreateWriteAddressInstructionPart(machineCycle);

                    return base.GetInstructionPart(machineCycle);
                case MachineCycleNames.M3:
                    if (!hasExtension)
                        throw Errors.InvalidMachineCycle(machineCycle);
                    // z80 does the IX+d arithemtic
                    return new AutoCompleteInstructionPart(Cpu, machineCycle);
                case MachineCycleNames.M4:
                    if (!hasExtension)
                        throw Errors.InvalidMachineCycle(machineCycle);

                    return CreateWriteAddressInstructionPart(machineCycle);
                default:
                    throw Errors.InvalidMachineCycle(machineCycle);
            }
        }

        protected override void OnFirstCycleLastM()
        {
            _instructionPart.Data = new OpcodeByte(GetValue());
        }

        private WriteT3InstructionPart CreateWriteAddressInstructionPart(MachineCycleNames machineCycle)
        {
            _instructionPart = new WriteT3InstructionPart(Cpu, machineCycle, GetAddress());
            return _instructionPart;
        }
    }
}
