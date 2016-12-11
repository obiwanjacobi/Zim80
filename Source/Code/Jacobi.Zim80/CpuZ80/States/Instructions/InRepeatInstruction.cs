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
                SetFlags();
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
        }

        private void DecrementCount()
        {
            Registers.B = Cpu.Alu.Dec8(Registers.B);
        }

        private void SetFlags()
        {
            Registers.Flags.N = (_writePart.Data.Value & (1 << 7)) != 0;

            // undocumented p16/p17
            byte adjustedC = (byte)(Registers.C + (IsDecrement ? -1 : 1));
            var temp = _writePart.Data.Value + adjustedC;
            Registers.Flags.H = Registers.Flags.C = (temp > 0xFF);
            Registers.Flags.PV = Alu.IsParityEven((byte)((temp & 7) ^ Registers.B));
        }

        private CpuState CreateInputPart(MachineCycleNames machineCycle)
        {
            DecrementCount();

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
