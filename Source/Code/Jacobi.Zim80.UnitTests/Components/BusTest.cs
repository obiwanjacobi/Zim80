using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.Components;
using FluentAssertions;
using System;

namespace Jacobi.Zim80.UnitTests.Components
{
    [TestClass]
    public class BusTest
    {
        [TestMethod]
        public void Ctor_BusData8_AllLevelsAre()
        {
            var bus = new Bus<BusData8>();

            bus.AllLevelsAre(DigitalLevel.Floating);
        }

        [TestMethod]
        public void Ctor_BusData16_AllLevelsAre()
        {
            var bus = new Bus<BusData16>();

            bus.AllLevelsAre(DigitalLevel.Floating);
        }

        [TestMethod]
        public void Ctor_BusData20_AllLevelsAre()
        {
            var bus = new Bus<BusData20>();

            bus.AllLevelsAre(DigitalLevel.Floating);
        }

        [TestMethod]
        public void Ctor_BusData24_AllLevelsAre()
        {
            var bus = new Bus<BusData24>();

            bus.AllLevelsAre(DigitalLevel.Floating);
        }

        [TestMethod]
        public void Write_EventTriggers()
        {
            var master = new BusMaster<BusData8>();
            var bus = new Bus<BusData8>();
            master.ConnectTo(bus);
            master.IsEnabled = true;
            bus.MonitorEvents();

            var newValue = new BusData8(0);
            master.Write(newValue);

            bus.ShouldRaise("OnChanged");
        }

        [TestMethod]
        public void Write_SingleMaster_SlaveSeesChanges()
        {
            var master = new BusMaster<BusData8>();
            var slave = new BusSlave<BusData8>();
            var bus = new Bus<BusData8>();
            master.ConnectTo(bus);
            slave.ConnectTo(bus);
            master.IsEnabled = true;

            var newValue = new BusData8(0);
            master.Write(newValue);

            slave.Value.AllLevelsAre(DigitalLevel.Low);
        }

        [TestMethod]
        public void Write_MultipleMasters_OneAtaTime()
        {
            var master1 = new BusMaster<BusData8>();
            var master2 = new BusMaster<BusData8>();
            var bus = new Bus<BusData8>();
            master1.ConnectTo(bus);
            master2.ConnectTo(bus);
            master1.IsEnabled = true;
            master2.IsEnabled = true;

            var loValue = new BusData8(0);
            var noValue = new BusData8();

            master1.Write(loValue);
            master1.Write(noValue);
            master2.Write(loValue);
            master2.Write(noValue);
            master1.Write(loValue);
        }

        [TestMethod]
        public void Write_MultipleMastersActivce_Error()
        {
            var master1 = new BusMaster<BusData8>();
            var master2 = new BusMaster<BusData8>();
            var bus = new Bus<BusData8>();
            master1.ConnectTo(bus);
            master2.ConnectTo(bus);
            master1.IsEnabled = true;
            master2.IsEnabled = true;

            var loValue = new BusData8(0);
            var noValue = new BusData8();

            master1.Write(loValue);
            Action act = () => master2.Write(loValue);

            act.ShouldThrow<BusConflictException>();
        }

        [TestMethod]
        public void Write_SingleMasters_MultipleSlavesSeeChanges()
        {
            var master = new BusMaster<BusData8>();
            var slave1 = new BusSlave<BusData8>();
            var slave2 = new BusSlave<BusData8>();
            var bus = new Bus<BusData8>();
            master.ConnectTo(bus);
            slave1.ConnectTo(bus);
            slave2.ConnectTo(bus);
            master.IsEnabled = true;

            var newValue = new BusData8(0);
            master.Write(newValue);

            slave1.Value.AllLevelsAre(DigitalLevel.Low);
            slave2.Value.AllLevelsAre(DigitalLevel.Low);
        }
    }
}
