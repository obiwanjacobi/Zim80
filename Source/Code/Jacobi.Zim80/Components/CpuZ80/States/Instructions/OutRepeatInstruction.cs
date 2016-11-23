namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class OutRepeatInstruction : MultiCycleInstruction
    {
        private ReadT3InstructionPart _readPart;
        private OutputInstructionPart _outputPart;

        public OutRepeatInstruction(Die die) 
            : base(die)
        { }

        protected override void OnClockNeg()
        {
            if (ExecutionEngine.Cycles.IsLastCycle &&
                ExecutionEngine.Cycles.MachineCycle == MachineCycleNames.M3)
            {
                if (IsConditionMet())
                    ExecutionEngine.Cycles.SetAltCycles();
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

        private CpuState CreateReadDataPart(MachineCycleNames machineCycle)
        {
            _readPart = new ReadT3InstructionPart(Die, machineCycle, Registers.HL);
            return _readPart;
        }

        private CpuState CreateOutputPart(MachineCycleNames machineCycle)
        {
            // decremented B is the IO address MSB
            MoveNext();

            _outputPart = new OutputInstructionPart(Die, machineCycle, Registers.BC)
            {
                Data = _readPart.Data
            };
            return _outputPart;
        }

        private CpuState CreateRepeatPart(MachineCycleNames machineCycle)
        {
            return new RepeatInstructionPart(Die, machineCycle, -2);
        }

        private bool IsConditionMet()
        {
            return IsRepeat && Registers.B == 0;
        }

        private void MoveNext()
        {
            if (IsDecrement)
                Registers.HL--;
            else
                Registers.HL++;

            Registers.B--;
            Registers.Flags.Z = Alu.IsZero(Registers.B);
            Registers.Flags.N = true;
        }

        private bool IsDecrement
        {
            get { return ExecutionEngine.Opcode.Definition.Y % 2 == 1; }
        }

        private bool IsRepeat
        {
            get { return ExecutionEngine.Opcode.Definition.Y >= 6; }
        }
    }
}
