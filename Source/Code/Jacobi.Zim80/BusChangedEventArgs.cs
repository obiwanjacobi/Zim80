using System;

namespace Jacobi.Zim80
{
    public class BusChangedEventArgs : EventArgs
    {
        public BusChangedEventArgs(BusMaster busMaster, BusData value)
        {
            BusMaster = busMaster;
            Value = value;
        }

        public BusMaster BusMaster { get; private set; }

        public BusData Value { get; private set; }
    }

    public class BusChangedEventArgs<T> : EventArgs
        where T : BusData
    {
        public BusChangedEventArgs(BusMaster busMaster, T value)
        {
            BusMaster = busMaster;
            Value = value;
        }

        public BusMaster BusMaster { get; private set; }

        public T Value { get; private set; }
    }
}
