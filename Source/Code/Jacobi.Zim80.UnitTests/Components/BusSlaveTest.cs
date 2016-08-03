using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.Components;
using FluentAssertions;

namespace Jacobi.Zim80.UnitTests.Components
{
    [TestClass]
    public class BusSlaveTest
    {
        [TestMethod]
        public void Ctor_HasValue()
        {
            var slave = new BusSlave<BusData8>();
            slave.Value.Should().NotBeNull();
        }

        [TestMethod]
        public void Ctor_AllLevelsAreFloating()
        {
            var slave = new BusSlave<BusData8>();
            slave.Value.AllLevelsAre(DigitalLevel.Floating);
        }
    }
}
