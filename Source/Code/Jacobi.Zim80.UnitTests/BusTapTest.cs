using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace Jacobi.Zim80.UnitTests
{
    [TestClass]
    public class BusTapTest
    {
        [TestMethod]
        public void ConnectTo_BiggerSource_MaintainsBusWidth()
        {
            var busWidth = 8;
            var source = new Bus<BusData16>();
            var uut = new BusTap(busWidth);
            uut.ConnectTo(source);

            uut.Signals.Should().HaveCount(busWidth);
        }

        [TestMethod]
        public void ConnectTo_SmallerSource_ThrowsException()
        {
            var busWidth = 8;
            var source = new Bus(4);
            var uut = new BusTap(busWidth);

            Action test = () => uut.ConnectTo(source);

            test.ShouldThrow<ArgumentException>();
        }

        [TestMethod]
        public void ConnectTo_ChangeSource_BusValueCorrect()
        {
            var busWidth = 8;
            var source = new Bus<BusData16>();
            var uut = new BusTap(busWidth);
            uut.ConnectTo(source);

            var driver = new BusMaster(source);
            driver.IsEnabled = true;
            driver.Write(new BusData16(0xAA55));

            uut.Value.ToUInt32().Should().Be(0x55);
        }

        [TestMethod]
        public void ConnectTo_ChangeSource_EventFires()
        {
            var busWidth = 8;
            var source = new Bus<BusData16>();
            var uut = new BusTap(busWidth);
            uut.ConnectTo(source);
            uut.MonitorEvents();

            var driver = new BusMaster(source);
            driver.IsEnabled = true;
            driver.Write(new BusData16(0xAA55));

            uut.ShouldRaise("OnChanged");
        }

    }
}
