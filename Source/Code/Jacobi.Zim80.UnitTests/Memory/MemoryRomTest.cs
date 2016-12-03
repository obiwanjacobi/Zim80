using FluentAssertions;
using Jacobi.Zim80.Test;
using Jacobi.Zim80.UnitTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Jacobi.Zim80.Memory.UnitTests
{
    [TestClass]
    public class MemoryRomTest
    {
        private const byte Value = 0x55;

        [TestMethod]
        public void Initialize_NoErrors()
        {
            var mem = MemoryTestExtensions.NewRom(new byte[] { Value });
            mem.Should().NotBeNull();
        }

        [TestMethod]
        public void Write_DigitalSignals_NoErrors()
        {
            var mem = MemoryTestExtensions.NewRom(new byte[] { Value });
            var ceProv = mem.ChipEnable.CreateConnection();
            var oeProv = mem.OutputEnable.CreateConnection();
            ceProv.Write(DigitalLevel.Low);
            oeProv.Write(DigitalLevel.Low);
        }

        [TestMethod]
        public void Write_AddressBus_NoErrors()
        {
            var mem = MemoryTestExtensions.NewRom(new byte[] { Value });
            var addressBus = mem.Address.CreateConnection();
            addressBus.IsEnabled = true;
            addressBus.Write(new BusData16(0));
        }

        [TestMethod]
        public void Read_FirstAddress_CorrectValue()
        {
            var mem = MemoryTestExtensions.NewRom(new byte[] { Value });
            var addressBus = mem.Address.CreateConnection();
            var dataBus = mem.Data.CreateConnection();
            var ceProv = mem.ChipEnable.CreateConnection();
            var oeProv = mem.OutputEnable.CreateConnection();

            // enable chip and output
            ceProv.Write(DigitalLevel.Low);
            oeProv.Write(DigitalLevel.Low);
            // set address
            addressBus.IsEnabled = true;
            addressBus.Write(new BusData16(0));
            // read data
            dataBus.Value.Equals(new BusData8(Value)).Should().BeTrue();
        }

        [TestMethod]
        public void Read_NextAddress_CorrectValue()
        {
            var mem = MemoryTestExtensions.NewRom(new byte[] { 0, Value });
            var addressBus = mem.Address.CreateConnection();
            var dataBus = mem.Data.CreateConnection();
            var ceProv = mem.ChipEnable.CreateConnection();
            var oeProv = mem.OutputEnable.CreateConnection();

            // enable chip and output
            ceProv.Write(DigitalLevel.Low);
            oeProv.Write(DigitalLevel.Low);
            // set address
            addressBus.IsEnabled = true;
            addressBus.Write(new BusData16(1));
            // read data
            dataBus.Value.Equals(new BusData8(Value)).Should().BeTrue();
        }

        [TestMethod]
        public void Read_SetAddressFirst_CorrectValue()
        {
            var mem = MemoryTestExtensions.NewRom(new byte[] { Value });
            var addressBus = mem.Address.CreateConnection();
            var dataBus = mem.Data.CreateConnection();
            var ceProv = mem.ChipEnable.CreateConnection();
            var oeProv = mem.OutputEnable.CreateConnection();

            // set address
            addressBus.IsEnabled = true;
            addressBus.Write(new BusData16(0));

            // enable chip and output
            ceProv.Write(DigitalLevel.Low);
            oeProv.Write(DigitalLevel.Low);
            
            // read data
            dataBus.Value.Equals(new BusData8(Value)).Should().BeTrue();
        }

        [TestMethod]
        public void Read_FirstAddress_SetOutputEnableFirst_CorrectValue()
        {
            var mem = MemoryTestExtensions.NewRom(new byte[] { Value });
            var addressBus = mem.Address.CreateConnection();
            var dataBus = mem.Data.CreateConnection();
            var ceProv = mem.ChipEnable.CreateConnection();
            var oeProv = mem.OutputEnable.CreateConnection();

            // set output enable
            oeProv.Write(DigitalLevel.Low);

            // set address
            addressBus.IsEnabled = true;
            addressBus.Write(new BusData16(0));

            // enable chip
            ceProv.Write(DigitalLevel.Low);

            // read data
            dataBus.Value.Equals(new BusData8(Value)).Should().BeTrue();
        }

        [TestMethod]
        public void Read_Inactive_NoValue()
        {
            var mem = MemoryTestExtensions.NewRom(new byte[] { Value });
            var addressBus = mem.Address.CreateConnection();
            var dataBus = mem.Data.CreateConnection();
            var ceProv = mem.ChipEnable.CreateConnection();
            var oeProv = mem.OutputEnable.CreateConnection();

            // do not enable chip
            ceProv.Write(DigitalLevel.High);
            oeProv.Write(DigitalLevel.Low);
            // set address
            addressBus.IsEnabled = true;
            addressBus.Write(new BusData16(0));
            // read data
            dataBus.Value.Equals(new BusData8()).Should().BeTrue();
        }

        [TestMethod]
        public void Write_Rom_Throws()
        {
            var mem = MemoryTestExtensions.NewRom(new byte[] { Value });
            var addressBus = mem.Address.CreateConnection();
            var dataBus = mem.Data.Slave.CreateConnection();
            var ceProv = mem.ChipEnable.CreateConnection();
            var oeProv = mem.OutputEnable.CreateConnection();
            
            ceProv.Write(DigitalLevel.Low);
            oeProv.Write(DigitalLevel.Low);
            // set address
            addressBus.IsEnabled = true;
            addressBus.Write(new BusData16(0));

            Action fn = () => dataBus.Write(new BusData8(0));
            fn.ShouldThrow<InvalidOperationException>();
        }
    }
}
