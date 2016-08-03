using System;
using System.Text;
using System.Collections.Generic;
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
            var signal = new DigitalSignal();
            signal.Level.Should().Be(DigitalLevel.Floating);
        }

        [TestMethod]
        public void Connect_WriteProvider_SignalLevelChanges()
        {
            var provider = new DigitalSignalProvider();
            var signal = new DigitalSignal();
            signal.Connect(provider);

            provider.Write(DigitalLevel.High);

            signal.Level.Should().Be(DigitalLevel.High);
        }

        [TestMethod]
        public void Connect_WriteProvider_ConsumerSeesChanges()
        {
            var provider = new DigitalSignalProvider();
            var consumer = new DigitalSignalConsumer();
            var signal = new DigitalSignal();
            signal.Connect(provider);
            signal.Connect(consumer);

            provider.Write(DigitalLevel.High);

            consumer.Level.Should().Be(DigitalLevel.High);
        }

        [TestMethod]
        public void Write_MultipleConsumers_AllSeeChangedLevel()
        {
            var provider = new DigitalSignalProvider();
            var consumer1 = new DigitalSignalConsumer();
            var consumer2 = new DigitalSignalConsumer();
            var signal = new DigitalSignal();
            signal.Connect(provider);
            signal.Connect(consumer1);
            signal.Connect(consumer2);

            provider.Write(DigitalLevel.High);

            consumer1.Level.Should().Be(DigitalLevel.High);
            consumer2.Level.Should().Be(DigitalLevel.High);
        }

        [TestMethod]
        public void Connect_MultipleProviders_NoConflicts()
        {
            var provider1 = new DigitalSignalProvider();
            var provider2 = new DigitalSignalProvider();
            var signal = new DigitalSignal();
            signal.Connect(provider1);
            signal.Connect(provider2);

            provider1.Write(DigitalLevel.High);
        }

        [TestMethod]
        public void Connect_MultipleProviders_WriteOneAtaTime()
        {
            var provider1 = new DigitalSignalProvider();
            var provider2 = new DigitalSignalProvider();
            var signal = new DigitalSignal();
            signal.Connect(provider1);
            signal.Connect(provider2);

            provider1.Write(DigitalLevel.High);
            provider1.Write(DigitalLevel.Floating);
            provider1.Write(DigitalLevel.Low);
            provider1.Write(DigitalLevel.Floating);
            provider1.Write(DigitalLevel.PosEdge);
        }
    }
}
