﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using Jacobi.Zim80.Components.CpuZ80.UnitTests;
using FluentAssertions;

namespace Jacobi.Zim80.Components.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class DjnzInstructionTest
    {
        [TestMethod]
        public void Djnz_Once_NZ()
        {
            var cpu = ExecuteTest(b: 2, cycles: 13);

            cpu.AssertRegisters(
                pc: 0,
                bc: (ushort)((1 << 8) | CpuZ80TestExtensions.MagicValue));

            cpu.Registers.PrimarySet.Flags.Z.Should().BeFalse();
        }

        [TestMethod]
        public void Djnz_Once_Z()
        {
            var cpu = ExecuteTest(b: 1, cycles: 13);

            cpu.AssertRegisters(
                pc: 2,
                bc: CpuZ80TestExtensions.MagicValue);

            cpu.Registers.PrimarySet.Flags.Z.Should().BeTrue();
        }

        [TestMethod]
        public void Djnz_Twice_Exit()
        {
            var cpu = ExecuteTest(b: 2, cycles: 21);

            cpu.AssertRegisters(
                pc: 2,
                bc: CpuZ80TestExtensions.MagicValue);

            cpu.Registers.PrimarySet.Flags.Z.Should().BeTrue();
        }

        private static CpuZ80 ExecuteTest(byte b, long cycles)
        {
            var djnz = OpcodeByte.New(y: 2);
            var d = unchecked((byte)-2);

            var cpuZ80 = new CpuZ80();
            var model = cpuZ80.Initialize(new byte[] { djnz.Value, d });

            var bc = (ushort)((b << 8) | CpuZ80TestExtensions.MagicValue);
            cpuZ80.FillRegisters(bc: bc);

            model.ClockGen.BlockWave(cycles);

            return cpuZ80;
        }
    }
}
