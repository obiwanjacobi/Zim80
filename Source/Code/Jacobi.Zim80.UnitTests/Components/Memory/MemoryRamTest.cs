using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Jacobi.Zim80.UnitTests;
using Jacobi.Zim80.Components.UnitTests;
using System;

namespace Jacobi.Zim80.Components.Memory.UnitTests
{
    [TestClass]
    public class MemoryRamTest
    {
        private const byte Value = 0x55;

        [TestMethod]
        public void Initialize_NoErrors()
        {
            var mem = MemoryTestExtensions.NewRam(new byte[] { 0x00 });
            mem.Should().NotBeNull();
        }

        [TestMethod]
        public void Enable_OutputAndWrite_Throws()
        {
            var mem = MemoryTestExtensions.NewRam(new byte[] { 0x00 });
            var ceProv = mem.ChipEnable.CreateConnection();
            var oeProv = mem.OutputEnable.CreateConnection();
            var weProv = mem.WriteEnable.CreateConnection();

            ceProv.Write(DigitalLevel.Low);
            weProv.Write(DigitalLevel.Low);

            Action fn = () => oeProv.Write(DigitalLevel.Low);
            fn.ShouldThrow<InvalidOperationException>();
        }

        [TestMethod]
        public void Write_FirstAddress_ReadBackCorrectValue()
        {
            var mem = MemoryTestExtensions.NewRam(new byte[] { 0x00 });
            var addressBus = mem.Address.CreateConnection();
            var rdDataBus = mem.Data.CreateConnection();
            var wrDataBus = mem.Data.Slave.CreateConnection();
            var ceProv = mem.ChipEnable.CreateConnection();
            var oeProv = mem.OutputEnable.CreateConnection();
            var weProv = mem.WriteEnable.CreateConnection();

            // enable chip and input
            ceProv.Write(DigitalLevel.Low);
            weProv.Write(DigitalLevel.Low);
            // set address
            addressBus.Write(new BusData16(0));
            // write data
            wrDataBus.Write(new BusData8(Value));

            weProv.Write(DigitalLevel.High);
            oeProv.Write(DigitalLevel.Low);

            rdDataBus.Value.Equals(new BusData8(Value)).Should().BeTrue();
        }

        [TestMethod]
        public void Write_SetAddressAndDataFirst_ReadBackCorrectValue()
        {
            var mem = MemoryTestExtensions.NewRam(new byte[] { 0x00 });
            var addressBus = mem.Address.CreateConnection();
            var rdDataBus = mem.Data.CreateConnection();
            var wrDataBus = mem.Data.Slave.CreateConnection();
            var ceProv = mem.ChipEnable.CreateConnection();
            var oeProv = mem.OutputEnable.CreateConnection();
            var weProv = mem.WriteEnable.CreateConnection();

            // set address
            addressBus.Write(new BusData16(0));
            // write data
            wrDataBus.Write(new BusData8(Value));

            // enable chip and input
            ceProv.Write(DigitalLevel.Low);
            weProv.Write(DigitalLevel.Low);
            

            weProv.Write(DigitalLevel.High);
            oeProv.Write(DigitalLevel.Low);

            rdDataBus.Value.Equals(new BusData8(Value)).Should().BeTrue();
        }
    }
}
