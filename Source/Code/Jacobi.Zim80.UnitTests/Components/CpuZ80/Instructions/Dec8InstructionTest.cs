using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.Components.CpuZ80.UnitTests;
using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using System;

namespace Jacobi.Zim80.Components.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class Dec8InstructionTest
    {
        private const UInt16 ExpectedValueHi = (UInt16)0xFF00 | CpuZ80TestExtensions.MagicValue;
        private const UInt16 ExpectedValueLo = CpuZ80TestExtensions.MagicValue - 1;

        [TestMethod]
        public void DecB()
        {
            var ob = OpcodeByte.New(z: 5, y: 0);

            var cpuZ80 = ExecuteTest(ob);

            cpuZ80.AssertRegisters(bc: ExpectedValueHi);
        }

        [TestMethod]
        public void DecC()
        {
            var ob = OpcodeByte.New(z: 5, y: 1);

            var cpuZ80 = ExecuteTest(ob);

            cpuZ80.AssertRegisters(bc: ExpectedValueLo);
        }

        [TestMethod]
        public void DecD()
        {
            var ob = OpcodeByte.New(z: 5, y: 2);

            var cpuZ80 = ExecuteTest(ob);

            cpuZ80.AssertRegisters(de: ExpectedValueHi);
        }

        [TestMethod]
        public void DecE()
        {
            var ob = OpcodeByte.New(z: 5, y: 3);

            var cpuZ80 = ExecuteTest(ob);

            cpuZ80.AssertRegisters(de: ExpectedValueLo);
        }

        [TestMethod]
        public void DecH()
        {
            var ob = OpcodeByte.New(z: 5, y: 4);

            var cpuZ80 = ExecuteTest(ob);

            cpuZ80.AssertRegisters(hl: ExpectedValueHi);
        }

        [TestMethod]
        public void DecL()
        {
            var ob = OpcodeByte.New(z: 5, y: 5);

            var cpuZ80 = ExecuteTest(ob);

            cpuZ80.AssertRegisters(hl: ExpectedValueLo);
        }

        [TestMethod]
        public void DecA()
        {
            var ob = OpcodeByte.New(z: 5, y: 7);

            var cpuZ80 = ExecuteTest(ob);

            cpuZ80.AssertRegisters(a: (byte)ExpectedValueLo);
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
