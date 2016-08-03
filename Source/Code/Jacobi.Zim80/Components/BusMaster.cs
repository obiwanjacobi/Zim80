using System;

namespace Jacobi.Zim80.Components
{
    // writes values to bus signals
    public class BusMaster<T> 
        where T : BusData, new()
    {
        public BusMaster()
        {
            Value = new T();
        }

        public event EventHandler<BusChangedEventArgs<T>> OnChanged;

        private bool _isEnabled;

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                if (!_isEnabled)
                    WriteInternal(new T());
            }
        }

        public T Value { get; private set; }

        public void Write(T value)
        {
            if (value == null) throw new ArgumentNullException("value");

            ThrowIfNotEnabled();
            WriteInternal(value);
        }

        protected virtual void WriteInternal(T value)
        {
            Value = value;
            OnChanged?.Invoke(this, new BusChangedEventArgs<T>(value));
        }

        private void ThrowIfNotEnabled()
        {
            if (!IsEnabled)
                throw new InvalidOperationException("The BusMaster is not enabled.");
        }
    }
}
