namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class ReadIndirectRegisterInstruction : IndirectRegisterInstruction
    {
        private ReadT3InstructionPart _instructionPart;

        public ReadIndirectRegisterInstruction(Die die)
            : base(die)
        { }

        protected override CpuState GetInstructionPart(MachineCycleNames machineCycle)
        {
            switch (machineCycle)
            {
                case MachineCycleNames.M2:
                    _instructionPart = new ReadT3InstructionPart(Die, machineCycle, GetAddress());
                    return _instructionPart;
                default:
                    throw Errors.InvalidMachineCycle(machineCycle);
            }
        }

        protected override void OnLastCycleLastM()
        {
            var z = ExecutionEngine.Opcode.Definition.Z;
            var q = ExecutionEngine.Opcode.Definition.Q;
            var p = ExecutionEngine.Opcode.Definition.P;

            // TODO: check z -for other instructions

            Registers.PrimarySet.A = _instructionPart.Data.Value;
        }
    }
}
