using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using Jacobi.Zim80.Model;
using Jacobi.Zim80.Components.CpuZ80.UnitTests;
using FluentAssertions;

namespace Jacobi.Zim80.Components.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class CompareRepeatInstructionTest
    {
        private const ushort Address = 0x04;
        private const byte Value = 0xAA;
        private const byte Length = 0x02;

        [TestMethod]
        public void CPI_nz()
        {
            var ob = OpcodeByte.New(x: 2, z: 1, y: 4);
            var model = ExecuteTest(ob, (cpu) =>
            {
                cpu.Registers.HL = Address;
                cpu.Registers.BC = Length;
                //cpu.Registers.A = 0;

            }, true);

            model.Cpu.AssertRegisters(hl: Address + 1, bc: Length - 1);
            model.Cpu.Registers.Flags.Z.Should().Be(false);
        }

        [TestMethod]
        public void CPI_z()
        {
            var ob = OpcodeByte.New(x: 2, z: 1, y: 4);
            var model = ExecuteTest(ob, (cpu) =>
            {
                cpu.Registers.HL = Address;
                cpu.Registers.BC = Length;
                cpu.Registers.A = Value;

            }, true);

            model.Cpu.AssertRegisters(a: Value, hl: Address + 1, bc: Length - 1);
            model.Cpu.Registers.Flags.Z.Should().Be(true);
        }

        [TestMethod]
        public void CPD_nz()
        {
            var ob = OpcodeByte.New(x: 2, z: 1, y: 5);
            var model = ExecuteTest(ob, (cpu) =>
            {
                cpu.Registers.HL = Address;
                cpu.Registers.BC = Length;
                //cpu.Registers.A = 0;

            }, true);

            model.Cpu.AssertRegisters(hl: Address - 1, bc: Length - 1);
            model.Cpu.Registers.Flags.Z.Should().Be(false);
        }

        [TestMethod]
        public void CPD_z()
        {
            var ob = OpcodeByte.New(x: 2, z: 1, y: 5);
            var model = ExecuteTest(ob, (cpu) =>
            {
                cpu.Registers.HL = Address;
                cpu.Registers.BC = Length;
                cpu.Registers.A = Value;

            }, true);

            model.Cpu.AssertRegisters(a: Value, hl: Address - 1, bc: Length - 1);
            model.Cpu.Registers.Flags.Z.Should().Be(true);
        }

        [TestMethod]
        public void CPIR()
        {
            var ob = OpcodeByte.New(x: 2, z: 1, y: 6);
            var model = ExecuteTest(ob, (cpu) =>
            {
                cpu.Registers.HL = Address - 1;
                cpu.Registers.BC = Length;
                cpu.Registers.A = Value;

            }, false);

            model.Cpu.AssertRegisters(a: Value, hl: Address + 1, bc: 0);
            model.Cpu.Registers.Flags.Z.Should().Be(true);
        }

        [TestMethod]
        public void CPDR()
        {
            var ob = OpcodeByte.New(x: 2, z: 1, y: 7);
            var model = ExecuteTest(ob, (cpu) =>
            {
                cpu.Registers.HL = Address + 1;
                cpu.Registers.BC = Length;
                cpu.Registers.A = Value;

            }, false);

            model.Cpu.AssertRegisters(a: Value, hl: Address - 1, bc: 0);
            model.Cpu.Registers.Flags.Z.Should().Be(true);
        }

        private SimulationModel ExecuteTest(OpcodeByte ob, Action<CpuZ80> preTest, bool isConditionMet)
        {
            var cpu = new CpuZ80();
            var model = cpu.Initialize(
                new byte[] { 0xED, ob.Value, 0, 0, Value, 0, 0 });

            cpu.FillRegisters();
            preTest(cpu);

            long cycles = 16;
            if (!isConditionMet) cycles += 21;
            model.ClockGen.BlockWave(cycles);

            return model;
        }
    }
}
