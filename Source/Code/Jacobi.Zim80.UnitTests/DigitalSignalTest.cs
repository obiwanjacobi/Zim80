using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace Jacobi.Zim80.UnitTests
{
    [TestClass]
    public class DigitalSignalTest
    {
        [TestMethod]
        public void Ctor_LevelIsFloating()
        {
            var uut = new DigitalSignal();

            uut.Level.Should().Be(DigitalLevel.Floating);
        }

        [TestMethod]
        public void Ctor_Name_NameIsSet()
        {
            var name = "Test";
            var uut = new DigitalSignal(name);

            uut.Name.Should().Be(name);
        }
    }
}
