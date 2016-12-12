using System;

namespace Jacobi.Zim80
{
    /// <summary>
    /// Provides new values to a DigitalSignal.
    /// Multiple instances can provide for a single DigitalSignal, 
    /// but only one instance can be active at a time.
    /// </summary>
    public class DigitalSignalProvider : INamedObject
    {
        public DigitalSignalProvider()
        { }

        public DigitalSignalProvider(string name)
        {
            Name = name;
        }

        public DigitalSignalProvider(DigitalSignal digitalSignal, string name)
        {
            Name = name;
            ConnectTo(digitalSignal);
        }

        private DigitalSignal _digitalSignal;

        public DigitalSignal DigitalSignal
        {
            get { return _digitalSignal; }
        }

        public void ConnectTo(DigitalSignal digitalSignal)
        {
            if (digitalSignal == null)
                throw new ArgumentNullException(nameof(digitalSignal));
            if (_digitalSignal != null)
                throw new InvalidOperationException("This DigitalSignalProvider is already connected.");

            _digitalSignal = digitalSignal;
            _digitalSignal.Attach(this);

            // catch up with set level value
            if (Level != DigitalLevel.Floating)
            {
                SetNewLevel(Level);
            }
        }

        private string _name;

        public string Name
        {
            get
            {
                if (IsConnected && _name == null)
                    return _digitalSignal.Name;
                return _name;
            }
            set
            { _name = value; }
        }

        // provider level (not signal level)
        public DigitalLevel Level { get; private set; }

        public int Write(DigitalLevel newLevel)
        {
            if (!IsConnected)
            {
                Level = newLevel;
                return 0;
            }

            return SetNewLevel(newLevel);
        }

        public bool IsConnected
        {
            get { return _digitalSignal != null; }
        }

        private int SetNewLevel(DigitalLevel newLevel)
        {
            if (DigitalSignal.Level == newLevel)
            {
                return 0;
            }

            if (newLevel == DigitalLevel.Floating)
            {
                Level = newLevel;
                _digitalSignal.OnNewProviderValue(this);
                return 1;
            }

            var nextLevel = DigitalLevelCycler.NextLevel(_digitalSignal.Level);
            if (nextLevel == newLevel)
            {
                Level = newLevel;
                _digitalSignal.OnNewProviderValue(this);
                return 1;
            }

            int count = 0;
            foreach (var level in new DigitalLevelCycler(nextLevel, newLevel))
            {
                Level = level;
                _digitalSignal.OnNewProviderValue(this);
                count++;
            }

            return count;
        }

        private void ThrowIfNotConnected()
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException(
                    "DigitalProvider is not connected to a DigitalSignal.");
            }
        }
    }
}
