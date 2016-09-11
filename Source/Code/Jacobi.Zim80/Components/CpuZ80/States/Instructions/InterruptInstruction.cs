namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class InterruptInstruction : SingleCycleInstruction
    {
        public InterruptInstruction(Die die) 
            : base(die)
        { }

        protected override void OnLastCycleFirstM()
        {
            switch(ExecutionEngine.Opcode.Definition.Y)
            {
                case 6: // di
                    Registers.Interrupt.DisableInterrupt();
                    break;
                case 7: // ei
                    Registers.Interrupt.EnableInterrupt();
                    ExecutionEngine.SuspendInterrupts();
                    break;
                default:
                    throw Errors.AssignedToIllegalOpcode();
            }
        }
    }
}
