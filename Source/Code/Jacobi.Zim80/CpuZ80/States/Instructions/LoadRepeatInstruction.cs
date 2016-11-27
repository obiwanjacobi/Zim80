namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class LoadRepeatInstruction : RepeatInstruction
    {
        private ReadT3InstructionPart _readPart;
        private WriteT3InstructionPart _writePart;

        public LoadRepeatInstruction(Die die) 
            : base(die)
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

        protected override void MoveNext()
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
            Registers.Flags.Z = Alu.IsZero(Registers.B) && Alu.IsZero(Registers.C);
            Registers.Flags.H = true;
            Registers.Flags.N = true;
        }

        private CpuState CreateWriteDatatPart(MachineCycleNames machineCycle)
        {
            _writePart = new Instructions.WriteT3InstructionPart(Die, machineCycle, Registers.DE)
            {
                Data = _readPart.Data
            };
            return _writePart;
        }

        private CpuState CreateReadDataPart(MachineCycleNames machineCycle)
        {
            _readPart = new ReadT3InstructionPart(Die, machineCycle, Registers.HL);
            return _readPart;
        }
    }
}
