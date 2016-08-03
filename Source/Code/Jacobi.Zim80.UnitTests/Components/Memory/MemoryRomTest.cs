using FluentAssertions;
using Jacobi.Zim80.Components.UnitTests;
using Jacobi.Zim80.UnitTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jacobi.Zim80.Components.Memory.UnitTests
{
    [TestClass]
    public class MemoryRomTest
    {
        [TestMethod]
        public void Initialize_NoErrors()
        {
            var mem = MemoryTestExtensions.NewRom(new byte[] { 0x00 });
            mem.Should().NotBeNull();
        }

        [TestMethod]
        public void Write_DigitalSignals_NoErrors()
        {
            var mem = MemoryTestExtensions.NewRom(new byte[] { 0x00 });
            var ceProv = mem.ChipEnable.CreateConnection();
            var oeProv = mem.OutputEnable.CreateConnection();
            ceProv.Write(DigitalLevel.Low);
            oeProv.Write(DigitalLevel.Low);
        }

        [TestMethod]
        public void Write_AddressBus_NoErrors()
        {
            var mem = MemoryTestExtensions.NewRom(new byte[] { 0x00 });
            var addressBus = mem.Address.CreateConnection();
            addressBus.Write(new BusData16(0));
        }

        [TestMethod]
        public void Read_FirstAddress_CorrectValue()
        {
            var mem = MemoryTestExtensions.NewRom(new byte[] { 0x00 });
            var addressBus = mem.Address.CreateConnection();
            var dataBus = mem.Data.CreateConnection();
            var ceProv = mem.ChipEnable.CreateConnection();
            var oeProv = mem.OutputEnable.CreateConnection();

            // enable chip and output
            ceProv.Write(DigitalLevel.Low);
            oeProv.Write(DigitalLevel.Low);
            // set address
            addressBus.Write(new BusData16(0));
            // read data
            dataBus.Value.Equals(new BusData8(0)).Should().BeTrue();
        }

        [TestMethod]
        public void Read_Inactive_NoValue()
        {
            var mem = MemoryTestExtensions.NewRom(new byte[] { 0x00 });
            var addressBus = mem.Address.CreateConnection();
            var dataBus = mem.Data.CreateConnection();
            var ceProv = mem.ChipEnable.CreateConnection();
            var oeProv = mem.OutputEnable.CreateConnection();

            // do not enable chip
            ceProv.Write(DigitalLevel.High);
            oeProv.Write(DigitalLevel.Low);
            // set address
            addressBus.Write(new BusData16(0));
            // read data
            dataBus.Value.Equals(new BusData8()).Should().BeTrue();
        }
    }
}
