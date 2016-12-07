namespace Jacobi.Zim80.Logic
{
    public class OutputPort : INamedObject
    {
        public OutputPort()
        {
            _portEnable = new DigitalSignalConsumer("PE");
            _portEnable.OnChanged += PortEnable_OnChanged;
            _busSlave = new BusSlave("OutputPort Input");
        }

        public OutputPort(string name)
        {
            Name = name;
            _portEnable = new DigitalSignalConsumer("PE");
            _portEnable.OnChanged += PortEnable_OnChanged;
            _busSlave = new BusSlave(name + "Input");
        }

        public OutputPort(Bus bus)
            : this()
        {
            Input.ConnectTo(bus);
        }

        public OutputPort(Bus bus, string name)
            : this(name)
        {
            Input.ConnectTo(bus);
        }

        private readonly DigitalSignalConsumer _portEnable;

        // active low (samples on pos-edge)
        public DigitalSignalConsumer PortEnable { get { return _portEnable; } }

        public string Name { get; set; }

        private BusDataStream _dataStream;
        public BusDataStream DataStream { get { return _dataStream; } }

        private readonly BusSlave _busSlave;
        public BusSlave Input { get { return _busSlave; } }

        private void PortEnable_OnChanged(object sender, DigitalLevelChangedEventArgs e)
        {
            if (e.Level != DigitalLevel.PosEdge) return;
            if (!Input.IsConnected) return;

            if (_dataStream == null)
                _dataStream = new BusDataStream(Input.Bus);

            _dataStream.Sample();
        }
    }
}
