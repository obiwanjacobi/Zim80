using System;

namespace Jacobi.Zim80.Components
{
    // reads bus signal values
    public class BusSlave<T> 
        where T : BusData, new()
    {
        public BusSlave()
        {
            Value = new T();
        }

        public event EventHandler<BusChangedEventArgs<T>> OnChanged;

        private T _value;
        public T Value
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    NotifyChange();
                }
            }
        }

        private void NotifyChange()
        {
            OnChanged?.Invoke(this, new BusChangedEventArgs<T>(Value));
        }
    }
}
