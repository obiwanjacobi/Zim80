using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.CpuZ80.Opcodes;
using Jacobi.Zim80.CpuZ80.UnitTests;
using System;
using Jacobi.Zim80.UnitTests;

namespace Jacobi.Zim80.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class LoadImmediate8InstructionTest
    {
        private readonly OpcodeByte _param = new OpcodeByte(0x55);

        private const UInt16 ExpectedValueHi = 0x552A;
        private const byte ExpectedValueLo = 0x55;

        [TestMethod]
        public void LdBn()
        {
            var ob = OpcodeByte.New(z: 6, y: 0);
            
            var cpuZ80 = ExecuteTest(ob, _param);

            cpuZ80.AssertRegisters(pc: 2, bc: ExpectedValueHi);
        }

        [TestMethod]
        public void LdCn()
        {
            var ob = OpcodeByte.New(z: 6, y: 1);

            var cpuZ80 = ExecuteTest(ob, _param);

            cpuZ80.AssertRegisters(pc: 2, bc: ExpectedValueLo);
        }

        [TestMethod]
        public void LdDn()
        {
            var ob = OpcodeByte.New(z: 6, y: 2);

            var cpuZ80 = ExecuteTest(ob, _param);

            cpuZ80.AssertRegisters(pc: 2, de: ExpectedValueHi);
        }

        [TestMethod]
        public void LdEn()
        {
            var ob = OpcodeByte.New(z: 6, y: 3);

            var cpuZ80 = ExecuteTest(ob, _param);

            cpuZ80.AssertRegisters(pc: 2, de: ExpectedValueLo);
        }

        [TestMethod]
        public void LdHn()
        {
            var ob = OpcodeByte.New(z: 6, y: 4);

            var cpuZ80 = ExecuteTest(ob, _param);

            cpuZ80.AssertRegisters(pc: 2, hl: ExpectedValueHi);
        }

        [TestMethod]
        public void LdLn()
        {
            var ob = OpcodeByte.New(z: 6, y: 5);

            var cpuZ80 = ExecuteTest(ob, _param);

            cpuZ80.AssertRegisters(pc: 2, hl: ExpectedValueLo);
        }

        [TestMethod]
        public void LdAn()
        {
            var ob = OpcodeByte.New(z: 6, y: 7);

            var cpuZ80 = ExecuteTest(ob, _param);

            cpuZ80.AssertRegisters(pc: 2, a: ExpectedValueLo);
        }

        [TestMethod]
        public void LdIXhn()
        {
            var ob = OpcodeByte.New(z: 6, y: 4);

            var cpuZ80 = ExecuteTest(ob, _param, 0xDD);

            cpuZ80.AssertRegisters(pc: 3, ix: ExpectedValueHi);
        }

        [TestMethod]
        public void LdIXln()
        {
            var ob = OpcodeByte.New(z: 6, y: 5);

            var cpuZ80 = ExecuteTest(ob, _param, 0xDD);

            cpuZ80.AssertRegisters(pc: 3, ix: ExpectedValueLo);
        }

        [TestMethod]
        public void LdIYhn()
        {
            var ob = OpcodeByte.New(z: 6, y: 4);

            var cpuZ80 = ExecuteTest(ob, _param, 0xFD);

            cpuZ80.AssertRegisters(pc: 3, iy: ExpectedValueHi);
        }

        [TestMethod]
        public void LdIYln()
        {
            var ob = OpcodeByte.New(z: 6, y: 5);

            var cpuZ80 = ExecuteTest(ob, _param, 0xFD);

            cpuZ80.AssertRegisters(pc: 3, iy: ExpectedValueLo);
        }

        private static CpuZ80 ExecuteTest(OpcodeByte ob, OpcodeByte parameter, byte extension = 0)
        {
            var cpuZ80 = new CpuZ80();
            byte[] buffer = (extension == 0) ?
                new byte[] { ob.Value, parameter.Value } :
                new byte[] { extension, ob.Value, parameter.Value };

            var model = cpuZ80.Initialize(buffer);

            cpuZ80.FillRegisters();

            model.ClockGen.SquareWave(extension == 0 ? 7 : 11);

            Console.WriteLine(model.LogicAnalyzer.ToWaveJson());

            return cpuZ80;
        }
    }
}
