namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal abstract class RepeatInstruction : MultiCycleInstruction
    {
        public RepeatInstruction(Die die) 
            : base(die)
        { }

        protected override void OnClockNeg()
        {
            if (ExecutionEngine.Cycles.IsLastCycle &&
                ExecutionEngine.Cycles.MachineCycle == MachineCycleNames.M3)
            {
                if (IsConditionMet() && IsRepeat)
                {
                    if (ExecutionEngine.Opcode.Definition.AltCycles == null)
                        throw Errors.AssignedToIllegalOpcode();

                    ExecutionEngine.Cycles.SetAltCycles();
                }
            }

            base.OnClockNeg();
        }

        protected virtual void MoveNext()
        {
            if (IsDecrement)
                Registers.HL--;
            else
                Registers.HL++;

            Registers.B--;
            Registers.Flags.Z = Alu.IsZero(Registers.B);
            Registers.Flags.N = true;
        }

        protected CpuState CreateRepeatPart(MachineCycleNames machineCycle)
        {
            return new RepeatInstructionPart(Die, machineCycle, -2);
        }

        protected virtual bool IsConditionMet()
        {
            return IsRepeat && Registers.Flags.Z;
        }

        protected bool IsDecrement
        {
            get { return ExecutionEngine.Opcode.Definition.Y % 2 == 1; }
        }

        protected bool IsRepeat
        {
            get { return ExecutionEngine.Opcode.Definition.Y >= 6; }
        }
    }
}
