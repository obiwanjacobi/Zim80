using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.Components.CpuZ80;
using FluentAssertions;

namespace Jacobi.Zim80.Components.Generators.UnitTests
{
    [TestClass]
    public class SignalGeneratorTest
    {
        private int _lowCount;
        private int _highCount;
        private int _posEdgeCount;
        private int _negEdgeCount;

        [TestInitialize]
        public void TestInitialize()
        {
            _lowCount = 0;
            _highCount = 0;
            _posEdgeCount = 0;
            _negEdgeCount = 0;
        }

        [TestMethod]
        public void BlockWave_ToM1T1Low_CountsAreCorrect()
        {
            ExecuteCountTest(1, CycleNames.T1, DigitalLevel.Low);

            _lowCount.Should().Be(1);
            _highCount.Should().Be(0);
            _posEdgeCount.Should().Be(0);
            _negEdgeCount.Should().Be(0);
        }

        [TestMethod]
        public void BlockWave_ToM1T1PosEdge_CountsAreCorrect()
        {
            ExecuteCountTest(1, CycleNames.T1, DigitalLevel.PosEdge);

            _lowCount.Should().Be(1);
            _highCount.Should().Be(0);
            _posEdgeCount.Should().Be(1);
            _negEdgeCount.Should().Be(0);
        }

        [TestMethod]
        public void BlockWave_ToM1T1High_CountsAreCorrect()
        {
            ExecuteCountTest(1, CycleNames.T1, DigitalLevel.High);

            _lowCount.Should().Be(1);
            _highCount.Should().Be(1);
            _posEdgeCount.Should().Be(1);
            _negEdgeCount.Should().Be(0);
        }

        [TestMethod]
        public void BlockWave_ToM1T1NegEdge_CountsAreCorrect()
        {
            ExecuteCountTest(1, CycleNames.T1, DigitalLevel.NegEdge);

            _lowCount.Should().Be(1);
            _highCount.Should().Be(1);
            _posEdgeCount.Should().Be(1);
            _negEdgeCount.Should().Be(1);
        }

        [TestMethod]
        public void BlockWave_ToM1T2PosEdge_CountsAreCorrect()
        {
            ExecuteCountTest(1, CycleNames.T2, DigitalLevel.PosEdge);

            _lowCount.Should().Be(2);
            _highCount.Should().Be(1);
            _posEdgeCount.Should().Be(2);
            _negEdgeCount.Should().Be(1);
        }

        [TestMethod]
        public void BlockWave_ToM1T2NegEdge_CountsAreCorrect()
        {
            ExecuteCountTest(1, CycleNames.T2, DigitalLevel.NegEdge);

            _lowCount.Should().Be(2);
            _highCount.Should().Be(2);
            _posEdgeCount.Should().Be(2);
            _negEdgeCount.Should().Be(2);
        }

        [TestMethod]
        public void BlockWave_ToM1T4PosEdge_CountsAreCorrect()
        {
            ExecuteCountTest(1, CycleNames.T4, DigitalLevel.PosEdge);

            _lowCount.Should().Be(4);
            _highCount.Should().Be(3);
            _posEdgeCount.Should().Be(4);
            _negEdgeCount.Should().Be(3);
        }

        [TestMethod]
        public void BlockWave_ToM1T4NegEdge_CountsAreCorrect()
        {
            ExecuteCountTest(1, CycleNames.T4, DigitalLevel.NegEdge);

            _lowCount.Should().Be(4);
            _highCount.Should().Be(4);
            _posEdgeCount.Should().Be(4);
            _negEdgeCount.Should().Be(4);
        }

        [TestMethod]
        public void BlockWave_ToM2T4PosEdge_CountsAreCorrect()
        {
            ExecuteCountTest(2, CycleNames.T4, DigitalLevel.PosEdge);

            _lowCount.Should().Be(8);
            _highCount.Should().Be(7);
            _posEdgeCount.Should().Be(8);
            _negEdgeCount.Should().Be(7);
        }

        [TestMethod]
        public void BlockWave_ToM2T4NegEdge_CountsAreCorrect()
        {
            ExecuteCountTest(2, CycleNames.T4, DigitalLevel.NegEdge);

            _lowCount.Should().Be(8);
            _highCount.Should().Be(8);
            _posEdgeCount.Should().Be(8);
            _negEdgeCount.Should().Be(8);
        }

        private void ExecuteCountTest(int machineCycles, CycleNames toCycle, DigitalLevel toLevel)
        {
            var gen = new SignalGenerator();
            var ds = new DigitalSignal("GenOut");
            ds.OnChanged += Output_OnChanged;
            gen.Output.ConnectTo(ds);

            gen.BlockWave(machineCycles, toCycle, toLevel);
        }

        private void Output_OnChanged(object sender, DigitalLevelChangedEventArgs e)
        {
            switch (e.Level)
            {
                case DigitalLevel.Low:
                    _lowCount++;
                    break;
                case DigitalLevel.PosEdge:
                    _posEdgeCount++;
                    break;
                case DigitalLevel.High:
                    _highCount++;
                    break;
                case DigitalLevel.NegEdge:
                    _negEdgeCount++;
                    break;
            }
        }
    }
}
