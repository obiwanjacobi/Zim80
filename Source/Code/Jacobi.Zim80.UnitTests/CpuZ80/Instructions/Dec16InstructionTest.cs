using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.CpuZ80.Opcodes;
using Jacobi.Zim80.CpuZ80.UnitTests;

namespace Jacobi.Zim80.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class Dec16InstructionTest
    {
        private const int ExpectedValue = CpuZ80TestExtensions.MagicValue - 1;

        [TestMethod]
        public void DecBC()
        {
            var ob = OpcodeByte.New(z: 3, q: 1, p: 0);

            var cpuZ80 = ExecuteTest(ob);

            cpuZ80.AssertRegisters(bc: ExpectedValue);
        }

        [TestMethod]
        public void DecDE()
        {
            var ob = OpcodeByte.New(z: 3, q: 1, p: 1);

            var cpuZ80 = ExecuteTest(ob);

            cpuZ80.AssertRegisters(de: ExpectedValue);
        }

        [TestMethod]
        public void DecHL()
        {
            var ob = OpcodeByte.New(z: 3, q: 1, p: 2);

            var cpuZ80 = ExecuteTest(ob);

            cpuZ80.AssertRegisters(hl: ExpectedValue);
        }

        [TestMethod]
        public void DecSP()
        {
            var ob = OpcodeByte.New(z: 3, q: 1, p: 3);

            var cpuZ80 = ExecuteTest(ob);

            cpuZ80.AssertRegisters(sp: ExpectedValue);
        }

        [TestMethod]
        public void DecIX()
        {
            var ob = OpcodeByte.New(z: 3, q: 1, p: 2);

            var cpuZ80 = ExecuteTest(ob, 0xDD);

            cpuZ80.AssertRegisters(ix: ExpectedValue, pc: 2);
        }

        [TestMethod]
        public void DecIY()
        {
            var ob = OpcodeByte.New(z: 3, q: 1, p: 2);

            var cpuZ80 = ExecuteTest(ob, 0xFD);

            cpuZ80.AssertRegisters(iy: ExpectedValue, pc: 2);
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

            model.ClockGen.SquareWave(extension == 0 ? 6 : 10);

            return cpuZ80;
        }
    }
}
