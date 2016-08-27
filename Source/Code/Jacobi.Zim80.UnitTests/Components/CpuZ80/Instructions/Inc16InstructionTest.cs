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

        [TestMethod]
        public void IncIX()
        {
            var ob = OpcodeByte.New(z: 3, p: 2);

            var cpuZ80 = ExecuteTest(ob, 0xDD);

            cpuZ80.AssertRegisters(ix: ExpectedValue);
        }

        [TestMethod]
        public void IncIY()
        {
            var ob = OpcodeByte.New(z: 3, p: 2);

            var cpuZ80 = ExecuteTest(ob, 0xFD);

            cpuZ80.AssertRegisters(iy: ExpectedValue);
        }

        private static CpuZ80 ExecuteTest(OpcodeByte ob, byte extension = 0)
        {
            var cpuZ80 = new CpuZ80();
            byte[] buffer;

            if (extension == 0)
                buffer = new byte[] { ob.Value };
            else
                buffer = new byte[] { extension, ob.Value };

            var model = cpuZ80.Initialize(buffer);

            cpuZ80.FillRegisters();

            model.ClockGen.BlockWave(extension == 0 ? 6 : 10);

            return cpuZ80;
        }
    }
}
