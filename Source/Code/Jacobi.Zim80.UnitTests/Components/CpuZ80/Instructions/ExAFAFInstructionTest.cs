using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using Jacobi.Zim80.Components.CpuZ80.States.Instructions;
using Jacobi.Zim80.Components.CpuZ80.UnitTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Jacobi.Zim80.Components.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class ExAFAFInstructionTest
    {
        [TestMethod]
        public void ExAFAF()
        {
            var od = OpcodeDefinition.FindAll(typeof(ExAFAFInstruction)).FirstOrDefault();

            var cpuZ80 = new CpuZ80();
            var model = cpuZ80.Initialize(new byte[] { od.Value });

            cpuZ80.FillRegisters(a: 0x55, a_a: 0xAA);

            model.ClockGen.BlockWave(4);

            cpuZ80.AssertRegisters(a: 0xAA, a_a: 0x55);
        }
    }
}
