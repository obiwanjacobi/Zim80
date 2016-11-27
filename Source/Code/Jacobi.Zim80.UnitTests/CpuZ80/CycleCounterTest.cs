using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jacobi.Zim80.CpuZ80.UnitTests
{
    [TestClass]
    public class CycleCounterTest
    {
        [TestMethod]
        public void OnClock_OnePosEdge_T1()
        {
            var counter = new CycleCounter();
            counter.OnClock(DigitalLevel.PosEdge);

            counter.CycleName.Should().Be(CycleNames.T1);
            counter.MachineCycle.Should().Be(MachineCycleNames.M1);
        }

        [TestMethod]
        public void OnClock_OnePosOneNegEdge_T1()
        {
            var counter = new CycleCounter();
            counter.OnClock(DigitalLevel.PosEdge);
            counter.OnClock(DigitalLevel.NegEdge);

            counter.CycleName.Should().Be(CycleNames.T1);
            counter.MachineCycle.Should().Be(MachineCycleNames.M1);
        }

        [TestMethod]
        public void OnClock_TwoPosEdges_T2()
        {
            var counter = new CycleCounter();
            counter.OnClock(DigitalLevel.PosEdge);
            counter.OnClock(DigitalLevel.PosEdge);

            counter.CycleName.Should().Be(CycleNames.T2);
            counter.MachineCycle.Should().Be(MachineCycleNames.M1);
        }

        [TestMethod]
        public void OnClock_TwoPosTwoNegEdges_T2()
        {
            var counter = new CycleCounter();
            counter.OnClock(DigitalLevel.PosEdge);
            counter.OnClock(DigitalLevel.NegEdge);
            counter.OnClock(DigitalLevel.PosEdge);
            counter.OnClock(DigitalLevel.NegEdge);

            counter.CycleName.Should().Be(CycleNames.T2);
            counter.MachineCycle.Should().Be(MachineCycleNames.M1);
        }

        [TestMethod]
        public void OnClock_FourPosEdges_T4()
        {
            var counter = new CycleCounter();
            counter.OnClock(DigitalLevel.PosEdge);
            counter.OnClock(DigitalLevel.PosEdge);
            counter.OnClock(DigitalLevel.PosEdge);
            counter.OnClock(DigitalLevel.PosEdge);

            counter.CycleName.Should().Be(CycleNames.T4);
            counter.MachineCycle.Should().Be(MachineCycleNames.M1);
        }
    }
}
