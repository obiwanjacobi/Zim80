using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System;

namespace Jacobi.Zim80.UnitTests
{
    [TestClass]
    public class DigitalSignalConsumerTest
    {
        [TestMethod]
        public void Unconnected_Level_DoesNotThrow()
        {
            var uut = new DigitalSignalConsumer();

            Action test = () => { var l = uut.Level; };

            test.ShouldNotThrow();
        }

        [TestMethod]
        public void Connected_Level_Floating()
        {
            var uut = new DigitalSignalConsumer();
            uut.ConnectTo(new DigitalSignal());

            uut.Level.Should().Be(DigitalLevel.Floating);
        }

        [TestMethod]
        public void Connected_WithName_BuildName()
        {
            var name = "Test";
            var uut = new DigitalSignalConsumer();
            uut.ConnectTo(new DigitalSignal(name));

            uut.Name.Should().StartWith(name);
        }

        [TestMethod]
        public void Connect_WriteProvider_ConsumerSeesChanges()
        {
            var provider = new DigitalSignalProvider();
            var consumer = new DigitalSignalConsumer();
            var signal = new DigitalSignal();
            provider.ConnectTo(signal);
            consumer.ConnectTo(signal);

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
            provider.ConnectTo(signal);
            consumer1.ConnectTo(signal);
            consumer2.ConnectTo(signal);

            provider.Write(DigitalLevel.High);

            consumer1.Level.Should().Be(DigitalLevel.High);
            consumer2.Level.Should().Be(DigitalLevel.High);
        }

        [TestMethod]
        public void Connect_WriteProvider_EventTriggered()
        {
            var provider = new DigitalSignalProvider();
            var consumer = new DigitalSignalConsumer();
            var signal = new DigitalSignal();
            provider.ConnectTo(signal);
            consumer.ConnectTo(signal);
            consumer.MonitorEvents();

            provider.Write(DigitalLevel.High);

            consumer.ShouldRaise("OnChanged");
        }
    }
}
