using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.CpuZ80.UnitTests;
using Jacobi.Zim80.CpuZ80.Opcodes;
using System;
using Jacobi.Zim80.UnitTests;
using Jacobi.Zim80.Diagnostics;

namespace Jacobi.Zim80.CpuZ80.Instructions.UnitTests
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

            cpuZ80.AssertRegisters(a: (byte)ExpectedValueLo);
        }

        [TestMethod]
        public void IncIXh()
        {
            var ob = OpcodeByte.New(z: 4, y: 4);

            var cpuZ80 = ExecuteTest(ob, 0xDD);

            cpuZ80.AssertRegisters(ix: ExpectedValueHi);
        }

        [TestMethod]
        public void IncIXl()
        {
            var ob = OpcodeByte.New(z: 4, y: 5);

            var cpuZ80 = ExecuteTest(ob, 0xDD);

            cpuZ80.AssertRegisters(ix: ExpectedValueLo);
        }

        [TestMethod]
        public void IncIYh()
        {
            var ob = OpcodeByte.New(z: 4, y: 4);

            var cpuZ80 = ExecuteTest(ob, 0xFD);

            cpuZ80.AssertRegisters(iy: ExpectedValueHi);
        }

        [TestMethod]
        public void IncIYl()
        {
            var ob = OpcodeByte.New(z: 4, y: 5);

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
