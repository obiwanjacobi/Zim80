using Jacobi.Zim80.Components.CpuZ80.States.Instructions;

namespace Jacobi.Zim80.Components.CpuZ80.States
{
    internal class CpuExecute : CpuRefresh
    {
        private Instruction _instruction;

        public CpuExecute(Die die, bool createInstruction = true)
            : base(die)
        {
            if (createInstruction)
                CreateInstruction();
        }

        public override void OnClock(DigitalLevel level)
        {
            base.OnClock(level);

            if (_instruction != null)
                _instruction.OnClock(level);

            HandleInstructionCompletion();
        }

        protected void CreateInstruction()
        {
            _instruction = Instruction.Create<Instruction>(Die, ExecutionEngine.Opcode.Definition);
        }

        private void HandleInstructionCompletion()
        {
            if (IsComplete || _instruction == null) return;

            IsComplete = _instruction.IsComplete;

            if (IsComplete)
            {
                ExecutionEngine.InterruptManager.ReleaseInterrupts();
                ExecutionEngine.NotifyInstructionExecuted();
            }
        }
    }
}
