using Jacobi.Zim80.Components.CpuZ80.States.Instructions;
using System;

namespace Jacobi.Zim80.Components.CpuZ80.States
{
    internal class CpuExecute : CpuRefresh
    {
        private readonly Instruction _instruction;

        public CpuExecute(ExecutionEngine executionEngine)
            : base(executionEngine)
        {
            if (executionEngine.Cycles.OpcodeDefinition.Instruction == null)
                throw new InvalidOperationException("The active OpcodeDefinition has no associated Instruction: " + executionEngine.Cycles.OpcodeDefinition.ToString());

            _instruction = (Instruction)Activator.CreateInstance(
                executionEngine.Cycles.OpcodeDefinition.Instruction, executionEngine);
        }

        public override void OnClock(DigitalLevel level)
        {
            base.OnClock(level);
            _instruction.OnClock(level);

            HandleInstructionCompletion();
        }

        private void HandleInstructionCompletion()
        {
            if (IsComplete) return;

            IsComplete = _instruction.IsComplete;

            if (IsComplete)
                ExecutionEngine.NotifyInstructionExecuted();
        }
    }
}
