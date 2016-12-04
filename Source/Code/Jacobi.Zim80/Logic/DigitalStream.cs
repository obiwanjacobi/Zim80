using System.Collections.Generic;

namespace Jacobi.Zim80.Logic
{
    public class DigitalStream
    {
        private readonly List<DigitalLevel> _samples = new List<DigitalLevel>();
        private readonly DigitalSignal _signal;

        public DigitalStream(DigitalSignal signal)
        {
            _signal = signal;
        }

        public DigitalSignal DigitalSignal { get { return _signal; } }

        public IEnumerable<DigitalLevel> Samples { get { return _samples; } }

        public void Sample()
        {
            _samples.Add(_signal.Level);
        }

        public void Clear()
        {
            _samples.Clear();
        }

        internal void AddFloating()
        {
            _samples.Add(DigitalLevel.Floating);
        }
    }
}
