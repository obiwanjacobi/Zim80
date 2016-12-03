using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.UnitTests;
using FluentAssertions;
using Jacobi.Zim80.Test;

namespace Jacobi.Zim80.CpuZ80.UnitTests
{
    [TestClass]
    public class CpuZ80NmiTest
    {
        [TestMethod]
        public void InterruptIsLatched()
        {
            var cpu = new CpuZ80();
            cpu.Registers.Interrupt.IFF1 = true;
            var interruptProvider = cpu.NonMaskableInterrupt.CreateConnection();
            var model = cpu.Initialize(new byte[] { 0 });

            model.ClockGen.SquareWave(2);
            interruptProvider.Write(DigitalLevel.NegEdge);
            model.ClockGen.SquareWave(2);

            // into nmi
            model.ClockGen.SquareWave(11);

            // nmi turns off interrupts
            cpu.Registers.Interrupt.IFF1.Should().BeFalse();
        }
    }
}
