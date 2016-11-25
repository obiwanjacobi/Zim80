using System.Collections.Generic;

namespace Jacobi.Zim80.Components.Logic
{
    public class Buffer 
    {
        private readonly List<DigitalSignalConsumer> _inputs = new List<DigitalSignalConsumer>();
        private readonly List<DigitalSignalProvider> _outputs = new List<DigitalSignalProvider>();
        private readonly DigitalSignalConsumer _outputEnable;

        public Buffer()
        {
            _outputEnable = new Zim80.DigitalSignalConsumer("/OE");
            _outputEnable.OnChanged += OutputEnable_OnChanged;
        }

        public IEnumerable<DigitalSignalConsumer> Inputs { get { return _inputs; } }
        public IEnumerable<DigitalSignalProvider> Outputs { get { return _outputs; } }
        // active low
        public DigitalSignalConsumer OutputEnable { get { return _outputEnable; }  }

        // returns index (always added to the end)
        public int Add(out DigitalSignalConsumer input,  DigitalSignalProvider output, string name = null)
        {
            var index = _inputs.Count;

            input = new DigitalSignalConsumer(name);
            input.OnChanged += Input_OnChanged;
            _inputs.Add(input);

            output = new DigitalSignalProvider(name);
            _outputs.Add(output);

            return index;
        }

        protected void DisableAllOutputs()
        {
            foreach (var output in _outputs)
                output.Write(DigitalLevel.Floating);
        }

        protected void WriteAllOutputs()
        {
            for (int i = 0; i < _inputs.Count; i++)
                _outputs[i].Write(_inputs[i].Level);
        }

        protected void WriteOutputFor(DigitalSignalConsumer input)
        {
            var index = _inputs.IndexOf(input);
            _outputs[index].Write(input.Level);
        }

        private void Input_OnChanged(object sender, DigitalLevelChangedEventArgs e)
        {
            WriteOutputFor((DigitalSignalConsumer)sender);
        }

        private void OutputEnable_OnChanged(object sender, DigitalLevelChangedEventArgs e)
        {
            if (_outputEnable.Level == DigitalLevel.Low)
                WriteAllOutputs();
            else
                DisableAllOutputs();
        }
    }
}
