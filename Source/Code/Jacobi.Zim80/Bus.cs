using System.Linq;
using System.Collections.Generic;
using System;

namespace Jacobi.Zim80
{
    public class Bus<T>
        where T : BusData, new()
    {
        private readonly List<BusMaster<T>> _masters = new List<BusMaster<T>>();
        private readonly List<BusSlave<T>> _slaves = new List<BusSlave<T>>();

        public Bus()
        {
            Value = new T();
        }

        public Bus(string name)
            : this()
        {
            Name = name;
        }

        public string Name { get; set; }

        public T Value { get; private set; }

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
                Value = value;
                NotifyChange(busMaster);
            }
        }

        private void NotifyChange(BusMaster<T> busMaster)
        {
            OnChanged?.Invoke(this, new BusChangedEventArgs<T>(busMaster, Value));
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
