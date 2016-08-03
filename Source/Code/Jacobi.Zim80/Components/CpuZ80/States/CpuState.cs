namespace Jacobi.Zim80.Components.CpuZ80.States
{
    internal abstract class CpuState
    {
        private ExecutionEngine _execEngine;

        public CpuState(ExecutionEngine executionEngine)
        {
            _execEngine = executionEngine;
        }

        protected ExecutionEngine ExecutionEngine { get { return _execEngine; } }

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
