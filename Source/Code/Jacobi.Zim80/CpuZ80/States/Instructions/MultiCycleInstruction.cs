namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal abstract class MultiCycleInstruction : Instruction
    {
        private CpuState _currentPart;

        public MultiCycleInstruction(Die die)
            : base(die)
        { }

        protected abstract CpuState GetInstructionPart(MachineCycleNames machineCycle);

        public override void OnClock(DigitalLevel level)
        {
            if (_currentPart != null)
                _currentPart.OnClock(level);

            base.OnClock(level);
        }

        protected override void OnClockNeg()
        {
            base.OnClockNeg();

            if (_currentPart != null &&
                _currentPart.IsComplete)
            {
                OnInstructionPartCompleted(_currentPart);
            }

            if (ExecutionEngine.Cycles.IsLastCycle)
            {
                if (ExecutionEngine.Cycles.IsMachineCycle1)
                {
                    OnLastCycleFirstM();

                    if (!IsComplete)
                        SetNextInstructionPart();
                }
                else if (ExecutionEngine.Cycles.IsLastMachineCycle)
                {
                    OnLastCycleLastM();
                    IsComplete = true;
                }
                else if (_currentPart.IsComplete)
                {
                    SetNextInstructionPart();
                }
            }
            else if (ExecutionEngine.Cycles.IsLastMachineCycle &&
                     ExecutionEngine.Cycles.CycleName == CycleNames.T1)
            {
                OnFirstCycleLastM();
            }
        }

        protected virtual void OnInstructionPartCompleted(CpuState completedPart)
        {
            if (completedPart == null ||
                !completedPart.IsComplete)
            {
                throw Errors.InstructionPartWasNotCompleted();
            }
        }

        protected override void OnLastCycleFirstM()
        { }

        protected virtual void OnFirstCycleLastM()
        { }

        protected virtual void OnLastCycleLastM()
        { }

        protected ushort GetHLOrIXIY(bool useOffset = true)
        {
            if (ExecutionEngine.Opcode.Definition.IsIX)
            {
                if (ExecutionEngine.Opcode.Definition.d && useOffset)
                {
                    ThrowIfNoParametersFound();
                    return Alu.Add(Registers.IX, (sbyte)ExecutionEngine.MultiCycleOpcode.GetParameter(0).Value);
                }
                return Registers.IX;
            }
            if (ExecutionEngine.Opcode.Definition.IsIY)
            {
                if (ExecutionEngine.Opcode.Definition.d && useOffset)
                {
                    ThrowIfNoParametersFound();
                    return Alu.Add(Registers.IY, (sbyte)ExecutionEngine.MultiCycleOpcode.GetParameter(0).Value);
                }
                return Registers.IY;
            }

            return Registers.HL;
        }

        protected void ThrowIfNoParametersFound()
        {
            if (ExecutionEngine.MultiCycleOpcode == null)
                throw Errors.AssignedToIllegalOpcode();
            if (ExecutionEngine.MultiCycleOpcode.ParameterCount == 0)
                throw Errors.ParametersNotFound();
        }

        private void SetNextInstructionPart()
        {
            if (_currentPart != null && !_currentPart.IsComplete)
                throw Errors.InstructionPartWasNotCompleted();

            _currentPart = GetInstructionPart(ExecutionEngine.Cycles.MachineCycle + 1);

            if (_currentPart == null)
                throw Errors.NextInstructionPartIsNull();
        }
    }
}
