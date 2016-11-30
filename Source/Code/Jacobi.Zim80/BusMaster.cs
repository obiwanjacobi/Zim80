using System;
using System.Diagnostics;

namespace Jacobi.Zim80
{
    // writes values to bus signals
    [DebuggerDisplay("{Name}: {Value}")]
    public class BusMaster : INamedObject
    {
        public BusMaster()
        {
            Value = NewBusData();
        }

        public BusMaster(string name)
            : this()
        {
            Name = name;
        }

        public BusMaster(Bus bus)
            : this()
        {
            ConnectTo(bus);
        }

        public BusMaster(Bus bus, string name)
            : this()
        {
            Name = name;
            ConnectTo(bus);
        }

        private bool _isEnabled;

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;

                    if (!_isEnabled)
                        Value = NewBusData();
                }
            }
        }

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

        public BusData Value { get; protected set; }

        private Bus _bus;

        public Bus Bus
        {
            get { ThrowIfNotConnected(); return _bus; }
        }

        public virtual void ConnectTo(Bus bus)
        {
            if (bus == null)
                throw new ArgumentNullException(nameof(bus));
            if (IsConnected)
                throw new InvalidOperationException(
                    "BusMaster is already connected.");

            _bus = bus;
            _bus.Attach(this);

            if (!Value.IsFloating)
            {
                _bus.OnMasterValueChanged(this);
            }
        }

        public void Write(BusData value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            ThrowIfNotEnabled();

            WriteInternal(value);
        }

        public bool IsConnected
        {
            get { return _bus != null; }
        }

        protected virtual BusData NewBusData()
        {
            return new BusData(0);
        }

        private void WriteInternal(BusData value)
        {
            Value = value;

            if (IsConnected)
                _bus.OnMasterValueChanged(this);
        }

        private void ThrowIfNotConnected()
        {
            if (!IsConnected)
                throw new InvalidOperationException(
                    "BusMaster is not connected.");
        }

        private void ThrowIfNotEnabled()
        {
            if (!IsEnabled)
                throw new InvalidOperationException(
                    "BusMaster is not enabled.");
        }
    }

    
    public class BusMaster<T> : BusMaster
        where T : BusData, new()
    {
        public BusMaster()
        { }

        public BusMaster(string name)
            : base(name)
        { }

        public BusMaster(Bus<T> bus)
            : base(bus)
        { }

        public BusMaster(Bus<T> bus, string name)
            : base(bus, name)
        { }

        public new T Value
        {
            get { return (T)base.Value; }
        }

        public new Bus<T> Bus
        {
            get { return (Bus<T>)base.Bus; }
        }

        public virtual void ConnectTo(Bus<T> bus)
        {
            base.ConnectTo(bus);
        }

        public void Write(T value)
        {
            base.Write(value);
        }

        protected override BusData NewBusData()
        {
            return new T();
        }
    }
}
