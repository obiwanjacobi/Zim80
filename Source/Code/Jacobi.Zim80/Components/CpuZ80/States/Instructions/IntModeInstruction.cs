namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class IntModeInstruction : SingleCycleInstruction
    {
        public IntModeInstruction(Die die) 
            : base(die)
        { }

        protected override void OnLastCycleFirstM()
        {
            switch (ExecutionEngine.Opcode.Definition.Y)
            {
                case 0:
                case 1:
                case 4:
                case 5:
                    Registers.Interrupt.InterruptMode = InterruptModes.InterruptMode0;
                    break;
                case 2:
                case 6:
                    Registers.Interrupt.InterruptMode = InterruptModes.InterruptMode1;
                    break;
                case 3:
                case 7:
                    Registers.Interrupt.InterruptMode = InterruptModes.InterruptMode2;
                    break;
            }
        }
    }
}
