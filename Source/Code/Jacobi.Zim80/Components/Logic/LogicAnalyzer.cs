using System.Collections.Generic;

namespace Jacobi.Zim80.Components.Logic
{
    public class LogicAnalyzer : MultipleInputGate
    {
        private readonly List<DigitalStream> _streams = new List<DigitalStream>();
        private readonly DigitalSignalConsumer _clock;
        private DigitalLevel _trigger;

        public LogicAnalyzer()
        {
            _clock = new DigitalSignalConsumer();
            _clock.OnChanged += Clock_OnChanged;
        }

        public bool IsRunning { get; private set; }

        public IEnumerable<DigitalStream> Streams { get { return _streams; } }

        public void Start(DigitalLevel trigger = DigitalLevel.PosEdge)
        {
            InitializeStreams();
            _trigger = trigger;
            IsRunning = true;
        }

        private void InitializeStreams()
        {
            _streams.Clear();

            foreach (var input in Inputs)
            {
                if (!input.IsConnected) continue;
                _streams.Add(new DigitalStream(input.DigitalSignal));
            }
        }

        public void Stop()
        {
            IsRunning = false;
        }

        private void Clock_OnChanged(object sender, DigitalLevelChangedEventArgs e)
        {
            if (IsRunning && 
                _trigger == e.Level)
            {
                SampleInputs();
            }
        }

        private void SampleInputs()
        {
            foreach (var stream in _streams)
            {
                stream.Sample();
            }
        }
    }
}
