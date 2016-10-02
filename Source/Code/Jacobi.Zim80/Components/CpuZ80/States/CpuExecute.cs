using Jacobi.Zim80.Components.CpuZ80.States.Instructions;
using System;

namespace Jacobi.Zim80.Components.CpuZ80.States
{
    internal class CpuExecute : CpuRefresh
    {
        private readonly Instruction _instruction;

        public CpuExecute(Die die)
            : base(die)
        {
            if (ExecutionEngine.Cycles.OpcodeDefinition == null)
                throw new InvalidOperationException("There is no active OpcodeDefinition.");
            if (ExecutionEngine.Cycles.OpcodeDefinition.Instruction == null)
                throw new InvalidOperationException("The active OpcodeDefinition has no associated Instruction: " 
                    + ExecutionEngine.Cycles.OpcodeDefinition.ToString());

            _instruction = (Instruction)Activator.CreateInstance(
                ExecutionEngine.Cycles.OpcodeDefinition.Instruction, Die);
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
            {
                ExecutionEngine.InterruptManager.ReleaseInterrupts();
                ExecutionEngine.NotifyInstructionExecuted();
            }
        }
    }
}
