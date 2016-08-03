using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using Jacobi.Zim80.Components.CpuZ80.UnitTests;
using System;

namespace Jacobi.Zim80.Components.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class LoadImmediate8InstructionTest
    {
        private readonly OpcodeByte _param = new OpcodeByte(0x55);

        private const UInt16 ExpectedValueHi = 0x552A;
        private const UInt16 ExpectedValueLo = 0x0055;

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

            cpuZ80.AssertRegisters(pc: 2, af: ExpectedValueHi);
        }

        private static CpuZ80 ExecuteTest(OpcodeByte ob, OpcodeByte parameter)
        {
            var cpuZ80 = new CpuZ80();
            var model = cpuZ80.Initialize(new byte[] { ob.Value, parameter.Value });

            cpuZ80.FillRegisters();

            model.ClockGen.BlockWave(7);

            return cpuZ80;
        }
    }
}
