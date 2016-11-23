namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class CompareRepeatInstruction : RepeatInstruction
    {
        private ReadT3InstructionPart _readPart;

        public CompareRepeatInstruction(Die die) 
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
                    // z80 does computation
                    return new AutoCompleteInstructionPart(Die, machineCycle);
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
                Registers.HL--;
            else
                Registers.HL++;

            Registers.BC--;

            bool c = Registers.Flags.C; // save carry
            Die.Alu.Sub8(Registers.A, _readPart.Data.Value);
            Registers.Flags.PV = Registers.BC == 0;
            // carry is unaffected
            Registers.Flags.C = c;
        }

        protected override bool IsConditionMet()
        {
            return base.IsConditionMet() || (IsRepeat && Registers.BC == 0);
        }

        private CpuState CreateReadDataPart(MachineCycleNames machineCycle)
        {
            _readPart = new Instructions.ReadT3InstructionPart(Die, machineCycle, Registers.HL);
            return _readPart;
        }
    }
}
