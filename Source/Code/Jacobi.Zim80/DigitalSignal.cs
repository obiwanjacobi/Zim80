using System.Collections.Generic;
using System.Linq;

namespace Jacobi.Zim80
{
    public class DigitalSignal
    {
        public string Name { get; set; }
        public DigitalLevel Level { get; internal set; }

        public void Connect(DigitalSignalConsumer consumer)
        {
            if (!_consumers.Contains(consumer))
                _consumers.Add(consumer);
        }

        public void Connect(DigitalSignalProvider provider)
        {
            if (!_providers.Contains(provider))
                _providers.Add(provider);

            provider.OnChanged += ProviderOnChanged;
        }

        #region Private
        private readonly List<DigitalSignalConsumer> _consumers = new List<DigitalSignalConsumer>();
        private readonly List<DigitalSignalProvider> _providers = new List<DigitalSignalProvider>();

        private void ProviderOnChanged(object sender, DigitalLevelChangedEventArgs e)
        {
            ThrowIfMultipleProvidersActive((DigitalSignalProvider)sender);
            Level = e.Level;
            foreach (var consumer in _consumers)
                consumer.Level = Level;
        }

        private void ThrowIfMultipleProvidersActive(DigitalSignalProvider changeProvider)
        {
            if (AreMultipleProviderActive(changeProvider))
                throw new DigitalSignalConflictException("Multiple Digital Signal Providers are active on: " + Name);
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
