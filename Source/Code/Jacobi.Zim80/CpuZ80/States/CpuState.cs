namespace Jacobi.Zim80.CpuZ80.States
{
    internal abstract class CpuState
    {
        private CpuZ80 _cpu;

        public CpuState(CpuZ80 cpu)
        {
            _cpu = cpu;
        }

        protected CpuZ80 Cpu { get { return _cpu; } }

        protected ExecutionEngine ExecutionEngine { get { return _cpu.Engine; } }

        public bool IsComplete { get; protected set; }

        public virtual void OnClock(DigitalLevel level)
        {
            switch (level)
            {
                case DigitalLevel.PosEdge:
                    OnClockPos();
                    break;
                case DigitalLevel.High:
                    OnClockHigh();
                    break;
                case DigitalLevel.NegEdge:
                    OnClockNeg();
                    break;
                case DigitalLevel.Low:
                    OnClockLow();
                    break;
            }
        }

        protected virtual void OnClockLow()
        { }

        protected virtual void OnClockHigh()
        { }

        protected virtual void OnClockPos()
        { }

        protected virtual void OnClockNeg()
        { }
    }
}
