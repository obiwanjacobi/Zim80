namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class CompareRepeatInstruction : RepeatInstruction
    {
        private ReadT3InstructionPart _readPart;

        public CompareRepeatInstruction(CpuZ80 cpu) 
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
                    // z80 does computation
                    return new AutoCompleteInstructionPart(Cpu, machineCycle);
                case MachineCycleNames.M4:
                    if (!IsRepeat)
                        throw Errors.InvalidMachineCycle(machineCycle);
                    return CreateRepeatPart(machineCycle);
                default:
                    throw Errors.InvalidMachineCycle(machineCycle);
            }
        }

        protected void MoveNext()
        {
            if (IsDecrement)
                Registers.HL--;
            else
                Registers.HL++;

            Registers.BC--;

            bool c = Registers.Flags.C; // save carry
            var temp = Cpu.Alu.Sub8(Registers.A, _readPart.Data.Value);
            Registers.Flags.C = c;  // carry is unaffected
            Registers.Flags.PV = Registers.BC != 0;
            Registers.Flags.N = true;

            // undocumented p16
            if (Registers.Flags.H) temp--;
            Registers.Flags.X = (temp & (1 << 3)) > 0;
            Registers.Flags.Y = (temp & (1 << 1)) > 0;
        }

        protected override bool IsConditionMet()
        {
            return IsRepeat && (Registers.Flags.Z || Registers.BC == 0);
        }

        private CpuState CreateReadDataPart(MachineCycleNames machineCycle)
        {
            _readPart = new Instructions.ReadT3InstructionPart(Cpu, machineCycle, Registers.HL);
            return _readPart;
        }
    }
}
