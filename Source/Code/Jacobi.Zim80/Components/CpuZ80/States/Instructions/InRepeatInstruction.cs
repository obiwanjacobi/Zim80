namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class InRepeatInstruction : MultiCycleInstruction
    {
        private InputInstructionPart _inputPart;
        private WriteT3InstructionPart _writePart;

        public InRepeatInstruction(Die die) 
            : base(die)
        { }

        protected override void OnClockNeg()
        {
            if (ExecutionEngine.Cycles.IsLastCycle &&
                ExecutionEngine.Cycles.MachineCycle == MachineCycleNames.M3)
            {
                MoveNext();

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
            _inputPart = new InputInstructionPart(Die, machineCycle, Registers.BC);
            return _inputPart;
        }

        private CpuState CreateWriteDataPart(MachineCycleNames machineCycle)
        {
            _writePart = new WriteT3InstructionPart(Die, machineCycle, Registers.HL)
            {
                Data = _inputPart.Data
            };
            return _writePart;
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
