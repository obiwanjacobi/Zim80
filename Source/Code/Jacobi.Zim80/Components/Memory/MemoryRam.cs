using System;

namespace Jacobi.Zim80.Components.Memory
{
    public class MemoryRam<AddressT, DataT> : MemoryRom<AddressT, DataT>
        where AddressT : BusData, new()
        where DataT : BusData, new()
    {
        public MemoryRam()
        {
            WriteEnable = new DigitalSignalConsumer();
            WriteEnable.OnChanged += WriteEnable_OnChanged;
        }

        public DigitalSignalConsumer WriteEnable { get; private set; }

        protected override void OnStateChanged()
        {
            if (!Data.IsConnected) return;

            InputData(Data.Slave.Value);
            base.OnStateChanged();
        }

        protected override void OnWriteDataBus(DataT value)
        {
            InputData(value);
        }

        private void WriteEnable_OnChanged(object sender, DigitalLevelChangedEventArgs e)
        {
            OnStateChanged();
        }

        private bool IsEnabled
        {
            get
            {
                return ChipEnable.Level == DigitalLevel.Low &&
                       WriteEnable.Level == DigitalLevel.Low;
            }
        }

        private void InputData(DataT data)
        {
            ThrowIfOutputAndWriteAreActive();

            if (IsEnabled)
            {
                Data.IsEnabled = false;
                Write(data);
            }
        }

        private void ThrowIfOutputAndWriteAreActive()
        {
            if (ChipEnable.Level == DigitalLevel.Low &&
                WriteEnable.Level == DigitalLevel.Low &&
                OutputEnable.Level == DigitalLevel.Low)
            {
                throw new InvalidOperationException(
                    "Both OutputEnable and WriteEnable are active (Low).");
            }
        }
    }
}
