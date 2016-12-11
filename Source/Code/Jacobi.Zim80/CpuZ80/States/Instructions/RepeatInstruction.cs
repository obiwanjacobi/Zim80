namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal abstract class RepeatInstruction : MultiCycleInstruction
    {
        public RepeatInstruction(CpuZ80 cpu) 
            : base(cpu)
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

        protected CpuState CreateRepeatPart(MachineCycleNames machineCycle)
        {
            return new RepeatInstructionPart(Cpu, machineCycle, -2);
        }

        protected abstract bool IsConditionMet();

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
