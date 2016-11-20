using System;

namespace Jacobi.Zim80.Components
{
    // reads bus signal values
    public class BusSlave<T> 
        where T : BusData, new()
    {
        public BusSlave()
        { }

        public BusSlave(string name)
        {
            Name = name;
        }

        public BusSlave(Bus<T> bus, string name)
        {
            Name = name;
            ConnectTo(bus);
        }

        public event EventHandler<BusChangedEventArgs<T>> OnChanged;

        public string Name { get; set; }

        public T Value
        {
            get
            {
                if (!IsConnected) return new T();

                return _bus.Value;
            }
        }

        private Bus<T> _bus;

        public Bus<T> Bus
        {
            get { ThrowIfNotConnected(); return _bus; }
        }

        public void ConnectTo(Bus<T> bus)
        {
            if (_bus != null)
                throw new InvalidOperationException(
                    "This BusSlave is already connected.");

            _bus = bus;
            _bus.Attach(this);
            _bus.OnChanged += Bus_OnChanged;
        }

        public bool IsConnected
        {
            get { return _bus != null; }
        }

        protected virtual void OnValueChanged(BusChangedEventArgs<T> e)
        {
            OnChanged?.Invoke(this, e);
        }

        private void Bus_OnChanged(object sender, BusChangedEventArgs<T> e)
        {
            OnValueChanged(e);
        }

        private void ThrowIfNotConnected()
        {
            if (!IsConnected)
                throw new InvalidOperationException("The BusSlave is not connected.");
        }
    }
}
