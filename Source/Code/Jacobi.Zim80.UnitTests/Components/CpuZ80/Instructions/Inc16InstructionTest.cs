using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.Components.CpuZ80.UnitTests;
using Jacobi.Zim80.Components.CpuZ80.Opcodes;

namespace Jacobi.Zim80.Components.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class Inc16InstructionTest
    {
        private const int ExpectedValue = CpuZ80TestExtensions.MagicValue + 1;

        [TestMethod]
        public void IncBC()
        {
            var ob = OpcodeByte.New(z: 3, p: 0);

            var cpuZ80 = ExecuteTest(ob);

            cpuZ80.AssertRegisters(bc: ExpectedValue);
        }

        [TestMethod]
        public void IncDE()
        {
            var ob = OpcodeByte.New(z: 3, p: 1);

            var cpuZ80 = ExecuteTest(ob);

            cpuZ80.AssertRegisters(de: ExpectedValue);
        }

        [TestMethod]
        public void IncHL()
        {
            var ob = OpcodeByte.New(z: 3, p: 2);

            var cpuZ80 = ExecuteTest(ob);

            cpuZ80.AssertRegisters(hl: ExpectedValue);
        }

        [TestMethod]
        public void IncSP()
        {
            var ob = OpcodeByte.New(z: 3, p: 3);

            var cpuZ80 = ExecuteTest(ob);

            cpuZ80.AssertRegisters(sp: ExpectedValue);
        }

        private static CpuZ80 ExecuteTest(OpcodeByte ob)
        {
            var cpuZ80 = new CpuZ80();
            var model = cpuZ80.Initialize(new byte[] { ob.Value });

            cpuZ80.FillRegisters();

            model.ClockGen.BlockWave(6);

            return cpuZ80;
        }
    }
}
