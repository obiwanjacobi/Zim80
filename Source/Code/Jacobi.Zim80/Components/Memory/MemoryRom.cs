using System;

namespace Jacobi.Zim80.Components.Memory
{
    public class MemoryRom<AddressT, DataT> : Memory<AddressT, DataT>
        where AddressT : BusData, new()
        where DataT : BusData, new()
    {
        public MemoryRom()
        {
            OutputEnable = new DigitalSignalConsumer("OE");
            OutputEnable.OnChanged += OutputEnable_OnChanged;
        }

        public string Name { get; set; }

        public DigitalSignalConsumer OutputEnable { get; private set; }

        protected override void OnStateChanged()
        {
            OutputData();
        }

        protected override void OnWriteDataBus(DataT value)
        {
            if (IsEnabled)
                throw new InvalidOperationException("Rom memory is written to.");
        }

        private bool IsEnabled
        {
            get
            {
                return ChipEnable.Level == DigitalLevel.Low &&
                       OutputEnable.Level == DigitalLevel.Low;
            }
        }

        private void OutputData()
        {
            if (IsEnabled)
            {
                if (Data.IsConnected)
                {
                    Data.IsEnabled = true;
                    Data.Write(Read());
                }
            }
            else
            {
                Data.IsEnabled = false;
            }
        }

        private void OutputEnable_OnChanged(object sender, DigitalLevelChangedEventArgs e)
        {
            OnStateChanged();
        }
    }
}
