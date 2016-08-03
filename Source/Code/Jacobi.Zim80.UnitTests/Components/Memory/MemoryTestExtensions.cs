using System.IO;

namespace Jacobi.Zim80.Components.Memory.UnitTests
{
    internal static class MemoryTestExtensions
    {
        public static MemoryRom<BusData16, BusData8> NewRom(byte[] buffer)
        {
            var mem = new MemoryRom<BusData16, BusData8>();
            var writer = new MemoryWriter<BusData16, BusData8>(mem);
            writer.CopyFrom(CreateBinaryReader(buffer));
            return mem;
        }

        public static MemoryRam<BusData16, BusData8> NewRam(byte[] buffer)
        {
            var mem = new MemoryRam<BusData16, BusData8>();
            var writer = new MemoryWriter<BusData16, BusData8>(mem);
            writer.CopyFrom(CreateBinaryReader(buffer));
            return mem;
        }

        private static BinaryReader CreateBinaryReader(byte[] buffer)
        {
            return new BinaryReader(new MemoryStream(buffer, false));
        }
    }
}
