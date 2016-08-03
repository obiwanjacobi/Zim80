using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace Jacobi.Zim80.UnitTests
{
    [TestClass]
    public class DigitalSignalProviderTest
    {
        [TestMethod]
        public void Ctor_LevelIsFloating()
        {
            var provider = new DigitalSignalProvider();
            provider.Level.Should().Be(DigitalLevel.Floating);
        }
    }
}
