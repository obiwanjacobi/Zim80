using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jacobi.Zim80.Components
{
    public class BusChangedEventArgs<T> : EventArgs
        where T : BusData
    {
        public BusChangedEventArgs(T value)
        {
            Value = value;
        }

        public T Value { get; private set; }
    }
}
