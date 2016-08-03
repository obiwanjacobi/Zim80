using System;

namespace Jacobi.Zim80
{
    public class DigitalSignalConsumer
    {
        public event EventHandler<DigitalLevelChangedEventArgs> OnChanged;

        private DigitalLevel _level;
        public DigitalLevel Level
        {
            get { return _level; }
            set
            {
                _level = value;
                NotifyChanged();
            }
        }

        private void NotifyChanged()
        {
            OnChanged?.Invoke(this, new DigitalLevelChangedEventArgs(Level));
        }
    }
}
