namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class Interrupt : Instruction
    {
        public Interrupt(Die die) 
            : base(die)
        { }

        protected override void OnClockPos()
        {
            if (ExecutionEngine.Cycles.IsMachineCycle1 &&
                ExecutionEngine.Cycles.IsFirstCycle)
                Registers.Interrupt.PushInt();

            base.OnClockPos();
        }

        protected override void OnLastCycleFirstM()
        {
        }
    }
}
