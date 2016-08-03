using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System;

namespace Jacobi.Zim80.Components.UnitTests
{
    [TestClass]
    public class BusDataTest
    {
        private const int TestValue = 42;

        [TestMethod]
        public void Write_Data8_Byte()
        {
            var data = new BusData8();
            data.Write((byte)TestValue);

            data.ToByte().Should().Be((byte)TestValue);
        }

        [TestMethod]
        public void Write_Data16_UInt16()
        {
            var data = new BusData16();
            data.Write((UInt16)TestValue);

            data.ToUInt16().Should().Be((UInt16)TestValue);
        }

        [TestMethod]
        public void Write_Data24_UInt32()
        {
            var data = new BusData24();
            data.Write((UInt32)TestValue);

            data.ToUInt32().Should().Be((UInt32)TestValue);
        }
    }
}
