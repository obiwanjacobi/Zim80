namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class InRepeatInstruction : RepeatInstruction
    {
        private InputInstructionPart _inputPart;
        private WriteT3InstructionPart _writePart;

        public InRepeatInstruction(CpuZ80 cpu) 
            : base(cpu)
        { }

        protected override void OnClockNeg()
        {
            if (ExecutionEngine.Cycles.IsLastCycle &&
                ExecutionEngine.Cycles.MachineCycle == MachineCycleNames.M3)
            {
                MoveNext();
            }

            base.OnClockNeg();
        }

        protected override CpuState GetInstructionPart(MachineCycleNames machineCycle)
        {
            switch (machineCycle)
            {
                case MachineCycleNames.M2:
                    return CreateInputPart(machineCycle);
                case MachineCycleNames.M3:
                    return CreateWriteDataPart(machineCycle);
                case MachineCycleNames.M4:
                    if (!IsRepeat)
                        throw Errors.InvalidMachineCycle(machineCycle);
                    return CreateRepeatPart(machineCycle);
                default:
                    throw Errors.InvalidMachineCycle(machineCycle);
            }
        }

        private CpuState CreateInputPart(MachineCycleNames machineCycle)
        {
            _inputPart = new InputInstructionPart(Cpu, machineCycle, Registers.BC);
            return _inputPart;
        }

        private CpuState CreateWriteDataPart(MachineCycleNames machineCycle)
        {
            _writePart = new WriteT3InstructionPart(Cpu, machineCycle, Registers.HL)
            {
                Data = _inputPart.Data
            };
            return _writePart;
        }
    }
}
