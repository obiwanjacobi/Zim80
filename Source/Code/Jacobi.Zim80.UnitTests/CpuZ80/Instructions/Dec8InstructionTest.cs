using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.CpuZ80.UnitTests;
using Jacobi.Zim80.CpuZ80.Opcodes;
using System;
using Jacobi.Zim80.UnitTests;

namespace Jacobi.Zim80.CpuZ80.Instructions.UnitTests
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

        [TestMethod]
        public void DecIXh()
        {
            var ob = OpcodeByte.New(z: 5, y: 4);

            var cpuZ80 = ExecuteTest(ob, 0xDD);

            cpuZ80.AssertRegisters(ix: ExpectedValueHi);
        }

        [TestMethod]
        public void DecIXl()
        {
            var ob = OpcodeByte.New(z: 5, y: 5);

            var cpuZ80 = ExecuteTest(ob, 0xDD);

            cpuZ80.AssertRegisters(ix: ExpectedValueLo);
        }

        [TestMethod]
        public void DecIYh()
        {
            var ob = OpcodeByte.New(z: 5, y: 4);

            var cpuZ80 = ExecuteTest(ob, 0xFD);

            cpuZ80.AssertRegisters(iy: ExpectedValueHi);
        }

        [TestMethod]
        public void DecIYl()
        {
            var ob = OpcodeByte.New(z: 5, y: 5);

            var cpuZ80 = ExecuteTest(ob, 0xFD);

            cpuZ80.AssertRegisters(iy: ExpectedValueLo);
        }

        private static CpuZ80 ExecuteTest(OpcodeByte ob, byte extension = 0)
        {
            var cpuZ80 = new CpuZ80();
            byte[] buffer = (extension == 0) ?
                new byte[] { ob.Value } :
                new byte[] { extension, ob.Value };

            var model = cpuZ80.Initialize(buffer);

            cpuZ80.FillRegisters();

            model.ClockGen.SquareWave(extension == 0 ? 4 : 8);

            Console.WriteLine(model.LogicAnalyzer.ToWaveJson());

            return cpuZ80;
        }
    }
}
