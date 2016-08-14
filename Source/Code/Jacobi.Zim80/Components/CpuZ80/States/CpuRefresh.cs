namespace Jacobi.Zim80.Components.CpuZ80.States
{
    internal abstract class CpuRefresh : CpuState
    {
        public CpuRefresh(Die die)
            : base(die)
        { }

        protected override void OnClockPos()
        {
            if (!ExecutionEngine.Cycles.IsMachineCycle1) return;

            switch (ExecutionEngine.Cycles.CycleName)
            {
                case CycleNames.T1:
                    Die.Refresh.Write(DigitalLevel.High);
                    break;

                case CycleNames.T3:
                    ExecutionEngine.SetRefreshOnAddressBus();
                    Die.Refresh.Write(DigitalLevel.Low);
                    break;
            }
        }

        protected override void OnClockNeg()
        {
            if (!ExecutionEngine.Cycles.IsMachineCycle1) return;

            switch (ExecutionEngine.Cycles.CycleName)
            {
                case CycleNames.T3:
                    Die.MemoryRequest.Write(DigitalLevel.Low);
                    break;
                case CycleNames.T4:
                    Die.MemoryRequest.Write(DigitalLevel.High);
                    break;
            }
        }
    }
}
