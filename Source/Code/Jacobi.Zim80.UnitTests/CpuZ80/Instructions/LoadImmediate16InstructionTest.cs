using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.CpuZ80.Opcodes;
using Jacobi.Zim80.CpuZ80.UnitTests;
using System;

namespace Jacobi.Zim80.CpuZ80.Instructions.UnitTests
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

        [TestMethod]
        public void LdIXnn()
        {
            var ob = OpcodeByte.New(z: 1, p: 2);

            var cpuZ80 = ExecuteTest(ob, _param1, _param2, 0xDD);

            cpuZ80.AssertRegisters(pc: 4, ix: ExpectedValue);
        }

        [TestMethod]
        public void LdIYnn()
        {
            var ob = OpcodeByte.New(z: 1, p: 2);

            var cpuZ80 = ExecuteTest(ob, _param1, _param2, 0xFD);

            cpuZ80.AssertRegisters(pc: 4, iy: ExpectedValue);
        }

        private static CpuZ80 ExecuteTest(OpcodeByte ob, OpcodeByte parameter1, OpcodeByte parameter2, byte extension = 0)
        {
            var cpuZ80 = new CpuZ80();
            byte[] buffer;

            if (extension == 0)
                buffer = new byte[] { ob.Value, parameter1.Value, parameter2.Value };
            else
                buffer = new byte[] { extension, ob.Value, parameter1.Value, parameter2.Value };

            var model = cpuZ80.Initialize(buffer);

            cpuZ80.FillRegisters();

            model.ClockGen.SquareWave(extension == 0 ? 10 : 14);

            return cpuZ80;
        }
    }
}
