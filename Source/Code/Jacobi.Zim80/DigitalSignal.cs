using System;
using System.Collections.Generic;
using System.Linq;

namespace Jacobi.Zim80
{
    /// <summary>
    /// Maintains the state (Level) of a digital signal (net).
    /// </summary>
    public class DigitalSignal
    {
        public DigitalSignal()
        {
            Level = DigitalLevel.Floating;
        }

        public DigitalSignal(string name)
            : this()
        {
            Name = name;
        }

        public string Name { get; set; }

        public DigitalLevel Level { get; private set; }

        public event EventHandler<DigitalLevelChangedEventArgs> OnChanged;

        internal void Attach(DigitalSignalConsumer consumer)
        {
            if (_consumers.Contains(consumer))
                throw new ArgumentException(
                    "Specified Consumer is already connected.", nameof(consumer));

                _consumers.Add(consumer);
        }

        internal void Attach(DigitalSignalProvider provider)
        {
            if (_providers.Contains(provider))
                throw new ArgumentException(
                    "Specified Provider is already connected.", nameof(provider));

            _providers.Add(provider);
        }

        internal void OnNewProviderValue(DigitalSignalProvider provider)
        {
            if (!_providers.Contains(provider))
                throw new ArgumentException("Specified Provider is not connected to this DigitalSignal.", nameof(provider));

            ThrowIfMultipleProvidersActive(provider);
            if (Level != provider.Level)
            {
                Level = provider.Level;
                OnChanged?.Invoke(this, new DigitalLevelChangedEventArgs(provider, Level));
            }
        }

        #region Private
        private readonly List<DigitalSignalConsumer> _consumers = new List<DigitalSignalConsumer>();
        private readonly List<DigitalSignalProvider> _providers = new List<DigitalSignalProvider>();

        private void ThrowIfMultipleProvidersActive(DigitalSignalProvider changeProvider)
        {
            if (AreMultipleProviderActive(changeProvider))
                throw new DigitalSignalConflictException("Multiple DigitalSignalProviders are active on: " + Name);
        }

        private bool AreMultipleProviderActive(DigitalSignalProvider changeProvider)
        {
            return _providers
                .Where(p => p.Level != DigitalLevel.Floating)
                .Except(new[] { changeProvider })
                .Any();
        } 
        #endregion
    }
}
