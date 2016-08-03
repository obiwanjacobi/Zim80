namespace Jacobi.Zim80.UnitTests
{
    internal static class DigitalSignalTestExtensions
    {
        public static DigitalSignalProvider CreateConnection(this DigitalSignalConsumer consumer, DigitalSignalProvider provider = null)
        {
            var signal = new DigitalSignal();
            if (provider == null)
                provider = new DigitalSignalProvider();

            signal.Connect(consumer);
            signal.Connect(provider);
            return provider;
        }

        public static DigitalSignalConsumer CreateConnection(this DigitalSignalProvider provider, DigitalSignalConsumer consumer = null)
        {
            var signal = new DigitalSignal();
            if (consumer == null)
                consumer = new DigitalSignalConsumer();

            signal.Connect(consumer);
            signal.Connect(provider);
            return consumer;
        }
    }
}
