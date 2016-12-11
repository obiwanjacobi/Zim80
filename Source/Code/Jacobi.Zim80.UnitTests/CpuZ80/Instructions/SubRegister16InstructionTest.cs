using FluentAssertions;
using Jacobi.Zim80.CpuZ80.Opcodes;
using Jacobi.Zim80.CpuZ80.UnitTests;
using Jacobi.Zim80.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Jacobi.Zim80.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class SubRegister16InstructionTest
    {
        private const ushort Value = 0xAA55;
        private const ushort ValueToSub = 0x1122;
        private const ushort ExpectedNC = 0x9933;
        private const ushort ExpectedC = 0x9932;

        [TestMethod]
        public void SbcHL_BC_nc()
        {
            var cpu = ExecuteTest(Register16Table.BC, carry: false);
            cpu.Registers.BC.Should().Be(ValueToSub);
            cpu.Registers.HL.Should().Be(ExpectedNC);
            cpu.Registers.Flags.Z.Should().Be(false);
            cpu.Registers.Flags.C.Should().Be(false);
            cpu.Registers.Flags.S.Should().Be(true);
        }

        [TestMethod]
        public void SbcHL_BC_c()
        {
            var cpu = ExecuteTest(Register16Table.BC, carry: true);
            cpu.Registers.BC.Should().Be(ValueToSub);
            cpu.Registers.HL.Should().Be(ExpectedC);
            cpu.Registers.Flags.Z.Should().Be(false);
            cpu.Registers.Flags.C.Should().Be(false);
            cpu.Registers.Flags.S.Should().Be(true);
        }

        [TestMethod]
        public void SbcHL_DE_nc()
        {
            var cpu = ExecuteTest(Register16Table.DE, carry: false);
            cpu.Registers.DE.Should().Be(ValueToSub);
            cpu.Registers.HL.Should().Be(ExpectedNC);
            cpu.Registers.Flags.Z.Should().Be(false);
            cpu.Registers.Flags.C.Should().Be(false);
            cpu.Registers.Flags.S.Should().Be(true);
        }

        [TestMethod]
        public void SbcHL_DE_c()
        {
            var cpu = ExecuteTest(Register16Table.DE, carry: true);
            cpu.Registers.DE.Should().Be(ValueToSub);
            cpu.Registers.HL.Should().Be(ExpectedC);
            cpu.Registers.Flags.Z.Should().Be(false);
            cpu.Registers.Flags.C.Should().Be(false);
            cpu.Registers.Flags.S.Should().Be(true);
        }

        [TestMethod]
        public void SbcHL_HL_nc()
        {
            var cpu = ExecuteTest(Register16Table.HL, carry: false);
            cpu.Registers.HL.Should().Be(0);
            cpu.Registers.Flags.Z.Should().Be(true);
            cpu.Registers.Flags.C.Should().Be(false);
            cpu.Registers.Flags.S.Should().Be(false);
        }

        [TestMethod]
        public void SbcHL_HL_c()
        {
            var cpu = ExecuteTest(Register16Table.HL, carry: true);
            cpu.Registers.HL.Should().Be(0xFFFF);
            cpu.Registers.Flags.Z.Should().Be(false);
            cpu.Registers.Flags.C.Should().Be(true);
            cpu.Registers.Flags.S.Should().Be(true);
        }

        [TestMethod]
        public void SbcHL_SP_nc()
        {
            var cpu = ExecuteTest(Register16Table.SP, carry: false);
            cpu.Registers.SP.Should().Be(ValueToSub);
            cpu.Registers.HL.Should().Be(ExpectedNC);
            cpu.Registers.Flags.Z.Should().Be(false);
            cpu.Registers.Flags.C.Should().Be(false);
            cpu.Registers.Flags.S.Should().Be(true);
        }

        [TestMethod]
        public void SbcHL_SP_c()
        {
            var cpu = ExecuteTest(Register16Table.SP, carry: true);
            cpu.Registers.SP.Should().Be(ValueToSub);
            cpu.Registers.HL.Should().Be(ExpectedC);
            cpu.Registers.Flags.Z.Should().Be(false);
            cpu.Registers.Flags.C.Should().Be(false);
            cpu.Registers.Flags.S.Should().Be(true);
        }

        private static CpuZ80 ExecuteTest(Register16Table reg16, bool carry)
        {
            var ob = OpcodeByte.New(x: 1, z: 2, q: 0, p: (byte)reg16);

            var cpuZ80 = new CpuZ80();
            byte[] buffer = new byte[] { 0xED, ob.Value };

            var model = cpuZ80.Initialize(buffer);

            cpuZ80.Registers.Flags.C = carry;
            cpuZ80.FillRegisters(hl: Value);
            cpuZ80.Registers[reg16] = ValueToSub;

            model.ClockGen.SquareWave(15);

            Console.WriteLine(model.LogicAnalyzer.ToWaveJson());

            return cpuZ80;
        }
    }
}
