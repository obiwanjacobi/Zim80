namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class RetIntInstruction : RetInstruction
    {
        public RetIntInstruction(Die die) 
            : base(die)
        { }

        protected override void OnLastCycleFirstM()
        {
            ExecutionEngine.InterruptManager.PopInt();
        }

        protected override bool IsConditionMet()
        {
            return true;
        }
    }
}
