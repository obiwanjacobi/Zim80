namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal abstract class Instruction : CpuState
    {
        protected Instruction(Die die)
            : base(die)
        { }

        protected Registers Registers { get { return Die.Registers; } }

        protected abstract void OnLastCycleFirstM();
    }
}
