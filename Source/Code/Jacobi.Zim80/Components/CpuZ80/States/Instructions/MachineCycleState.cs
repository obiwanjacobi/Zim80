namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal abstract class MachineCycleState : CpuState
    {
        public MachineCycleState(Die die,
            MachineCycleNames activeMachineCycle) 
            : base(die)
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
