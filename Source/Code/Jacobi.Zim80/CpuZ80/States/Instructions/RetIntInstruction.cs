namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class RetIntInstruction : RetInstruction
    {
        public RetIntInstruction(CpuZ80 cpu) 
            : base(cpu)
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
