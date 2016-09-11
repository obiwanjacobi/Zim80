namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class Interrupt : Instruction
    {
        public Interrupt(Die die) 
            : base(die)
        { }

        protected override void OnLastCycleFirstM()
        {
        }
    }
}
