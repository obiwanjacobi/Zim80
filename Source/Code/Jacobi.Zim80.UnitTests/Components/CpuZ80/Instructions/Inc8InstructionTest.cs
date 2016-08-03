using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.Components.CpuZ80.UnitTests;
using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using System;

namespace Jacobi.Zim80.Components.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class Inc8InstructionTest
    {
        private const UInt16 ExpectedValueHi = 0x0100 | CpuZ80TestExtensions.MagicValue;
        private const UInt16 ExpectedValueLo = CpuZ80TestExtensions.MagicValue + 1;

        [TestMethod]
        public void IncB()
        {
            var ob = OpcodeByte.New(z: 4, y: 0);

            var cpuZ80 = ExecuteTest(ob);

            cpuZ80.AssertRegisters(bc: ExpectedValueHi);
        }

        [TestMethod]
        public void IncC()
        {
            var ob = OpcodeByte.New(z: 4, y: 1);

            var cpuZ80 = ExecuteTest(ob);

            cpuZ80.AssertRegisters(bc: ExpectedValueLo);
        }

        [TestMethod]
        public void IncD()
        {
            var ob = OpcodeByte.New(z: 4, y: 2);

            var cpuZ80 = ExecuteTest(ob);

            cpuZ80.AssertRegisters(de: ExpectedValueHi);
        }

        [TestMethod]
        public void IncE()
        {
            var ob = OpcodeByte.New(z: 4, y: 3);

            var cpuZ80 = ExecuteTest(ob);

            cpuZ80.AssertRegisters(de: ExpectedValueLo);
        }

        [TestMethod]
        public void IncH()
        {
            var ob = OpcodeByte.New(z: 4, y: 4);

            var cpuZ80 = ExecuteTest(ob);

            cpuZ80.AssertRegisters(hl: ExpectedValueHi);
        }

        [TestMethod]
        public void IncL()
        {
            var ob = OpcodeByte.New(z: 4, y: 5);

            var cpuZ80 = ExecuteTest(ob);

            cpuZ80.AssertRegisters(hl: ExpectedValueLo);
        }

        [TestMethod]
        public void IncA()
        {
            var ob = OpcodeByte.New(z: 4, y: 7);

            var cpuZ80 = ExecuteTest(ob);

            cpuZ80.AssertRegisters(af: ExpectedValueHi);
        }

        private static CpuZ80 ExecuteTest(OpcodeByte ob)
        {
            var cpuZ80 = new CpuZ80();
            var model = cpuZ80.Initialize(new byte[] { ob.Value });

            cpuZ80.FillRegisters();

            model.ClockGen.BlockWave(4);

            return cpuZ80;
        }
    }
}
