using Jacobi.Zim80.CpuZ80.Opcodes;
using Jacobi.Zim80.CpuZ80.States.Instructions;
using System;

namespace Jacobi.Zim80.CpuZ80.States
{
    internal class CpuInterrupt : CpuState
    {
        private readonly Interrupt _interrupt;

        public CpuInterrupt(Die die, OpcodeDefinition definition) 
            : base(die)
        {
            if (definition == null)
                throw new ArgumentNullException("definition");

            _definition = definition;

            if (_definition.Instruction == null)
                throw new InvalidOperationException(
                    "The Interrupt OpcodeDefinition has no associated Instruction: "
                    + definition.ToString());

            _interrupt = (Interrupt)Activator.CreateInstance(
                _definition.Instruction, Die, _definition);
        }

        private readonly OpcodeDefinition _definition;
        public OpcodeDefinition Definition
        {
            get { return _definition; }
        }

        public override void OnClock(DigitalLevel level)
        {
            base.OnClock(level);
            _interrupt.OnClock(level);

            HandleInstructionCompletion();
        }

        private void HandleInstructionCompletion()
        {
            if (IsComplete) return;

            IsComplete = _interrupt.IsComplete;

            if (IsComplete)
            {
                //ExecutionEngine.InterruptManager.ReleaseInterrupts();
                //ExecutionEngine.NotifyInterruptExecuted();
            }
        }
    }
}
