using Jacobi.Zim80.CpuZ80.States.Instructions;

namespace Jacobi.Zim80.CpuZ80.States
{
    internal class CpuExecute : CpuRefresh
    {
        private Instruction _instruction;

        public CpuExecute(CpuZ80 cpu, bool createInstruction = true)
            : base(cpu)
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
            _instruction = Instruction.Create<Instruction>(Cpu, ExecutionEngine.Opcode.Definition);
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
