namespace Jacobi.Zim80.UnitTests
{
    internal static class DigitalSignalTestExtensions
    {
        public static DigitalSignalProvider CreateConnection(this DigitalSignalConsumer consumer, DigitalSignalProvider provider = null, string signalName = null)
        {
            if (signalName == null && consumer.Name != null)
                signalName = consumer.Name;

            var signal = new DigitalSignal(signalName);
            if (provider == null)
                provider = new DigitalSignalProvider();

            consumer.ConnectTo(signal);
            provider.ConnectTo(signal);
            return provider;
        }

        public static DigitalSignalConsumer CreateConnection(this DigitalSignalProvider provider, DigitalSignalConsumer consumer = null, string signalName = null)
        {
            if (signalName == null && provider.Name != null)
                signalName = provider.Name;

            var signal = new DigitalSignal(signalName);
            if (consumer == null)
                consumer = new DigitalSignalConsumer();

            consumer.ConnectTo(signal);
            provider.ConnectTo(signal);
            return consumer;
        }
    }
}
