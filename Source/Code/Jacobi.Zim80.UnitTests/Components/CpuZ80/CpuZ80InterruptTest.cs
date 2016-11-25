using FluentAssertions;
using Jacobi.Zim80.UnitTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jacobi.Zim80.Components.CpuZ80.UnitTests
{
    [TestClass]
    public class CpuZ80InterruptTest
    {
        [TestMethod]
        public void ByDefault_InterruptModeIs0()
        {
            var cpu = new CpuZ80();

            cpu.Registers.Interrupt.InterruptMode.Should().Be(InterruptModes.InterruptMode0);
        }

        [TestMethod]
        public void ByDefault_InterruptIsDisabled()
        {
            var cpu = new CpuZ80();

            cpu.Registers.Interrupt.IFF1.Should().BeFalse();
        }

        [TestMethod]
        public void IntDI_After2Cycles()
        {
            var cpu = new CpuZ80();
            var interruptProvider = cpu.Interrupt.CreateConnection();
            var model = cpu.Initialize(new byte[] { 0 });

            model.ClockGen.SquareWave(2);
            interruptProvider.Write(DigitalLevel.Low);
            model.ClockGen.SquareWave(2);

            cpu.Registers.Interrupt.IFF1.Should().BeFalse();
        }

        [TestMethod]
        public void IntEI_AcceptInterrupt_TurnsOffIFF1()
        {
            var cpu = new CpuZ80();
            cpu.Registers.Interrupt.IFF1 = true;
            var interruptProvider = cpu.Interrupt.CreateConnection();
            var model = cpu.Initialize(new byte[] { 0 });

            // nop
            model.ClockGen.SquareWave(2);
            interruptProvider.Write(DigitalLevel.Low);
            model.ClockGen.SquareWave(2);
            // start interrupt
            model.ClockGen.SquareWave(2);

            // interrupt accepted turns off IFF1
            cpu.Registers.Interrupt.IFF1.Should().BeFalse();
        }
    }
}
