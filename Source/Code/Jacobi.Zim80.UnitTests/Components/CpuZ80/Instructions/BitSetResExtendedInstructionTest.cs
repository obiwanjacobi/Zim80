using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using Jacobi.Zim80.Model;
using Jacobi.Zim80.Components.CpuZ80.UnitTests;
using FluentAssertions;
using Jacobi.Zim80.Components.Memory.UnitTests;

namespace Jacobi.Zim80.Components.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class BitSetResExtendedInstructionTest
    {
        private const ushort Address = 0x05;
        private const byte Offset = 0xFF;   //-1
        private const byte ExpectedAddress = 0x04;

        private const byte IX = 0xDD;
        private const byte IY = 0xFD;

        [TestMethod]
        public void BIT_IXd_z()
        {
            ExecuteTestAllRegs(1, IX, false, (reg, bit) => 0, true);
        }

        [TestMethod]
        public void BIT_IXd_nz()
        {
            ExecuteTestAllRegs(1, IX, true, (reg, bit) => 0xFF, false);
        }

        [TestMethod]
        public void RES_IXd_z()
        {
            ExecuteTestAllRegs(2, IX, false, (reg, bit) => 0, false);
        }

        [TestMethod]
        public void RES_IXd_nz()
        {
            ExecuteTestAllRegs(2, IX, true, (reg, bit) => (byte)~(1 << bit), false);
        }

        [TestMethod]
        public void SET_IXd_z()
        {
            ExecuteTestAllRegs(3, IX, false, (reg, bit) => (byte)(1 << bit), false);
        }

        [TestMethod]
        public void SET_IXd_nz()
        {
            ExecuteTestAllRegs(3, IX, true, (reg, bit) => 0xFF, false);
        }

        [TestMethod]
        public void BIT_IYd_z()
        {
            ExecuteTestAllRegs(1, IY, false, (reg, bit) => 0, true);
        }

        [TestMethod]
        public void BIT_IYd_nz()
        {
            ExecuteTestAllRegs(1, IY, true, (reg, bit) => 0xFF, false);
        }

        [TestMethod]
        public void RES_IYd_z()
        {
            ExecuteTestAllRegs(2, IY, false, (reg, bit) => 0, false);
        }

        [TestMethod]
        public void RES_IYd_nz()
        {
            ExecuteTestAllRegs(2, IY, true, (reg, bit) => (byte)~(1 << bit), false);
        }

        [TestMethod]
        public void SET_IYd_z()
        {
            ExecuteTestAllRegs(3, IY, false, (reg, bit) => (byte)(1 << bit), false);
        }

        [TestMethod]
        public void SET_IYd_nz()
        {
            ExecuteTestAllRegs(3, IY, true, (reg, bit) => 0xFF, false);
        }

        private void ExecuteTestAllRegs(byte x, byte extension, bool bitValue,
            Func<Register8Table, byte, byte> calcExpected, bool expectedZero)
        {
            ForEachRegister((reg) =>
            {
                for (int bit = 0; bit < 8; bit++)
                {
                    ExecuteTest(x, extension, bitValue, 
                        calcExpected, expectedZero, reg, bit);
                }
            });
        }

        private static void ExecuteTest(byte x, byte extension, bool bitValue, 
            Func<Register8Table, byte, byte> calcExpected, bool expectedZero, 
            Register8Table reg, int bit)
        {
            var ob = OpcodeByte.New(x: x, z: (byte)reg, y: (byte)bit);
            var model = ExecuteTest(ob, (byte)(bitValue ? 0xFF : 0), (cpu) =>
            {
                if (extension == IX)
                    cpu.Registers.IX = Address;
                if (extension == IY)
                    cpu.Registers.IY = Address;

            }, extension);

            var expectedValue = calcExpected(reg, (byte)bit);

            if (reg != Register8Table.HL && x != 1 /*bit*/)
                model.Cpu.Registers.PrimarySet[reg].Should().Be(expectedValue,
                    "bit: " + bit + " reg: " + reg.ToString());

            model.Memory.Assert(ExpectedAddress, expectedValue);
            model.Cpu.Registers.PrimarySet.Flags.Z.Should().Be(expectedZero);
        }

        private static void ForEachRegister(Action<Register8Table> action)
        {
            for (Register8Table reg = Register8Table.B; reg <= Register8Table.A; reg++)
            {
                action(reg);
            }
        }

        private static SimulationModel ExecuteTest(OpcodeByte ob, byte value, Action<CpuZ80> preTest, byte extension)
        {
            var cpuZ80 = new CpuZ80();
            byte[] buffer = new byte[] { extension, 0xCB, Offset, ob.Value, value };
            var model = cpuZ80.Initialize(buffer);

            cpuZ80.FillRegisters();
            preTest(cpuZ80);

            model.ClockGen.BlockWave(ob.X == 1 ? 20 : 23);

            return model;
        }
    }
}
