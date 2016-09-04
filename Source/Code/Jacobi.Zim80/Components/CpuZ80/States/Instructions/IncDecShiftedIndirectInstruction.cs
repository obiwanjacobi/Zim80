namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class IncDecShiftedIndirectInstruction : IncDecIndirectInstruction
    {
        public IncDecShiftedIndirectInstruction(Die die) 
            : base(die)
        { }

        private ReadT3InstructionPart _instructionM2;
        private ReadT3InstructionPart _instructionM4;
        private WriteT3InstructionPart _instructionM5;

        protected override CpuState GetInstructionPart(MachineCycleNames machineCycle)
        {
            switch (machineCycle)
            {
                case MachineCycleNames.M2:
                    // read d
                    _instructionM2 = new ReadT3InstructionPart(Die, machineCycle);
                    return _instructionM2;
                case MachineCycleNames.M3:
                    // we postpone inc/dec operation until M4
                    return new AutoCompleteInstructionPart(Die, machineCycle);
                case MachineCycleNames.M4:
                    _instructionM4 = new ReadT3InstructionPart(Die, machineCycle, GetAddress());
                    return _instructionM4;
                case MachineCycleNames.M5:
                    _instructionM5 = new WriteT3InstructionPart(Die, machineCycle, GetAddress());
                    _instructionM5.Data = IncDecValue(_instructionM4.Data);
                    return _instructionM5;
                default:
                    throw Errors.InvalidMachineCycle(machineCycle);
            }
        }
    }
}

