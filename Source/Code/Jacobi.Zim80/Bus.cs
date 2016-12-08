using System.Linq;
using System.Collections.Generic;
using System;
using System.Diagnostics;

namespace Jacobi.Zim80
{
    [DebuggerDisplay("{Name}: {Value}")]
    public class Bus : INamedObject
    {
        private readonly List<BusMaster> _masters = new List<BusMaster>();
        private readonly List<BusSlave> _slaves = new List<BusSlave>();
        private readonly List<DigitalSignal> _signals = new List<DigitalSignal>();

        public Bus(int busWidth)
        {
            if (busWidth <= 0)
                throw new ArgumentOutOfRangeException("busWidth", "Bus Width must be greater than zero.");

            for (int i = 0; i < busWidth; i++)
                _signals.Add(new DigitalSignal());
        }

        public Bus(int busWidth, string name)
            : this(busWidth)
        {
            Name = name;
        }

        public int BusWidth
        {
            get { return _signals.Count; }
        }

        public string Name { get; set; }

        private BusData _value;
        public BusData Value
        {
            get
            {
                if (_value == null)
                    _value = ValueFromSignals();

                return _value;
            }
            protected set
            {
                _value = value;
            }
        }

        public event EventHandler<BusChangedEventArgs<BusData>> OnChanged;

        public IEnumerable<BusMaster> Masters
        {
            get { return _masters; }
        }

        public IEnumerable<BusSlave> Slaves
        {
            get { return _slaves; }
        }

        public IEnumerable<DigitalSignal> Signals
        {
            get { return _signals; }
        }

        protected IList<DigitalSignal> SignalList
        {
            get { return _signals; }
        }

        protected virtual BusData NewBusData()
        {
            return new BusData(BusWidth);
        }

        internal void Attach(BusMaster busMaster) 
        {
            if (busMaster == null)
                throw new ArgumentNullException(nameof(busMaster));
            if (_masters.Contains(busMaster))
                throw new ArgumentException("Specified BusMaster is already connected.", nameof(busMaster));

            _masters.Add(busMaster);
        }

        internal void Attach(BusSlave busSlave)
        {
            if (busSlave == null)
                throw new ArgumentNullException(nameof(busSlave));
            if (_slaves.Contains(busSlave))
                throw new ArgumentException("Specified BusSlave is already connected.", nameof(busSlave));

            _slaves.Add(busSlave);
        }

        internal void OnMasterValueChanged(BusMaster busMaster)
        {
            ThrowIfMultipleMastersAreActive(busMaster);
            var value = busMaster.Value;
            if (!Value.Equals(value))
            {
                Value = value;
                ApplyValue(value);
                NotifyChange(busMaster);
            }
        }

        protected BusData ValueFromSignals()
        {
            var value = NewBusData();
            value.Write(_signals.Select((s) => s.Level));

            return value;
        }

        protected void ApplyValue(BusData value)
        {
            int index = 0;
            foreach (var level in value.Signals)
            {
                _signals[index].Level = level;
                index++;
            }
        }

        protected void NotifyChange(BusMaster source)
        {
            OnChanged?.Invoke(this, new BusChangedEventArgs<BusData>(source, Value));
        }

        private void ThrowIfMultipleMastersAreActive(BusMaster currentMaster)
        {
            if (AreMultipleMastersActive(currentMaster))
                throw new BusConflictException("Multiple masters are active on bus: " + Name);
        }

        private bool AreMultipleMastersActive(BusMaster currentMaster)
        {
            var floating = new BusData(BusWidth);

            return _masters
                .Where(m => !m.Value.Equals(floating))
                .Except(new[] { currentMaster })
                .Any();
        }
    }

    public class Bus<T> : Bus
        where T : BusData, new()
    {
        public Bus()
            : base(new T().Width)
        { }

        public Bus(string name)
            : base(new T().Width, name)
        { }

        public new T Value
        {
            get { return (T)base.Value; }
        }

        protected override BusData NewBusData()
        {
            return new T();
        }
    }
}
