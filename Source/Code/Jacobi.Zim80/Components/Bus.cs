using System.Linq;
using System.Collections.Generic;

namespace Jacobi.Zim80.Components
{
    public class Bus<T> 
        where T : BusData, new()
    {
        private readonly List<BusMaster<T>> _masters = new List<BusMaster<T>>();
        private readonly List<BusSlave<T>>  _slaves = new List<BusSlave<T>>();

        public Bus()
        {
            Value = new T();
        }

        public string Name { get; set; }
        public T Value { get; private set; }

        public void Connect(BusMaster<T> busMaster)
        {
            if (!_masters.Contains(busMaster))
            {
                _masters.Add(busMaster);
                busMaster.OnChanged += MasterOnChanged;
            }
        }

        private void MasterOnChanged(object sender, BusChangedEventArgs<T> e)
        {
            ThrowIfMultipleMastersAreActive((BusMaster<T>)sender);
            Value = e.Value;
            foreach (var slave in _slaves)
                slave.Value = Value;
        }

        public void Connect(BusSlave<T> busSlave)
        {
            if (busSlave != null)
                _slaves.Add(busSlave);
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
