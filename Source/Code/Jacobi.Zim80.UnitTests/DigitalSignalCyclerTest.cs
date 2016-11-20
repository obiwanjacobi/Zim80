using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jacobi.Zim80.UnitTests
{
    [TestClass]
    public class DigitalSignalCyclerTest
    {
        [TestMethod]
        public void Cycler_Default_StartsLow()
        {
            var uut = new DigitalLevelCycler();

            DigitalLevel? firstLevel = null;

            foreach (var level in uut)
            {
                firstLevel = level;
                break;
            }

            firstLevel.Should().Be(DigitalLevel.Low);
        }

        [TestMethod]
        public void Cycler_StartLow_StartsLow()
        {
            var uut = new DigitalLevelCycler(DigitalLevel.Low);

            DigitalLevel? firstLevel = null;

            foreach (var level in uut)
            {
                firstLevel = level;
                break;
            }

            firstLevel.Should().Be(DigitalLevel.Low);
        }

        [TestMethod]
        public void Cycler_StartLowEndHigh_StartsLowEndsHigh()
        {
            var uut = new DigitalLevelCycler(DigitalLevel.Low, DigitalLevel.High);

            DigitalLevel? firstLevel = null;
            DigitalLevel lastLevel = DigitalLevel.Floating;

            foreach (var level in uut)
            {
                if (firstLevel == null)
                    firstLevel = level;

                lastLevel = level;
            }

            firstLevel.Should().Be(DigitalLevel.Low);
            lastLevel.Should().Be(DigitalLevel.High);
        }

        [TestMethod]
        public void Cycler_StartLowEndHigh_Counts3()
        {
            var uut = new DigitalLevelCycler(DigitalLevel.Low, DigitalLevel.High);

            int count = 0;
            foreach (var level in uut)
            {
                count++;
            }

            count.Should().Be(3);
        }

        [TestMethod]
        public void Cycler_AbortAfter3Cycles_StartsAndEndsLow()
        {
            var uut = new DigitalLevelCycler(DigitalLevel.Low);

            DigitalLevel? firstLevel = null;
            DigitalLevel lastLevel = DigitalLevel.Floating;

            foreach (var level in uut)
            {
                if (firstLevel == null)
                    firstLevel = level;

                lastLevel = level;

                if (uut.CycleCount == 3)
                    uut.Abort();
            }

            firstLevel.Should().Be(DigitalLevel.Low);
            lastLevel.Should().Be(DigitalLevel.Low);
        }
    }
}
