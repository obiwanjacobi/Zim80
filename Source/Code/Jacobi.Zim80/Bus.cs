using System.Linq;
using System.Collections.Generic;
using System;

namespace Jacobi.Zim80
{
    public class Bus<T> : INamedObject
        where T : BusData, new()
    {
        private readonly List<BusMaster<T>> _masters = new List<BusMaster<T>>();
        private readonly List<BusSlave<T>> _slaves = new List<BusSlave<T>>();
        private readonly List<DigitalSignal> _signals = new List<DigitalSignal>();

        public Bus()
        {
            var width = new T().Width;

            for (int i = 0; i < width; i++)
            {
                _signals.Add(new DigitalSignal());
            }
        }

        public Bus(string name)
            : this()
        {
            Name = name;
        }

        public string Name { get; set; }

        private T _value;
        public T Value
        {
            get
            {
                if (_value == null)
                {
                    _value = new T();
                    _value.Write(_signals.Select((s) => s.Level));
                }

                return _value;
            }
        }

        public event EventHandler<BusChangedEventArgs<T>> OnChanged;

        public IEnumerable<BusMaster<T>> Masters
        {
            get { return _masters; }
        }

        public IEnumerable<BusSlave<T>> Slaves
        {
            get { return _slaves; }
        }

        internal void Attach(BusMaster<T> busMaster)
        {
            if (busMaster == null)
                throw new ArgumentNullException(nameof(busMaster));
            if (_masters.Contains(busMaster))
                throw new ArgumentException("Specified BusMaster is already connected.", nameof(busMaster));

            _masters.Add(busMaster);
        }

        internal void Attach(BusSlave<T> busSlave)
        {
            if (busSlave == null)
                throw new ArgumentNullException(nameof(busSlave));
            if (_slaves.Contains(busSlave))
                throw new ArgumentException("Specified BusSlave is already connected.", nameof(busSlave));

            _slaves.Add(busSlave);
        }

        internal void OnMasterValueChanged(BusMaster<T> busMaster)
        {
            ThrowIfMultipleMastersAreActive(busMaster);
            var value = busMaster.Value;
            if (!Value.Equals(value))
            {
                _value = value;
                ApplyValue(value);
                NotifyChange(busMaster);
            }
        }

        private void ApplyValue(T value)
        {
            int index = 0;
            foreach(var level in value.Signals)
            {
                _signals[index].Level = level;
                index++;
            }
        }

        private void NotifyChange(BusMaster<T> source)
        {
            OnChanged?.Invoke(this, new BusChangedEventArgs<T>(source, Value));
        }

        private void ThrowIfMultipleMastersAreActive(BusMaster<T> currentMaster)
        {
            if (AreMultipleMastersActive(currentMaster))
                throw new BusConflictException("Multiple masters are active on bus: " + Name);
        }

        private bool AreMultipleMastersActive(BusMaster<T> currentMaster)
        {
            var floating = new T();

            return _masters
                .Where(m => !m.Value.Equals(floating))
                .Except(new[] { currentMaster })
                .Any();
        }
    }
}
