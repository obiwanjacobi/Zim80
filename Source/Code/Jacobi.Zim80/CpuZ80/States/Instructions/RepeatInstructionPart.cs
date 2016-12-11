using System;

namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class RepeatInstructionPart : AutoCompleteInstructionPart
    {
        private readonly sbyte _deltaPC;

        public RepeatInstructionPart(CpuZ80 cpu, MachineCycleNames activeMachineCycle, sbyte deltaPC) 
            : base(cpu, activeMachineCycle)
        {
            if (deltaPC >= 0) throw new ArgumentException(
                "The value must be negative.", "deltaPC");

            _deltaPC = deltaPC;
        }

        protected override void OnClockHigh()
        {
            if (ExecutionEngine.Cycles.IsLastCycle)
            {
                Cpu.Registers.PC = Alu.Add(Cpu.Registers.PC, _deltaPC);
            }

            base.OnClockHigh();
        }
    }
}
