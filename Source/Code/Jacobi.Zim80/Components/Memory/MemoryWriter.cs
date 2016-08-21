using System;
using System.IO;

namespace Jacobi.Zim80.Components.Memory
{
    public class MemoryWriter<AddressT, DataT>
        where AddressT : BusData, new()
        where DataT : BusData, new()
    {
        private readonly IDirectMemoryAccess<DataT> _memory;

        public MemoryWriter(IDirectMemoryAccess<DataT> memory)
        {
            _memory = memory;
        }

        public long Fill(long size, DataT data)
        {
            var length = CalcLength(size);

            // TODO: int-long mismatch
            for (int address = 0; address < length; address++)
            {
                _memory[address] = data;
            }

            return length;
        }

        public DataT this[AddressT address]
        {
            // TODO: unsiged to signed cast
            get { return _memory[(int)address.ToUInt32()]; }
            set { _memory[(int)address.ToUInt32()] = value; }
        }

        public long CopyFrom(BinaryReader reader)
        {
            var width = new DataT().Width;
            long bytesCopied;

            if (width <= 8)
                bytesCopied = CopyFrom8(reader);
            else if (width <= 16)
                bytesCopied = CopyFrom16(reader);
            else if (width <= 32)
                bytesCopied = CopyFrom32(reader);
            else
                throw new NotSupportedException("DataT is bigger than 32 bits.");

            return bytesCopied;
        }

        private long CopyFrom8(BinaryReader reader)
        {
            return WriteToMemory(reader.BaseStream.Length, (data) => { data.Write(reader.ReadByte()); });
        }

        private long CopyFrom16(BinaryReader reader)
        {
            return WriteToMemory(reader.BaseStream.Length, (data) => { data.Write(reader.ReadUInt16()); });
        }

        private long CopyFrom32(BinaryReader reader)
        {
            return WriteToMemory(reader.BaseStream.Length, (data) => { data.Write(reader.ReadUInt32()); });
        }

        private long WriteToMemory(long maxLength, Action<DataT> writeData)
        {
            long length = CalcLength(maxLength);

            // TODO: int-long mismatch
            for (int address = 0; address < length; address++)
            {
                DataT data = new DataT();
                writeData(data);
                _memory[address] = data;
            }

            return length;
        }

        private static long CalcLength(long size)
        {
            return Math.Min(size, (long)Math.Pow(2, new AddressT().Width));
        }
    }
}
