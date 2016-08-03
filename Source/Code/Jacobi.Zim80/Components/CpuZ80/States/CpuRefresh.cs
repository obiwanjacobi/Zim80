namespace Jacobi.Zim80.Components.CpuZ80.States
{
    internal abstract class CpuRefresh : CpuState
    {
        public CpuRefresh(ExecutionEngine executionEngine)
            : base(executionEngine)
        { }

        protected override void OnClockPos()
        {
            if (!ExecutionEngine.Cycles.IsMachineCycle1) return;

            switch (ExecutionEngine.Cycles.CycleName)
            {
                case CycleNames.T1:
                    ExecutionEngine.Die.Refresh.Write(DigitalLevel.High);
                    break;

                case CycleNames.T3:
                    ExecutionEngine.SetRefreshOnAddressBus();
                    ExecutionEngine.Die.Refresh.Write(DigitalLevel.Low);
                    break;
            }
        }

        protected override void OnClockNeg()
        {
            if (!ExecutionEngine.Cycles.IsMachineCycle1) return;

            switch (ExecutionEngine.Cycles.CycleName)
            {
                case CycleNames.T3:
                    ExecutionEngine.Die.MemoryRequest.Write(DigitalLevel.Low);
                    break;
                case CycleNames.T4:
                    ExecutionEngine.Die.MemoryRequest.Write(DigitalLevel.High);
                    break;
            }
        }
    }
}
