using Jacobi.Zim80.Memory;
using System.IO;
using System.Text;

namespace Jacobi.Zim80.Test
{
    public static class MemoryExtensions
    {
        public static void Set<DataT>(this IDirectMemoryAccess<DataT> memory, 
                int address, byte value)
            where DataT : BusData, new()
        {
            var data = new DataT();
            data.Write(value);

            memory[address] = data;
        }

        public static DataT Get<DataT>(this IDirectMemoryAccess<DataT> memory,
                int address)
            where DataT : BusData
        {
            return memory[address];
        }

        public static uint GetInt<DataT>(this IDirectMemoryAccess<DataT> memory,
                int address)
            where DataT : BusData
        {
            return memory[address].ToUInt32();
        }

        public static string GetString<DataT>(this IDirectMemoryAccess<DataT> memory,
                int address, int length = -1)
            where DataT : BusData
        {
            var builder = new StringBuilder();
            bool stop = false;

            do
            {
                var c = (char)memory[address].ToByte();

                if (c == 0)
                    stop = true;
                else
                    builder.Append(c);

                address++;

                if (length == 0)
                    stop = true;
            }
            while (!stop);

            return builder.ToString();
        }

        public static long Write<AddressT, DataT>(this Memory<AddressT, DataT> memory, byte[] buffer)
            where AddressT : BusData, new()
            where DataT : BusData, new()
        {
            var writer = new MemoryWriter<AddressT, DataT>(memory);
            return writer.CopyFrom(CreateBinaryReader(buffer));
        }

        private static BinaryReader CreateBinaryReader(byte[] buffer)
        {
            return new BinaryReader(new MemoryStream(buffer, false));
        }
    }
}
