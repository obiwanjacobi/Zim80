using FluentAssertions;
using Jacobi.Zim80.UnitTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jacobi.Zim80.Components.CpuZ80.UnitTests
{
    [TestClass]
    public class CpuZ80InterruptTest
    {
        [TestMethod]
        public void IntDI_After2Cycles()
        {
            var cpu = new CpuZ80();
            var interruptProvider = cpu.Interrupt.CreateConnection();
            var model = cpu.Initialize(new byte[] { 0 });

            model.ClockGen.BlockWave(2);
            interruptProvider.Write(DigitalLevel.Low);
            model.ClockGen.BlockWave(2);

            cpu.Registers.Interrupt.ActiveInterrupt.Should().Be(null);
        }

        [TestMethod]
        public void IntEI_After2Cycles()
        {
            var cpu = new CpuZ80();
            cpu.Registers.Interrupt.EnableInterrupt();
            var interruptProvider = cpu.Interrupt.CreateConnection();
            var model = cpu.Initialize(new byte[] { 0 });

            // nop
            model.ClockGen.BlockWave(2);
            interruptProvider.Write(DigitalLevel.Low);
            model.ClockGen.BlockWave(2);
            // start interrupt
            model.ClockGen.BlockWave(2);

            cpu.Registers.Interrupt.ActiveInterrupt.Should().Be(InterruptTypes.Int0);
        }
    }
}
