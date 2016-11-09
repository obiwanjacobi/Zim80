using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using Jacobi.Zim80.Components.CpuZ80.UnitTests;
using Jacobi.Zim80.Model;
using FluentAssertions;
using Jacobi.Zim80.Components.Memory;
using Jacobi.Zim80.Components.Memory.UnitTests;

namespace Jacobi.Zim80.Components.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class IndirectBitSetResInstructionTest
    {
        private const ushort AddressHL = 5;

        [TestMethod]
        public void BIT_HL_n_False()
        {
            BIT_TestAll(false);
        }

        [TestMethod]
        public void BIT_HL_n_true()
        {
            BIT_TestAll(true);
        }

        [TestMethod]
        public void RES_HL_n_False()
        {
            RES_TestAll(false);
        }

        [TestMethod]
        public void RES_HL_n_true()
        {
            RES_TestAll(true);
        }

        [TestMethod]
        public void SET_HL_n_False()
        {
            SET_TestAll(false);
        }

        [TestMethod]
        public void SET_HL_n_true()
        {
            SET_TestAll(true);
        }

        private static void BIT_TestAll(bool bitValue)
        {
            for (byte bit = 0; bit < 8; bit++)
            {
                var value = CreateValue(bit, bitValue);
                var model = ExecuteTest(1, bit, value);

                model.Cpu.AssertRegisters(hl: AddressHL);
                model.Cpu.Registers.Flags.Z.Should().Be(!bitValue);
            }
        }

        private static void RES_TestAll(bool bitValue)
        {
            for (byte bit = 0; bit < 8; bit++)
            {
                var value = CreateValue(bit, bitValue);
                var model = ExecuteTest(2, bit, value);

                model.Cpu.AssertRegisters(hl: AddressHL);
                model.Memory.Assert(AddressHL, (byte)(value & ~(1 << bit)));
            }
        }

        private static void SET_TestAll(bool bitValue)
        {
            for (byte bit = 0; bit < 8; bit++)
            {
                var value = CreateValue(bit, bitValue);
                var model = ExecuteTest(3, bit, value);

                model.Cpu.AssertRegisters(hl: AddressHL);
                model.Memory.Assert(AddressHL, (byte)(value | (1 << bit)));
            }
        }

        private static byte CreateValue(byte bit, bool bitValue)
        {
            var value = (1 << bit);
            if (!bitValue) value = ~value;
            return (byte)value;
        }

        private static SimulationModel ExecuteTest(byte x, byte bit, byte value)
        {
            var ob = OpcodeByte.New(x: x, y: bit, z: 6);
            return ExecuteTest(ob, value);
        }

        private static SimulationModel ExecuteTest(OpcodeByte ob, byte value)
        {
            var cpuZ80 = new CpuZ80();
            byte[] buffer = new byte[] { 0xCB, ob.Value, 0, 0, 0, value };
            var model = cpuZ80.Initialize(buffer);

            cpuZ80.FillRegisters(hl: AddressHL);

            model.ClockGen.BlockWave(ob.X == 1 ? 12 : 15);

            return model;
        }
    }
}
