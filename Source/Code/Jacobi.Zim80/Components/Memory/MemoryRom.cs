using System;
using System.IO;

namespace Jacobi.Zim80.Components.Memory
{
    public class MemoryRom<AddressT, DataT> 
        // TODO: remove hard coded BusData types!!
        where AddressT : BusData16, new()
        where DataT : BusData8, new()
    {
        private byte[] _memory;

        public MemoryRom()
        {
            ChipEnable = new DigitalSignalConsumer();
            OutputEnable = new DigitalSignalConsumer();
            Address = new BusSlave<AddressT>();
            Data = new BusMasterSlave<DataT>();

            ChipEnable.OnChanged += ChipEnable_OnChanged;
            OutputEnable.OnChanged += OutputEnable_OnChanged;
            Address.OnChanged += Address_OnChanged;
        }

        public void Initialize(BinaryReader reader)
        {
            if (reader.BaseStream.Length > (long)Math.Pow(2, Address.Value.Width))
                throw new ArgumentOutOfRangeException("reader", "Stream too long.");

            _memory = reader.ReadBytes((int)reader.BaseStream.Length);
        }

        public DigitalSignalConsumer ChipEnable { get; private set; }
        public DigitalSignalConsumer OutputEnable { get; private set; }
        public BusSlave<AddressT> Address { get; private set; }
        public BusMasterSlave<DataT> Data { get; private set; }

        protected byte Read()
        {
            var address = Address.Value.ToUInt16();
            return _memory[address];
        }

        protected void Write(byte data)
        {
            var address = Address.Value.ToUInt16();
            _memory[address] = data;
        }

        private void OutputData()
        {
            if (ChipEnable.Level == DigitalLevel.Low &&
                OutputEnable.Level == DigitalLevel.Low)
            {
                Data.IsEnabled = true;
                var data = new DataT();
                data.Write(Read());
                Data.Write(data);
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
