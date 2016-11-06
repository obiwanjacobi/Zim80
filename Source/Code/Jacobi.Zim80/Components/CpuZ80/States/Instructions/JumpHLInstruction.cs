namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class JumpHLInstruction : SingleCycleInstruction
    {
        public JumpHLInstruction(Die die) 
            : base(die)
        { }

        protected override void OnLastCycleFirstM()
        {
            Registers.PC = Registers.PrimarySet.HL;
        }
    }
}
