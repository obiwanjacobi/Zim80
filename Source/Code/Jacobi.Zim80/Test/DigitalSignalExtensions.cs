using System;

namespace Jacobi.Zim80.Test
{
    public static class DigitalSignalExtensions
    {
        public static DigitalSignal ConnectTo(this DigitalSignalConsumer consumer, DigitalSignalProvider provider, string signalName = null)
        {
            DigitalSignal signal = Connect(provider, consumer);

            if (signal == null)
            {
                if (signalName == null) signalName = consumer.Name;
                signal = consumer.GetOrAddSignal(signalName);
                provider.ConnectTo(signal);
            }

            return signal;
        }

        public static DigitalSignal ConnectTo(this DigitalSignalProvider provider, DigitalSignalConsumer consumer, string signalName = null)
        {
            DigitalSignal signal = Connect(provider, consumer);

            if (signal == null)
            {
                if (signalName == null) signalName = provider.Name;
                signal = provider.GetOrAddSignal(signalName);
                consumer.ConnectTo(signal);
            }

            return signal;
        }

        private static DigitalSignal Connect(DigitalSignalProvider provider, DigitalSignalConsumer consumer)
        {
            if (consumer.IsConnected && provider.IsConnected)
            {
                if (consumer.DigitalSignal != provider.DigitalSignal)
                    throw new InvalidOperationException("Both Consumer and Provider are already connected to different signals.");

                return consumer.DigitalSignal;
            }

            DigitalSignal signal = null;

            if (consumer.IsConnected)
            {
                signal = consumer.DigitalSignal;
                provider.ConnectTo(signal);
            }
            else if (provider.IsConnected)
            {
                signal = provider.DigitalSignal;
                consumer.ConnectTo(signal);
            }

            return signal;
        }

        public static DigitalSignalProvider CreateConnection(this DigitalSignalConsumer consumer, string signalName = null)
        {
            var signal = consumer.GetOrAddSignal(signalName);
            return new DigitalSignalProvider(signal, signalName);
        }

        public static DigitalSignalConsumer CreateConnection(this DigitalSignalProvider provider, string signalName = null)
        {
            var signal = provider.GetOrAddSignal(signalName);
            return new DigitalSignalConsumer(signal, signalName);
        }

        public static DigitalSignal GetOrAddSignal(this DigitalSignalConsumer consumer, string signalName = null)
        {
            DigitalSignal signal = null;

            if (!consumer.IsConnected)
            {
                signal = new DigitalSignal(signalName);
                consumer.ConnectTo(signal);
            }
            else
            {
                signal = consumer.DigitalSignal;
            }

            return signal;
        }

        public static DigitalSignal GetOrAddSignal(this DigitalSignalProvider provider, string signalName = null)
        {
            DigitalSignal signal = null;

            if (!provider.IsConnected)
            {
                signal = new DigitalSignal(signalName);
                provider.ConnectTo(signal);
            }
            else
            {
                signal = provider.DigitalSignal;
            }

            return signal;
        }
    }
}
