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
