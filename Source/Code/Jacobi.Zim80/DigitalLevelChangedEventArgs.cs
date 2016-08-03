using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jacobi.Zim80
{
    public class DigitalLevelChangedEventArgs : EventArgs
    {
        public DigitalLevelChangedEventArgs(DigitalLevel level)
        {
            Level = level;
        }

        public DigitalLevel Level { get; private set; }
    }
}
