namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class DjnzInstruction : ReadParametersInstruction
    {
        public DjnzInstruction(Die die)
            : base(die)
        { }

        protected override CpuState GetInstructionPart(MachineCycleNames machineCycle)
        {
            switch (machineCycle)
            {
                case MachineCycleNames.M3:
                    // just chews thru the M3 cycles.
                    return new AutoCompleteInstructionPart(Die, machineCycle);
                default:
                    return base.GetInstructionPart(machineCycle);
            }
        }

        protected override void OnClockNeg()
        {
            base.OnClockNeg();

            if (ExecutionEngine.Cycles.IsMachineCycle1 &&
                ExecutionEngine.Cycles.IsLastCycle)
            {
                Registers.PrimarySet.B =
                    Die.Alu.Dec8(Registers.PrimarySet.B);

                if (Registers.PrimarySet.Flags.Z)
                {
                    ExecutionEngine.Cycles.SetAltCycles();
                }
            }
        }

        protected override void OnExecute()
        {
            // perform jump
            if (!Registers.PrimarySet.Flags.Z)
            {
                sbyte d = (sbyte)InstructionM2.Data.Value;
                Registers.PC = 
                    Alu.Add(Registers.PC, d);
            }
        }
    }
}
