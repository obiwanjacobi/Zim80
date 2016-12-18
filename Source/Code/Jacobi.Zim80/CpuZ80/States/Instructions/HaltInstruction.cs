namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class HaltInstruction : Instruction
    {
        public HaltInstruction(CpuZ80 cpu) 
            : base(cpu)
        { }

        protected override void OnClockNeg()
        {
            base.OnClockNeg();

            if (ExecutionEngine.Cycles.IsLastCycle)
                OnLastCycleFirstM();
        }

        protected override void OnLastCycleFirstM()
        {
            Cpu.Halt.Write(DigitalLevel.Low);

            if (ExecutionEngine.InterruptManager.HasInterruptWaiting)
                IsComplete = true;
        }
    }
}
