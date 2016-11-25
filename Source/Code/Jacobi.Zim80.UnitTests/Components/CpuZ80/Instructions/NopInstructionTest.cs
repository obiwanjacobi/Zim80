using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.Components.CpuZ80.UnitTests;

namespace Jacobi.Zim80.Components.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class NopInstructionTest
    {
        [TestMethod]
        public void Nop()
        {
            var cpuZ80 = new CpuZ80();
            var model = cpuZ80.Initialize(new byte[] { 0x00 });

            cpuZ80.FillRegisters(pc: 0);

            model.ClockGen.SquareWave(4);

            cpuZ80.AssertRegisters(pc: 1);
        }
    }
}
