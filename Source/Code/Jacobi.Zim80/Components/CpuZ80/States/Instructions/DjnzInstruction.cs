namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class DjnzInstruction : ReadParametersInstruction
    {
        public DjnzInstruction(ExecutionEngine executionEngine) 
            : base(executionEngine)
        { }

        protected override CpuState GetInstructionPart(MachineCycleNames machineCycle)
        {
            switch (machineCycle)
            {
                case MachineCycleNames.M3:
                    // just chews thru the M3 cycles.
                    return new AutoCompleteInstructionPart(ExecutionEngine, machineCycle);
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
                ExecutionEngine.Die.Registers.PrimarySet.B =
                    ExecutionEngine.Die.Alu.Dec8(ExecutionEngine.Die.Registers.PrimarySet.B);

                if (ExecutionEngine.Die.Registers.PrimarySet.Flags.Z)
                {
                    ExecutionEngine.Cycles.SetAltCycles();
                }
            }
        }

        protected override void OnExecute()
        {
            // perform jump
            if (!ExecutionEngine.Die.Registers.PrimarySet.Flags.Z)
            {
                sbyte d = (sbyte)InstructionM2.Data.Value;
                ExecutionEngine.Die.Registers.PC = 
                    Alu.Add(ExecutionEngine.Die.Registers.PC, d);
            }
        }
    }
}
