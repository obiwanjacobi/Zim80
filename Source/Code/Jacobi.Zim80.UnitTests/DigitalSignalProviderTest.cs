using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System.Collections.Generic;

namespace Jacobi.Zim80.UnitTests
{
    [TestClass]
    public class DigitalSignalProviderTest
    {
        [TestMethod]
        public void Unconnected_Level_IsFLoating()
        {
            var uut = new DigitalSignalProvider();

            uut.Level.Should().Be(DigitalLevel.Floating);
        }

        [TestMethod]
        public void Connected_Level_IsFloating()
        {
            var uut = new DigitalSignalProvider();
            uut.ConnectTo(new DigitalSignal());

            uut.Level.Should().Be(DigitalLevel.Floating);
        }

        [TestMethod]
        public void Connected_WithName_BuildName()
        {
            var name = "Test";
            var uut = new DigitalSignalProvider();
            uut.ConnectTo(new DigitalSignal(name));

            uut.Name.Should().StartWith(name);
        }

        [TestMethod]
        public void Connected_WriteHigh_LevelIsHigh()
        {
            var uut = new DigitalSignalProvider();
            var signal = new DigitalSignal();
            uut.ConnectTo(signal);

            uut.Write(DigitalLevel.High);

            uut.Level.Should().Be(DigitalLevel.High);
            signal.Level.Should().Be(DigitalLevel.High);
        }

        [TestMethod]
        public void Connected_WriteHigh_TriggersEvent()
        {
            var uut = new DigitalSignalProvider();
            var signal = new DigitalSignal();
            uut.ConnectTo(signal);

            signal.MonitorEvents();
            uut.Write(DigitalLevel.High);
            
            signal.ShouldRaise("OnChanged");
            uut.Level.Should().Be(DigitalLevel.High);
            signal.Level.Should().Be(DigitalLevel.High);
        }

        [TestMethod]
        public void Connected_WriteHigh_MovesToHigh()
        {
            var uut = new DigitalSignalProvider();
            var signal = new DigitalSignal();
            uut.ConnectTo(signal);

            var levels = new List<DigitalLevel>();
            signal.OnChanged += (s, e) => levels.Add(e.Level);

            var count = uut.Write(DigitalLevel.High);

            count.Should().Be(3);
            levels.Should().HaveCount(count);
            levels[0].Should().Be(DigitalLevel.Low);
            levels[1].Should().Be(DigitalLevel.PosEdge);
            levels[2].Should().Be(DigitalLevel.High);
        }

        [TestMethod]
        public void MultipleProviders_OneWrites_NoConflicts()
        {
            var provider1 = new DigitalSignalProvider();
            var provider2 = new DigitalSignalProvider();
            var signal = new DigitalSignal();
            provider1.ConnectTo(signal);
            provider2.ConnectTo(signal);

            provider1.Write(DigitalLevel.High);
        }

        [TestMethod]
        public void MultipleProviders_OneWritesAtaTime_NoConflicts()
        {
            var provider1 = new DigitalSignalProvider();
            var provider2 = new DigitalSignalProvider();
            var signal = new DigitalSignal();
            provider1.ConnectTo(signal);
            provider2.ConnectTo(signal);

            provider1.Write(DigitalLevel.High);
            provider1.Write(DigitalLevel.Floating);
            provider2.Write(DigitalLevel.Low);
            provider2.Write(DigitalLevel.Floating);
            provider1.Write(DigitalLevel.PosEdge);
        }
    }
}
