namespace Jacobi.Zim80.Components.Memory
{
    public class MemoryRam<AddressT, DataT> : MemoryRom<AddressT, DataT>
        where AddressT : BusData, new()
        where DataT : BusData, new()
    {
        private readonly BusSlave<DataT> _dataSlave;

        public MemoryRam()
        {
            WriteEnable = new DigitalSignalConsumer();
            WriteEnable.OnChanged += WriteEnable_OnChanged;
            _dataSlave = Data.ToSlave();
            _dataSlave.OnChanged += DataSlave_OnChanged;
        }

        public DigitalSignalConsumer WriteEnable { get; private set; }

        private void DataSlave_OnChanged(object sender, BusChangedEventArgs<DataT> e)
        {
            InputData(e.Value);
        }

        private void WriteEnable_OnChanged(object sender, DigitalLevelChangedEventArgs e)
        {
            InputData(Data.Value);
        }

        private void InputData(DataT data)
        {
            if (ChipEnable.Level == DigitalLevel.Low &&
                WriteEnable.Level == DigitalLevel.Low)
            {
                Data.IsEnabled = false;
                Write(data);
            }
        }
    }
}
