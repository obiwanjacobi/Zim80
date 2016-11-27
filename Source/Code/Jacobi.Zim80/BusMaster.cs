using System;
using System.Diagnostics;

namespace Jacobi.Zim80
{
    // writes values to bus signals
    [DebuggerDisplay("{Value} {Name}")]
    public class BusMaster<T> 
        where T : BusData, new()
    {
        public BusMaster()
        {
            Value = new T();
        }

        public BusMaster(string name)
            : this()
        {
            Name = name;
        }

        public BusMaster(Bus<T> bus, string name)
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
                        WriteInternal(new T());
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

        public T Value { get; private set; }

        private Bus<T> _bus;

        public Bus<T> Bus
        {
            get { return _bus; }
        }

        public virtual void ConnectTo(Bus<T> bus)
        {
            if (_bus != null)
                throw new InvalidOperationException(
                    "BusMaster is already connected.");

            _bus = bus;
            _bus.Attach(this);

            if (!Value.IsFloating)
            {
                _bus.OnMasterValueChanged(this);
            }
        }

        public void Write(T value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            ThrowIfNotEnabled();

            if (!IsConnected)
                Value = value;
            else
                WriteInternal(value);
        }

        public bool IsConnected
        {
            get { return _bus != null; }
        }

        private void WriteInternal(T value)
        {
            Value = value;
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
}
