using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using Jacobi.Zim80.Components.CpuZ80.UnitTests;
using System;

namespace Jacobi.Zim80.Components.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class LoadImmediate16InstructionTest
    {
        private readonly OpcodeByte _param1 = new OpcodeByte(0x55);
        private readonly OpcodeByte _param2 = new OpcodeByte(0xAA);

        private const UInt16 ExpectedValue = 0xAA55;

        [TestMethod]
        public void LdBCnn()
        {
            var ob = OpcodeByte.New(z: 1, p: 0);
            
            var cpuZ80 = ExecuteTest(ob, _param1, _param2);

            cpuZ80.AssertRegisters(pc: 3, bc: ExpectedValue);
        }

        [TestMethod]
        public void LdDEnn()
        {
            var ob = OpcodeByte.New(z: 1, p: 1);

            var cpuZ80 = ExecuteTest(ob, _param1, _param2);

            cpuZ80.AssertRegisters(pc: 3, de: ExpectedValue);
        }

        [TestMethod]
        public void LdHLnn()
        {
            var ob = OpcodeByte.New(z: 1, p: 2);

            var cpuZ80 = ExecuteTest(ob, _param1, _param2);

            cpuZ80.AssertRegisters(pc: 3, hl: ExpectedValue);
        }

        [TestMethod]
        public void LdSPnn()
        {
            var ob = OpcodeByte.New(z: 1, p: 3);

            var cpuZ80 = ExecuteTest(ob, _param1, _param2);

            cpuZ80.AssertRegisters(pc: 3, sp: ExpectedValue);
        }

        private static CpuZ80 ExecuteTest(OpcodeByte ob, OpcodeByte parameter1, OpcodeByte parameter2)
        {
            var cpuZ80 = new CpuZ80();
            var model = cpuZ80.Initialize(new byte[] { ob.Value, parameter1.Value, parameter2.Value });

            cpuZ80.FillRegisters();

            model.ClockGen.BlockWave(10);

            return cpuZ80;
        }
    }
}
