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
            var master = new BusMaster<BusData8>();
            master.Value.Should().NotBeNull();
        }

        [TestMethod]
        public void Ctor_MasterIsDisabled()
        {
            var master = new BusMaster<BusData8>();
            master.IsEnabled.Should().BeFalse();
        }

        [TestMethod]
        public void Ctor_LevelsAreFloating()
        {
            var master = new BusMaster<BusData8>();
            master.Value.AllLevelsAre(DigitalLevel.Floating);
        }

        [TestMethod]
        public void Write_DisabledMaster_Error()
        {
            var master = new BusMaster<BusData8>();
            var newValue = new BusData8(0);

            Action act = () => master.Write(newValue);

            act.ShouldThrow<InvalidOperationException>();
        }

        [TestMethod]
        public void Write_ChangesLevels()
        {
            var master = new BusMaster<BusData8>();
            var newValue = new BusData8(0);
            master.IsEnabled = true;

            master.Write(newValue);

            master.Value.AllLevelsAre(DigitalLevel.Low);
        }

        [TestMethod]
        public void IsEnabled_ToFalse_ChangesLevelsToFloating()
        {
            var master = new BusMaster<BusData8>();
            var newValue = new BusData8(0);
            master.IsEnabled = true;

            master.Write(newValue);
            master.IsEnabled = false;

            master.Value.AllLevelsAre(DigitalLevel.Floating);
        }
    }
}
