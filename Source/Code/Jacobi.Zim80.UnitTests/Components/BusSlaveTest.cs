using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.Components;
using FluentAssertions;
using System;

namespace Jacobi.Zim80.UnitTests.Components
{
    [TestClass]
    public class BusSlaveTest
    {
        [TestMethod]
        public void Unconnected_ReadValue_DoesNotThrow()
        {
            var uut = new BusSlave<BusData8>();

            Action test = () => { var l = uut.Value; };

            test.ShouldNotThrow();
        }

        [TestMethod]
        public void Connected_AllLevelsAreFloating()
        {
            var uut = new BusSlave<BusData8>();
            var bus = new Bus<BusData8>();
            uut.ConnectTo(bus);

            uut.Value.AllLevelsAre(DigitalLevel.Floating);
        }
    }
}
