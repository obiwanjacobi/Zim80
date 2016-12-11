namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class OutRepeatInstruction : RepeatInstruction
    {
        private ReadT3InstructionPart _readPart;
        private OutputInstructionPart _outputPart;

        public OutRepeatInstruction(CpuZ80 cpu) 
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
                    return CreateOutputPart(machineCycle);
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
            return IsRepeat && Registers.B == 0;
        }

        protected void MoveNext()
        {
            if (IsDecrement)
                Registers.HL--;
            else
                Registers.HL++;

            DecrementCount();
            SetFlags();
        }

        private void DecrementCount()
        {
            Registers.B = Cpu.Alu.Dec8(Registers.B);
        }

        private void SetFlags()
        {
            Registers.Flags.N = (_readPart.Data.Value & (1 << 7)) != 0;

            // undocumented p16
            var temp = _readPart.Data.Value + Registers.L;
            Registers.Flags.H = Registers.Flags.C = (temp > 0xFF);
            Registers.Flags.PV = Alu.IsParityEven((byte)((temp & 7) ^ Registers.B));
        }

        private CpuState CreateReadDataPart(MachineCycleNames machineCycle)
        {
            _readPart = new ReadT3InstructionPart(Cpu, machineCycle, Registers.HL);
            return _readPart;
        }

        private CpuState CreateOutputPart(MachineCycleNames machineCycle)
        {
            _outputPart = new OutputInstructionPart(Cpu, machineCycle, Registers.BC)
            {
                Data = _readPart.Data
            };
            return _outputPart;
        }
    }
}
