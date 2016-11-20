﻿using System;

namespace Jacobi.Zim80
{
    /// <summary>
    /// Provides new values to a DigitalSignal.
    /// Multiple instances can provide for a single DigitalSignal, 
    /// but only one instance can be active at a time.
    /// </summary>
    public class DigitalSignalProvider
    {
        public DigitalSignalProvider()
        { }

        public DigitalSignalProvider(string name)
        {
            Name = name;
        }

        public DigitalSignalProvider(DigitalSignal digitalSignal, string name)
        {
            if (digitalSignal == null)
                throw new ArgumentNullException(nameof(digitalSignal));

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
            if (_digitalSignal != null)
                throw new InvalidOperationException("This DigitalSignalProvider is already connected.");

            _digitalSignal = digitalSignal;
            _digitalSignal.Attach(this);

            if (String.IsNullOrEmpty(Name) &&
                !String.IsNullOrEmpty(_digitalSignal.Name))
            {
                Name = _digitalSignal.Name + " Provider";
            }

            // catch up with set level value
            if (Level != DigitalLevel.Floating)
            {
                var newLevel = Level;
                foreach (var level in new DigitalLevelCycler(
                    DigitalLevelCycler.NextLevel(_digitalSignal.Level), newLevel))
                {
                    Level = level;
                    _digitalSignal.OnNewProviderValue(this);
                }
            }
        }

        public string Name { get; set; }

        // provider level (not signal level)
        public DigitalLevel Level { get; private set; }

        public int Write(DigitalLevel newLevel)
        {
            if (!IsConnected)
            {
                Level = newLevel;
                return 0;
            }

            if (newLevel == DigitalLevel.Floating)
            {
                Level = newLevel;
                _digitalSignal.OnNewProviderValue(this);
                return 1;
            }

            var count = 0;
            foreach (var level in new DigitalLevelCycler(
                DigitalLevelCycler.NextLevel(_digitalSignal.Level), newLevel))
            {
                Level = level;
                _digitalSignal.OnNewProviderValue(this);
                count++;
            }

            return count;
        }

        public bool IsConnected
        {
            get { return _digitalSignal != null; }
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
