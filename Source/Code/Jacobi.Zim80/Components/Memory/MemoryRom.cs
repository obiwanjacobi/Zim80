using System;

namespace Jacobi.Zim80.Components.Memory
{
    public class MemoryRom<AddressT, DataT> : IDirectMemoryAccess<DataT>
        where AddressT : BusData, new()
        where DataT : BusData, new()
    {
        private DataT[] _memory;

        public MemoryRom()
        {
            ChipEnable = new DigitalSignalConsumer();
            OutputEnable = new DigitalSignalConsumer();
            Address = new BusSlave<AddressT>();
            Data = new BusMasterSlave<DataT>();

            ChipEnable.OnChanged += ChipEnable_OnChanged;
            OutputEnable.OnChanged += OutputEnable_OnChanged;
            Address.OnChanged += Address_OnChanged;

            AllocateMemory();
        }

        private void AllocateMemory()
        {
            long length = (long)Math.Pow(2, Address.Value.Width);
            _memory = new DataT[length];
        }

        public DigitalSignalConsumer ChipEnable { get; private set; }
        public DigitalSignalConsumer OutputEnable { get; private set; }
        public BusSlave<AddressT> Address { get; private set; }
        public BusMasterSlave<DataT> Data { get; private set; }

        DataT IDirectMemoryAccess<DataT>.this[int address]
        {
            get { return Read(address); }
            set { Write(address, value); }
        }

        protected DataT Read(int address)
        {
            var data = _memory[address];

            if (data == null)
                throw new UninitialzedDataException(address);

            return data;
        }

        protected DataT Read()
        {
            var address = Address.Value.ToUInt16();
            return Read(address);
        }

        protected void Write(int address, DataT value)
        {
            _memory[address] = value;
        }

        protected void Write(DataT data)
        {
            var address = Address.Value.ToUInt16();
            Write(address, data);
        }

        private void OutputData()
        {
            if (ChipEnable.Level == DigitalLevel.Low &&
                OutputEnable.Level == DigitalLevel.Low)
            {
                Data.IsEnabled = true;
                Data.Write(Read());
            }
            else
            {
                Data.IsEnabled = false;
            }
        }

        private void Address_OnChanged(object sender, BusChangedEventArgs<AddressT> e)
        {
            OutputData();
        }

        private void OutputEnable_OnChanged(object sender, DigitalLevelChangedEventArgs e)
        {
            OutputData();
        }

        private void ChipEnable_OnChanged(object sender, DigitalLevelChangedEventArgs e)
        {
            OutputData();
        }
    }
}
