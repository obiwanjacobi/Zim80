using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace Jacobi.Zim80.UnitTests
{
    [TestClass]
    public class DigitalSignalConsumerTest
    {
        [TestMethod]
        public void Ctor_LevelIsFloating()
        {
            var consumer = new DigitalSignalConsumer();
            consumer.Level.Should().Be(DigitalLevel.Floating);
        }
    }
}
