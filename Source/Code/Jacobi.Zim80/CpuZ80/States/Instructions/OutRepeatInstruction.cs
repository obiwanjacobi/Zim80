namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class OutRepeatInstruction : RepeatInstruction
    {
        private ReadT3InstructionPart _readPart;
        private OutputInstructionPart _outputPart;

        public OutRepeatInstruction(CpuZ80 cpu) 
            : base(cpu)
        { }

        protected override CpuState GetInstructionPart(MachineCycleNames machineCycle)
        {
            switch (machineCycle)
            {
                case MachineCycleNames.M2:
                    return CreateReadDataPart(machineCycle);
                case MachineCycleNames.M3:
                    return CreateOutputPart(machineCycle);
                case MachineCycleNames.M4:
                    if (!IsRepeat)
                        throw Errors.InvalidMachineCycle(machineCycle);
                    return CreateRepeatPart(machineCycle);
                default:
                    throw Errors.InvalidMachineCycle(machineCycle);
            }
        }

        private CpuState CreateReadDataPart(MachineCycleNames machineCycle)
        {
            _readPart = new ReadT3InstructionPart(Cpu, machineCycle, Registers.HL);
            return _readPart;
        }

        private CpuState CreateOutputPart(MachineCycleNames machineCycle)
        {
            // decremented B is the IO address MSB
            MoveNext();

            _outputPart = new OutputInstructionPart(Cpu, machineCycle, Registers.BC)
            {
                Data = _readPart.Data
            };
            return _outputPart;
        }
    }
}
