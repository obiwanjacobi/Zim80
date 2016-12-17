namespace Jacobi.Zim80.Logic
{
    public class InputPort : INamedObject
    {
        public InputPort()
        {
            _portEnable = new DigitalSignalConsumer("PE");
            _portEnable.OnChanged += PortEnable_OnChanged;
            _busMaster = new BusMaster("InputPort Output");
            _buffer = new BusDataBuffer();
        }

        public InputPort(string name)
        {
            Name = name;
            _portEnable = new DigitalSignalConsumer("PE");
            _portEnable.OnChanged += PortEnable_OnChanged;
            _busMaster = new BusMaster(name + "-Output");
            _buffer = new BusDataBuffer();
        }

        public InputPort(Bus bus)
            : this()
        {
            Output.ConnectTo(bus);
        }

        public InputPort(Bus bus, string name)
            : this(name)
        {
            Output.ConnectTo(bus);
        }

        private readonly DigitalSignalConsumer _portEnable;

        // active low (samples on pos-edge)
        public DigitalSignalConsumer PortEnable { get { return _portEnable; } }

        public string Name { get; set; }

        private readonly BusDataBuffer _buffer;
        public BusDataBuffer DataBuffer { get { return _buffer; } }

        private readonly BusMaster _busMaster;
        public BusMaster Output { get { return _busMaster; } }

        private void PortEnable_OnChanged(object sender, DigitalLevelChangedEventArgs e)
        {
            if (e.Level != DigitalLevel.PosEdge)
            {
                Output.IsEnabled = false;
                return;
            }
            if (!Output.IsConnected) return;

            var data = DataBuffer.Read();

            if (data != null)
            {
                Output.IsEnabled = true;
                Output.Write(data);
            }
            // TODO: else write floating?
        }
    }
}
