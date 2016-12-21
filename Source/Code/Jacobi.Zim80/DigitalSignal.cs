using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Jacobi.Zim80
{
    /// <summary>
    /// Maintains the state (Level) of a digital signal (net).
    /// </summary>
    [DebuggerDisplay("{Level} {Name}")]
    public class DigitalSignal : INamedObject
    {
        public DigitalSignal()
        {
            _level = DigitalLevel.Floating;
        }

        public DigitalSignal(string name)
            : this()
        {
            Name = name;
        }

        public string Name { get; set; }

        private DigitalLevel _level;
        public DigitalLevel Level
        {
            get { return _level; }
            set
            {
                _level = value;
                NotifyOnChanged(null);
            }
        }

        public event EventHandler<DigitalLevelChangedEventArgs> OnChanged;

        public IEnumerable<DigitalSignalConsumer> Consumers
        {
            get { return _consumers; }
        }

        public IEnumerable<DigitalSignalProvider> Providers
        {
            get { return _providers; }
        }

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
            ThrowIfNotOurs(provider);
            ThrowIfMultipleProvidersActive(provider);

            if (Level != provider.Level)
            {
                _level = provider.Level;
                NotifyOnChanged(provider);
            }
        }

        #region Private
        private readonly List<DigitalSignalConsumer> _consumers = new List<DigitalSignalConsumer>();
        private readonly List<DigitalSignalProvider> _providers = new List<DigitalSignalProvider>();

        private void NotifyOnChanged(DigitalSignalProvider provider)
        {
            OnChanged?.Invoke(this, new DigitalLevelChangedEventArgs(provider, Level));
        }

        [Conditional("DEBUG")]
        private void ThrowIfNotOurs(DigitalSignalProvider provider)
        {
            if (!_providers.Contains(provider))
                throw new ArgumentException("Specified Provider is not connected to this DigitalSignal.", nameof(provider));
        }

        [Conditional("DEBUG")]
        private void ThrowIfMultipleProvidersActive(DigitalSignalProvider changeProvider)
        {
            if (AreMultipleProvidersActive(changeProvider))
                throw new DigitalSignalConflictException("Multiple DigitalSignalProviders are active on: " + Name);
        }

        private bool AreMultipleProvidersActive(DigitalSignalProvider changeProvider)
        {
            return _providers
                .Where(p => p.Level != DigitalLevel.Floating)
                .Except(new[] { changeProvider })
                .Any();
        } 
        #endregion
    }
}
