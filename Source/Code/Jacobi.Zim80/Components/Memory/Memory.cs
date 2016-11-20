using System;

namespace Jacobi.Zim80.Components.Memory
{
    public abstract class Memory<AddressT, DataT> : IDirectMemoryAccess<DataT>,
        IMemoryAccessNotification<AddressT, DataT>
        where AddressT : BusData, new()
        where DataT : BusData, new()
    {
        private readonly DataT[] _memory;

        public Memory()
        {
            ChipEnable = new DigitalSignalConsumer("CE");
            Address = new BusSlave<AddressT>("Address");
            Data = new BusMasterSlave<DataT>("Data");

            ChipEnable.OnChanged += ChipEnable_OnChanged;
            Address.OnChanged += Address_OnChanged;
            Data.Slave.OnChanged += DataIn_OnChanged;

            long length = (long)Math.Pow(2, Address.Value.Width);
            _memory = new DataT[length];
        }

        public DigitalSignalConsumer ChipEnable { get; private set; }
        public BusSlave<AddressT> Address { get; private set; }
        public BusMasterSlave<DataT> Data { get; private set; }

        public event EventHandler<MemoryNotificationEventArgs<AddressT, DataT>> OnMemoryRead;
        public event EventHandler<MemoryNotificationEventArgs<AddressT, DataT>> OnMemoryWritten;

        DataT IDirectMemoryAccess<DataT>.this[int address]
        {
            get { return Read(address); }
            set { Write(address, value); }
        }

        protected DataT Read()
        {
            var address = Address.Value.ToUInt16();
            var data = Read(address);

            NotifyMemoryRead(Address.Value, data);

            return data;
        }

        protected void Write(DataT data)
        {
            var address = Address.Value.ToUInt16();
            Write(address, data);
            NotifyMemoryWritten(Address.Value, data);
        }

        protected DataT Read(int address)
        {
            var data = _memory[address];

            if (data == null)
                throw new UninitialzedDataException(address);

            return data;
        }

        protected void NotifyMemoryRead(AddressT address, DataT data)
        {
            OnMemoryRead?.Invoke(this, new MemoryNotificationEventArgs<AddressT, DataT>(address, data));
        }

        protected void NotifyMemoryWritten(AddressT address, DataT data)
        {
            OnMemoryWritten?.Invoke(this, new MemoryNotificationEventArgs<AddressT, DataT>(address, data));
        }

        protected virtual void OnStateChanged()
        { }

        protected virtual void OnWriteDataBus(DataT value)
        { }

        private void Write(int address, DataT value)
        {
            _memory[address] = value;
        }

        private void Address_OnChanged(object sender, BusChangedEventArgs<AddressT> e)
        {
            OnStateChanged();
        }

        private void ChipEnable_OnChanged(object sender, DigitalLevelChangedEventArgs e)
        {
            OnStateChanged();
        }

        private void DataIn_OnChanged(object sender, BusChangedEventArgs<DataT> e)
        {
            OnWriteDataBus(e.Value);
        }
    }
}
