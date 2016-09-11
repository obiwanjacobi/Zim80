namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class IntModeInstruction : SingleCycleInstruction
    {
        public IntModeInstruction(Die die) 
            : base(die)
        { }

        protected override void OnLastCycleFirstM()
        {
            switch (ExecutionEngine.Opcode.Definition.Y & 0x03)
            {
                case 0:
                    Registers.Interrupt.InterruptMode = InterruptTypes.Int0;
                    break;
                //case 1:   ??
                case 2:
                    Registers.Interrupt.InterruptMode = InterruptTypes.Int1;
                    break;
                case 3:
                    Registers.Interrupt.InterruptMode = InterruptTypes.Int2;
                    break;
            }
        }
    }
}
