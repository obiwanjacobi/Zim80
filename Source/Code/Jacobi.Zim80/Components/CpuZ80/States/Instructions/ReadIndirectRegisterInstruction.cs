using Jacobi.Zim80.Components.CpuZ80.Opcodes;

namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class ReadIndirectRegisterInstruction : ReadIndirectInstruction
    {
        private ReadT3InstructionPart _instructionPart;

        public ReadIndirectRegisterInstruction(Die die)
            : base(die)
        { }

        protected override CpuState GetInstructionPart(MachineCycleNames machineCycle)
        {
            var hasExtension = ExecutionEngine.Opcode.Definition.HasExtension;

            switch (machineCycle)
            {
                case MachineCycleNames.M2:
                    if (!hasExtension)
                        return CreateReadAddressInstructionPart(machineCycle);
                    // read d param
                    return base.GetInstructionPart(machineCycle);
                case MachineCycleNames.M3:
                    if (!hasExtension)
                        throw Errors.InvalidMachineCycle(machineCycle);
                    // z80 does the IX+d arithmetic
                    return new AutoCompleteInstructionPart(Die, machineCycle);
                case MachineCycleNames.M4:
                    if (!hasExtension)
                        throw Errors.InvalidMachineCycle(machineCycle);

                    return CreateReadAddressInstructionPart(machineCycle);
                default:
                    throw Errors.InvalidMachineCycle(machineCycle);
            }
        }

        private ReadT3InstructionPart CreateReadAddressInstructionPart(MachineCycleNames machineCycle)
        {
            _instructionPart = new ReadT3InstructionPart(Die, machineCycle, GetAddress());
            return _instructionPart;
        }

        protected override void OnLastCycleLastM()
        {
            var x = ExecutionEngine.Opcode.Definition.X;
            var z = ExecutionEngine.Opcode.Definition.Z;

            var reg = Register8Table.A;

            if (x == 1 && z == 6)
            {
                reg = ExecutionEngine.Opcode.Definition.Register8FromY;
                if (reg == Register8Table.HL) throw Errors.AssignedToIllegalOpcode(); // halt
            }

            Registers[reg] = _instructionPart.Data.Value;
        }
    }
}
