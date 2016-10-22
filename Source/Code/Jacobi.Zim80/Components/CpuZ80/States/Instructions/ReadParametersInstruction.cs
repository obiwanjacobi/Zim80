namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal abstract class ReadParametersInstruction : MultiCycleInstruction
    {
        public ReadParametersInstruction(Die die)
            : base(die)
        { }

        protected ReadT3InstructionPart InstructionM2 { get; private set; }
        protected ReadT3InstructionPart InstructionM3 { get; private set; }

        protected override CpuState GetInstructionPart(MachineCycleNames machineCycle)
        {
            if (!ExecutionEngine.Opcode.Definition.HasParameters)
                throw Errors.NoParameters();

            switch (machineCycle)
            {
                case MachineCycleNames.M2:
                    InstructionM2 = new ReadT3InstructionPart(Die, machineCycle);
                    return InstructionM2;
                case MachineCycleNames.M3:
                    if (ExecutionEngine.Opcode.Definition.ParameterCount <= 1)
                        throw Errors.InvalidMachineCycle(machineCycle);

                    InstructionM3 = new ReadT3InstructionPart(Die, machineCycle);
                    return InstructionM3;
                default:
                    throw Errors.InvalidMachineCycle(machineCycle);
            }
        }
    }
}
