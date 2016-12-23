using System;
using System.Diagnostics;

namespace Jacobi.Zim80
{
    public class DigitalLevelChangedEventArgs : EventArgs
    {
        [DebuggerStepThrough]
        public DigitalLevelChangedEventArgs(DigitalSignalProvider provider, DigitalLevel level)
        {
            Provider = provider;
            Level = level;
        }

        /// <summary>
        ///     CAN BE NULL!
        /// </summary>
        public DigitalSignalProvider Provider { get; private set; }

        public DigitalLevel Level { get; private set; }
    }
}
