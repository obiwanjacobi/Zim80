namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class LoadRepeatInstruction : RepeatInstruction
    {
        private ReadT3InstructionPart _readPart;
        private WriteT3InstructionPart _writePart;

        public LoadRepeatInstruction(CpuZ80 cpu) 
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
                    return CreateReadDataPart(machineCycle);
                case MachineCycleNames.M3:
                    return CreateWriteDatatPart(machineCycle);
                case MachineCycleNames.M4:
                    if (!IsRepeat)
                        throw Errors.InvalidMachineCycle(machineCycle);
                    return CreateRepeatPart(machineCycle);
                default:
                    throw Errors.InvalidMachineCycle(machineCycle);
            }
        }

        protected override bool IsConditionMet()
        {
            return IsRepeat && Registers.BC == 0;
        }

        protected void MoveNext()
        {
            if (IsDecrement)
            {
                Registers.DE--;
                Registers.HL--;
            }
            else
            {
                Registers.DE++;
                Registers.HL++;
            }

            Registers.BC--;
            Registers.Flags.H = false;
            Registers.Flags.N = true;
            Registers.Flags.PV = Registers.BC != 0;

            // undocumented p16
            var temp = _readPart.Data.Value + Registers.A;
            Registers.Flags.X = (temp & (1 << 3)) > 0;
            Registers.Flags.Y = (temp & (1 << 1)) > 0;
        }

        private CpuState CreateWriteDatatPart(MachineCycleNames machineCycle)
        {
            _writePart = new Instructions.WriteT3InstructionPart(Cpu, machineCycle, Registers.DE)
            {
                Data = _readPart.Data
            };
            return _writePart;
        }

        private CpuState CreateReadDataPart(MachineCycleNames machineCycle)
        {
            _readPart = new ReadT3InstructionPart(Cpu, machineCycle, Registers.HL);
            return _readPart;
        }
    }
}
