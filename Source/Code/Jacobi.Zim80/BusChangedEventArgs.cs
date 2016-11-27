using System;

namespace Jacobi.Zim80
{
    public class BusChangedEventArgs<T> : EventArgs
        where T : BusData, new()
    {
        public BusChangedEventArgs(BusMaster<T> busMaster, T value)
        {
            BusMaster = busMaster;
            Value = value;
        }

        public BusMaster<T> BusMaster { get; private set; }

        public T Value { get; private set; }
    }
}
