using FluentAssertions;
using Jacobi.Zim80.Components;

namespace Jacobi.Zim80.UnitTests.Components
{
    internal static class BusDataTestExtensions
    {
        public static void AssertAllLevelsAre<T>(this Bus<T> bus, DigitalLevel level)
            where T : BusData, new()
        {
            bus.Value.AssertAllLevelsAre(level);
        }

        public static void AssertAllLevelsAre<T>(this T busData, DigitalLevel level)
            where T : BusData, new()
        {
            for (int i = 0; i < busData.Width; i++)
                busData.Read(i).Should().Be(level);
        }
    }
}
