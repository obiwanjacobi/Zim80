namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class MathIndirectInstruction : ReadIndirectInstruction
    {
        private ReadT3InstructionPart _instructionPart;

        public MathIndirectInstruction(CpuZ80 cpu) 
            : base(cpu)
        { }

        protected override CpuState GetInstructionPart(MachineCycleNames machineCycle)
        {
            var hasExtension = ExecutionEngine.Opcode.Definition.HasExtension;

            switch (machineCycle)
            {
                case MachineCycleNames.M2:
                    if (!hasExtension)
                        return CreateReadIndirectValueInstructionPart(machineCycle);
                    
                    // read d param
                    return base.GetInstructionPart(machineCycle);
                case MachineCycleNames.M3:
                    if (!hasExtension)
                        throw Errors.InvalidMachineCycle(machineCycle);
                    // z80 does the IX+d arithmetic
                    return new AutoCompleteInstructionPart(Cpu, machineCycle);
                case MachineCycleNames.M4:
                    if (!hasExtension)
                        throw Errors.InvalidMachineCycle(machineCycle);

                    return CreateReadIndirectValueInstructionPart(machineCycle);
                default:
                    throw Errors.InvalidMachineCycle(machineCycle);
            }
        }

        protected override void OnLastCycleLastM()
        {
            Cpu.Alu.DoAccumulatorMath(ExecutionEngine.Opcode.Definition.MathOperationFromY, _instructionPart.Data.Value);
        }

        private CpuState CreateReadIndirectValueInstructionPart(MachineCycleNames machineCycle)
        {
            _instructionPart = new ReadT3InstructionPart(Cpu, machineCycle, GetAddress());
            return _instructionPart;
        }
    }
}
