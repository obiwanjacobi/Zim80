﻿namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class InterruptInstruction : SingleCycleInstruction
    {
        public InterruptInstruction(CpuZ80 cpu) 
            : base(cpu)
        { }

        protected override void OnLastCycleFirstM()
        {
            switch(ExecutionEngine.Opcode.Definition.Y)
            {
                case 6: // di
                    ExecutionEngine.InterruptManager.DisableInterrupt();
                    break;
                case 7: // ei
                    ExecutionEngine.InterruptManager.EnableInterrupt();
                    ExecutionEngine.InterruptManager.SuspendInterrupts();
                    break;
                default:
                    throw Errors.AssignedToIllegalOpcode();
            }
        }
    }
}
