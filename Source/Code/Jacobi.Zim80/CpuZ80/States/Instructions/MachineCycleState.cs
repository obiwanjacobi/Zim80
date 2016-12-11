namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal abstract class MachineCycleState : CpuState
    {
        public MachineCycleState(CpuZ80 cpu,
            MachineCycleNames activeMachineCycle) 
            : base(cpu)
        {
            ActiveMachineCycle = activeMachineCycle;
        }

        public override void OnClock(DigitalLevel level)
        {
            if (!IsActive) return;
            base.OnClock(level);
        }

        public MachineCycleNames ActiveMachineCycle
        { get; private set; }

        public bool IsActive
        {
            get { return ExecutionEngine.Cycles.MachineCycle == ActiveMachineCycle; }
        }
    }
}
