using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80;
using FluentAssertions;
using System;

namespace Jacobi.Zim80.UnitTests
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

            uut.Value.AssertAllLevelsAre(DigitalLevel.Floating);
        }
    }
}
