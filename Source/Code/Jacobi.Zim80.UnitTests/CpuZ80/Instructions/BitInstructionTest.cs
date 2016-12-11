﻿using FluentAssertions;
using Jacobi.Zim80.CpuZ80.Opcodes;
using Jacobi.Zim80.CpuZ80.UnitTests;
using Jacobi.Zim80.Diagnostics;
using Jacobi.Zim80.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Jacobi.Zim80.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class BitInstructionTest
    {
        [TestMethod]
        public void Bit0Bz()
        {
            var cpu = ExecuteTest(0, Register8Table.B, 
                (m) => m.Cpu.FillRegisters(bc:0));

            cpu.AssertRegisters(
                pc: 2,
                bc: 0);
        }

        [TestMethod]
        public void Bit7Bz()
        {
            var cpu = ExecuteTest(7, Register8Table.B,
                (m) => m.Cpu.FillRegisters(bc: 0x80));

            cpu.AssertRegisters(
                pc: 2,
                bc: 0x80);
        }

        [TestMethod]
        public void TestAll_z()
        {
            TestAll(false);
        }

        [TestMethod]
        public void TestAll_nz()
        {
            TestAll(true);
        }

        private void TestAll(bool bitValue)
        {
            for (Register8Table reg = Register8Table.B; reg <= Register8Table.A; reg++)
            {
                if (reg == Register8Table.HL) continue;
                for (byte bit = 0; bit < 8; bit++)
                {
                    var value = CreateValue(bit, reg, bitValue);
                    var cpu = ExecuteTest(bit, reg, (m) => FillRegister(m.Cpu, reg, value));

                    AssertRegisters(cpu, reg, value);
                    cpu.Registers.Flags.Z.Should().Be(!bitValue);
                }
            }
        }

        private void AssertRegisters(CpuZ80 cpu, Register8Table reg, ushort value)
        {
            switch (reg)
            {
                case Register8Table.B:
                case Register8Table.C:
                    cpu.AssertRegisters(pc: 2, bc: value);
                    break;
                case Register8Table.D:
                case Register8Table.E:
                    cpu.AssertRegisters(pc: 2, de: value);
                    break;
                case Register8Table.H:
                case Register8Table.L:
                    cpu.AssertRegisters(pc: 2, hl: value);
                    break;
                case Register8Table.A:
                    cpu.AssertRegisters(pc: 2, a: (byte)value);
                    break;
            }
        }

        private void FillRegister(CpuZ80 cpu, Register8Table reg, ushort value)
        {
            switch (reg)
            {
                case Register8Table.B:
                case Register8Table.C:
                    cpu.FillRegisters(bc: value);
                    break;
                case Register8Table.D:
                case Register8Table.E:
                    cpu.FillRegisters(de: value);
                    break;
                case Register8Table.H:
                case Register8Table.L:
                    cpu.FillRegisters(hl: value);
                    break;
                case Register8Table.A:
                    cpu.FillRegisters(a: (byte)value);
                    break;
            }
        }

        private static ushort CreateValue(byte bit, Register8Table reg, bool bitValue)
        {
            var regValue = (1 << bit);
            if (!bitValue) regValue = ~regValue;

            switch (reg)
            {
                case Register8Table.B:
                case Register8Table.D:
                case Register8Table.H:
                    regValue <<= 8;
                    break;
            }

            return (ushort)regValue;
        }

        private static CpuZ80 ExecuteTest(byte bit, Register8Table register, Action<SimulationModel> fnPreTest)
        {
            var ob = OpcodeByte.New(x: 1, y: bit, z: (byte)register);
            return ExecuteTest(ob, fnPreTest);
        }

        private static CpuZ80 ExecuteTest(OpcodeByte ob, Action<SimulationModel> fnPreTest)
        {
            var cpuZ80 = new CpuZ80();
            byte[] buffer = new byte[] { 0xCB, ob.Value };
            var model = cpuZ80.Initialize(buffer);

            fnPreTest(model);

            model.ClockGen.SquareWave(8);

            Console.WriteLine(model.LogicAnalyzer.ToWaveJson());

            return cpuZ80;
        }
    }
}
