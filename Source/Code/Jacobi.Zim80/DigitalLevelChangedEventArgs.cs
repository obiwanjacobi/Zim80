using System;

namespace Jacobi.Zim80
{
    public class DigitalLevelChangedEventArgs : EventArgs
    {
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
