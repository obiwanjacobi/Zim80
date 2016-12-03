using FluentAssertions;
using Jacobi.Zim80.Test;

namespace Jacobi.Zim80.Memory.UnitTests
{
    internal static class MemoryTestExtensions
    {
        public static void Assert(this IDirectMemoryAccess<BusData8> memory, 
                ushort address, byte expected)
        {
            var actual = memory[address];
            actual.ToByte().Should().Be(expected, "at address: " + address);
        }

        public static void Set(this IDirectMemoryAccess<BusData8> memory,
                ushort address, byte value)
        {
            memory[address] = new BusData8(value);
        }

        public static MemoryRom<BusData16, BusData8> NewRom(byte[] buffer)
        {
            var mem = new MemoryRom<BusData16, BusData8>();

            if (buffer != null)
                mem.Write(buffer);

            return mem;
        }

        public static MemoryRam<BusData16, BusData8> NewRam(byte[] buffer)
        {
            var mem = new MemoryRam<BusData16, BusData8>();

            if (buffer != null)
                mem.Write(buffer);

            return mem;
        }
    }
}
