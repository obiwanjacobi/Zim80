using System;
using System.Linq;

namespace Jacobi.Zim80
{
    // taps off specific bus signals
    public class BusTap : Bus
    {
        private BusData _prevValue;

        public BusTap(int busWidth)
            : base(busWidth)
        { }

        private Bus _sourceBus;

        public Bus SourceBus
        {
            get { return _sourceBus; }
            protected set
            {
                _sourceBus = value;

                if (_sourceBus != null)
                    _sourceBus.OnChanged += SourceBus_OnChanged;
            }
        }

        public void ConnectTo(Bus bus, int offset = 0, int count = 0)
        {
            ThrowIfBusWidthIsTooSmall(bus);

            SourceBus = bus;

            if (count == 0) count = BusWidth;

            SignalList.Clear();
            var signals = bus.Signals.Skip(offset).Take(count);
            foreach (var signal in signals)
                SignalList.Add(signal);

            _prevValue = ValueFromSignals();
        }

        public void ConnectToByIndex(Bus bus, params int[] indexes)
        {
            ThrowIfBusWidthIsTooSmall(bus);

            SourceBus = bus;

            SignalList.Clear();
            foreach (var index in indexes)
                SignalList.Add(SignalList[index]);

            _prevValue = ValueFromSignals();
        }

        private void SourceBus_OnChanged(object sender, BusChangedEventArgs<BusData> e)
        {
            var newValue = ValueFromSignals();

            if (!_prevValue.Equals(newValue))
            {
                Value = newValue;
                NotifyChange(e.BusMaster);
            }
        }

        private void ThrowIfBusWidthIsTooSmall(Bus bus)
        {
            if (bus.BusWidth < BusWidth)
                throw new ArgumentException("The BusWith is too small.", nameof(bus));
        }
    }

    // taps off specific bus signals
    public class BusTap<T> : BusTap
        where T : BusData, new()
    {
        public BusTap()
            : base(new T().Width)
        { }

        public new Bus<T> SourceBus
        {
            get { return (Bus<T>)base.SourceBus; }
        }

        public void ConnectTo(Bus<T> bus, int offset = 0, int count = 0)
        {
            base.ConnectTo(bus, offset, count);
        }

        public void ConnectToByIndex(Bus<T> bus, params int[] indexes)
        {
            base.ConnectToByIndex(bus, indexes);
        }
    }
}
