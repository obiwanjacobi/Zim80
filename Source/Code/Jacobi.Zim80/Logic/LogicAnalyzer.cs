using System;
using System.Collections.Generic;

namespace Jacobi.Zim80.Logic
{
    public class LogicAnalyzer : MultipleInputGate
    {
        private readonly List<BusSlave> _busInputs = new List<BusSlave>();
        private readonly List<BusDataStream> _busStreams = new List<BusDataStream>();
        private readonly List<DigitalStream> _digitalStreams = new List<DigitalStream>();
        private readonly DigitalSignalConsumer _clock;

        public LogicAnalyzer()
        {
            _clock = new DigitalSignalConsumer("Clock");
            _clock.OnChanged += Clock_OnChanged;
        }

        // used for sampling
        public DigitalSignalConsumer Clock { get { return _clock; } }

        public bool IsRunning { get; private set; }

        public TriggerMode TriggerMode { get; set; }

        public IEnumerable<DigitalStream> SignalStreams { get { return _digitalStreams; } }

        public IEnumerable<BusDataStream> BusStreams { get { return _busStreams; } }

        public IEnumerable<BusSlave> BusInputs { get { return _busInputs; } }

        public BusSlave ConnectInput(Bus busInput, string name = null)
        {
            var slave = AddBusInput(name);

            slave.ConnectTo(busInput);

            return slave;
        }

        public BusSlave AddBusInput(string name = null)
        {
            var slave = new BusSlave(name);
            _busInputs.Add(slave);

            return slave;
        }

        public void Start()
        {
            InitializeStreams();
            AddFloating();
            IsRunning = true;
        }

        public void Stop()
        {
            IsRunning = false;
            AddFloating();
        }

        private void InitializeStreams()
        {
            if (!_clock.IsConnected)
                throw new InvalidOperationException("The Clock has not been connected.");

            _digitalStreams.Clear();
            _digitalStreams.Add(new Logic.DigitalStream(_clock.DigitalSignal));

            foreach (var input in Inputs)
            {
                if (!input.IsConnected) continue;
                _digitalStreams.Add(new DigitalStream(input.DigitalSignal));
            }

            _busStreams.Clear();

            foreach (var bus in _busInputs)
            {
                if (!bus.IsConnected) continue;
                _busStreams.Add(new Logic.BusDataStream(bus.Bus));
            }
        }

        private void AddFloating()
        {
            foreach (var stream in _digitalStreams)
            {
                stream.AddFloating();
            }

            foreach (var stream in _busStreams)
            {
                stream.AddFloating();
            }
        }

        private void Clock_OnChanged(object sender, DigitalLevelChangedEventArgs e)
        {
            if (IsRunning && IsTriggered(e.Level))
            {
                SampleInputs();
            }
        }

        private void SampleInputs()
        {
            foreach (var stream in _digitalStreams)
            {
                stream.Sample();
            }

            foreach (var stream in _busStreams)
            {
                stream.Sample();
            }
        }

        private bool IsTriggered(DigitalLevel level)
        {
            switch (TriggerMode)
            {
                case TriggerMode.Edge:
                    return level == DigitalLevel.NegEdge || level == DigitalLevel.PosEdge;
                case TriggerMode.Level:
                    return level == DigitalLevel.Low || level == DigitalLevel.High;
                case TriggerMode.All:
                    return level != DigitalLevel.Floating;
            }

            return false;
        }
    }

    public enum TriggerMode
    {
        Edge,
        Level,
        All
    }
}
