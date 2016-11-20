using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.Components;
using FluentAssertions;
using System;

namespace Jacobi.Zim80.UnitTests.Components
{
    [TestClass]
    public class BusMasterTest
    {
        [TestMethod]
        public void Ctor_HasLevels()
        {
            var uut = new BusMaster<BusData8>();
            uut.Value.Should().NotBeNull();
        }

        [TestMethod]
        public void Ctor_MasterIsDisabled()
        {
            var uut = new BusMaster<BusData8>();
            uut.IsEnabled.Should().BeFalse();
        }

        [TestMethod]
        public void Ctor_LevelsAreFloating()
        {
            var uut = new BusMaster<BusData8>();
            uut.Value.AllLevelsAre(DigitalLevel.Floating);
        }

        [TestMethod]
        public void Write_DisabledMaster_Throws()
        {
            var uut = new BusMaster<BusData8>();
            var newValue = new BusData8(0);

            Action act = () => uut.Write(newValue);

            act.ShouldThrow<InvalidOperationException>();
        }

        [TestMethod]
        public void Write_Unconnected_DoesNotThrow()
        {
            var uut = new BusMaster<BusData8>();
            var newValue = new BusData8(0);
            uut.IsEnabled = true;

            Action test = () => uut.Write(newValue);
            test.ShouldNotThrow();
        }

        [TestMethod]
        public void IsEnabled_ToFalse_ChangesLevelsToFloating()
        {
            var uut = new BusMaster<BusData8>();
            var bus = new Bus<BusData8>();
            uut.ConnectTo(bus);
            uut.IsEnabled = true;

            var newValue = new BusData8(0);
            uut.Write(newValue);
            uut.IsEnabled = false;

            uut.Value.AllLevelsAre(DigitalLevel.Floating);
        }

        [TestMethod]
        public void Connect_OneMaster_NoErrors()
        {
            var uut = new BusMaster<BusData8>();
            var bus = new Bus<BusData8>();

            uut.ConnectTo(bus);
        }

        [TestMethod]
        public void Connect_MultipleMasters_NoErrors()
        {
            var master1 = new BusMaster<BusData8>();
            var master2 = new BusMaster<BusData8>();
            var bus = new Bus<BusData8>();

            master1.ConnectTo(bus);
            master2.ConnectTo(bus);
        }

        [TestMethod]
        public void Write_BusValueChanged()
        {
            var uut = new BusMaster<BusData8>();
            var bus = new Bus<BusData8>();
            uut.ConnectTo(bus);
            uut.IsEnabled = true;

            var newValue = new BusData8(0);
            uut.Write(newValue);

            bus.AllLevelsAre(DigitalLevel.Low);
        }
    }
}
