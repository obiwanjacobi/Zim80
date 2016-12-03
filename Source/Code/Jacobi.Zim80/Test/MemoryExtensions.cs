using Jacobi.Zim80.Memory;
using System.IO;

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
