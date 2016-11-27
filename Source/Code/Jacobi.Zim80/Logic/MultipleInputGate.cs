using System;
using System.Collections.Generic;
using System.Linq;

namespace Jacobi.Zim80.Logic
{
    public abstract class MultipleInputGate
    {
        private readonly List<DigitalSignalConsumer> _inputs = new List<DigitalSignalConsumer>();

        public IEnumerable<DigitalSignalConsumer> Inputs { get { return _inputs; } }

        public DigitalSignalConsumer ConnectInput(DigitalSignal inputSignal, string name = null)
        {
            var consumer = AddInput(name);

            consumer.ConnectTo(inputSignal);

            return consumer;
        }

        public DigitalSignalConsumer AddInput(string name = null)
        {
            var consumer = new DigitalSignalConsumer(name);
            _inputs.Add(consumer);

            consumer.OnChanged += Input_OnChanged;
            return consumer;
        }

        protected virtual void OnInputChanged(DigitalSignalConsumer input, DigitalSignalProvider source)
        { }

        protected void ThrowIfAnyInputsNotConnected()
        {
            if (Inputs.Any((c) => !c.IsConnected))
                throw new InvalidOperationException("Not all inputs are connected to a signal.");
        }

        private void Input_OnChanged(object sender, DigitalLevelChangedEventArgs e)
        {
            OnInputChanged((DigitalSignalConsumer)sender, e.Provider);
        }
    }
}
