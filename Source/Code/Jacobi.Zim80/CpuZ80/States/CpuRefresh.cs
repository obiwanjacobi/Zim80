namespace Jacobi.Zim80.CpuZ80.States
{
    internal abstract class CpuRefresh : CpuState
    {
        public CpuRefresh(Die die)
            : base(die)
        {
            RefreshEnabled = true;
        }

        protected bool RefreshEnabled { get; set; }

        protected bool IsEnabled
        {
            get { return RefreshEnabled && ExecutionEngine.Cycles.IsMachineCycle1; }
        }

        protected override void OnClockPos()
        {
            switch (ExecutionEngine.Cycles.CycleName)
            {
                case CycleNames.T1:
                    // always reset refresh on T1.
                    Die.Refresh.Write(DigitalLevel.High);
                    break;

                case CycleNames.T3:
                    if (IsEnabled)
                    {
                        ExecutionEngine.SetRefreshOnAddressBus();
                        Die.Refresh.Write(DigitalLevel.Low);
                    }
                    break;
            }
        }

        protected override void OnClockNeg()
        {
            if (!IsEnabled) return;

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
