using System;

namespace Jacobi.Zim80
{
    public class DigitalSignalProvider
    {
        public event EventHandler<DigitalLevelChangedEventArgs> OnChanged;

        public DigitalLevel Level { get; private set; }

        public void Write(DigitalLevel level)
        {
            if (Level != level)
            {
                Level = level;
                OnChanged?.Invoke(this, new DigitalLevelChangedEventArgs(level));
            }
        }
    }
}
