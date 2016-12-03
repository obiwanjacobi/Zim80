using System;
using System.Diagnostics;

namespace Jacobi.Zim80
{
    // reads bus signal values
    [DebuggerDisplay("{Name}: {Value}")]
    public class BusSlave : INamedObject
    {
        public BusSlave()
        { }

        public BusSlave(string name)
        {
            Name = name;
        }

        public BusSlave(Bus bus)
        {
            ConnectTo(bus);
        }

        public BusSlave(Bus bus, string name)
        {
            Name = name;
            ConnectTo(bus);
        }

        public event EventHandler<BusChangedEventArgs<BusData>> OnChanged;

        private string _name;

        public string Name
        {
            get
            {
                if (IsConnected && _name == null)
                    return _bus.Name;
                return _name;
            }
            set
            { _name = value; }
        }

        public BusData Value
        {
            get
            {
                if (!IsConnected)
                    return NewBusData();

                return _bus.Value;
            }
        }

        private Bus _bus;

        public Bus Bus
        {
            get { ThrowIfNotConnected(); return _bus; }
        }

        public virtual void ConnectTo(Bus bus)
        {
            ConnectToInternal(bus);
        }

        protected void ConnectToInternal(Bus bus)
        {
            if (bus == null)
                throw new ArgumentNullException(nameof(bus));
            if (IsConnected)
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

        protected virtual BusData NewBusData()
        {
            return new BusData(0);
        }

        protected virtual void OnValueChanged(BusChangedEventArgs<BusData> e)
        {
            OnChanged?.Invoke(this, e);
        }

        private void Bus_OnChanged(object sender, BusChangedEventArgs<BusData> e)
        {
            OnValueChanged(e);
        }

        private void ThrowIfNotConnected()
        {
            if (!IsConnected)
                throw new InvalidOperationException("The BusSlave is not connected.");
        }
    }

    public class BusSlave<T> : BusSlave
        where T : BusData, new()
    {
        public BusSlave()
        { }

        public BusSlave(string name)
            : base(name)
        { }

        public BusSlave(Bus<T> bus)
            : base(bus)
        { }

        public BusSlave(Bus<T> bus, string name)
            : base(bus, name)
        { }

        public new Bus<T> Bus
        {
            get { return (Bus<T>)base.Bus; }
        }

        public new T Value
        {
            get { return (T)base.Value; }
        }

        public virtual void ConnectTo(Bus<T> bus)
        {
            base.ConnectTo(bus);
        }

        protected override BusData NewBusData()
        {
            return new T();
        }
    }
}
