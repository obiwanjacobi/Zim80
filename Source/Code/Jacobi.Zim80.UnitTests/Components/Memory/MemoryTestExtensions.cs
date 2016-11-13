using FluentAssertions;
using System.IO;

namespace Jacobi.Zim80.Components.Memory.UnitTests
{
    internal static class MemoryTestExtensions
    {
        public static void Assert(this IDirectMemoryAccess<BusData8> memory, 
                ushort address, byte expected)
        {
            var actual = memory[address];
            actual.ToByte().Should().Be(expected, "at address: " + address);
        }

        public static MemoryRom<BusData16, BusData8> NewRom(byte[] buffer)
        {
            var mem = new MemoryRom<BusData16, BusData8>();

            if (buffer != null)
            {
                var writer = new MemoryWriter<BusData16, BusData8>(mem);
                writer.CopyFrom(CreateBinaryReader(buffer));
            }

            return mem;
        }

        public static MemoryRam<BusData16, BusData8> NewRam(byte[] buffer)
        {
            var mem = new MemoryRam<BusData16, BusData8>();

            if (buffer != null)
            {
                var writer = new MemoryWriter<BusData16, BusData8>(mem);
                writer.CopyFrom(CreateBinaryReader(buffer));
            }

            return mem;
        }

        private static BinaryReader CreateBinaryReader(byte[] buffer)
        {
            return new BinaryReader(new MemoryStream(buffer, false));
        }
    }
}
