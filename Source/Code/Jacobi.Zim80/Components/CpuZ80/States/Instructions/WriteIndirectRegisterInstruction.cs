using Jacobi.Zim80.Components.CpuZ80.Opcodes;

namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class WriteIndirectRegisterInstruction : WriteIndirectInstruction
    {
        private WriteT3InstructionPart _instructionPart;

        public WriteIndirectRegisterInstruction(Die die)
            : base(die)
        { }

        protected override CpuState GetInstructionPart(MachineCycleNames machineCycle)
        {
            switch (machineCycle)
            {
                case MachineCycleNames.M2:
                    if (!ExecutionEngine.Opcode.Definition.HasExtension)
                        return CreateWriteAddressInstructionPart(machineCycle);

                    return base.GetInstructionPart(machineCycle);
                case MachineCycleNames.M3:
                    if (!ExecutionEngine.Opcode.Definition.HasExtension)
                        throw Errors.InvalidMachineCycle(machineCycle);
                    // z80 does the IX+d arithemtic
                    return new AutoCompleteInstructionPart(Die, machineCycle);
                case MachineCycleNames.M4:
                    if (!ExecutionEngine.Opcode.Definition.HasExtension)
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
            _instructionPart = new WriteT3InstructionPart(Die, machineCycle, GetAddress());
            return _instructionPart;
        }
    }
}
