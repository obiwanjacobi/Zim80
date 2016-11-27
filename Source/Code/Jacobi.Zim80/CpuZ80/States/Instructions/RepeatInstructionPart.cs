using System;

namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class RepeatInstructionPart : AutoCompleteInstructionPart
    {
        private readonly sbyte _deltaPC;

        public RepeatInstructionPart(Die die, MachineCycleNames activeMachineCycle, sbyte deltaPC) 
            : base(die, activeMachineCycle)
        {
            if (deltaPC >= 0) throw new ArgumentException(
                "The value must be negative.", "deltaPC");

            _deltaPC = deltaPC;
        }

        protected override void OnClockHigh()
        {
            if (ExecutionEngine.Cycles.IsLastCycle)
            {
                Die.Registers.PC = Alu.Add(Die.Registers.PC, _deltaPC);
            }

            base.OnClockHigh();
        }
    }
}
