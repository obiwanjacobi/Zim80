using FluentAssertions;
using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using Jacobi.Zim80.Components.CpuZ80.UnitTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jacobi.Zim80.Components.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class NegateInstructionTest
    {
        [TestMethod]
        public void Negate_00()
        {
            var cpu = ExecuteTest(0);
            cpu.AssertRegisters(a:0xFF);
            cpu.Registers.Flags.S.Should().Be(true);
            cpu.Registers.Flags.Z.Should().Be(false);
            cpu.Registers.Flags.PV.Should().Be(false);
            cpu.Registers.Flags.C.Should().Be(false);
        }

        [TestMethod]
        public void Negate_FF()
        {
            var cpu = ExecuteTest(0xFF);
            cpu.AssertRegisters(a: 0);
            cpu.Registers.Flags.S.Should().Be(false);
            cpu.Registers.Flags.Z.Should().Be(true);
            cpu.Registers.Flags.PV.Should().Be(false);
            cpu.Registers.Flags.C.Should().Be(true);
        }

        [TestMethod]
        public void Negate_80()
        {
            var cpu = ExecuteTest(0x80);
            cpu.AssertRegisters(a: 0x7F);
            cpu.Registers.Flags.S.Should().Be(false);
            cpu.Registers.Flags.Z.Should().Be(false);
            cpu.Registers.Flags.PV.Should().Be(true);
            cpu.Registers.Flags.C.Should().Be(true);
        }

        private CpuZ80 ExecuteTest(byte value)
        {
            var ob = OpcodeByte.New(x: 1, z: 4, y: 0);

            var cpuZ80 = new CpuZ80();
            byte[] buffer = new byte[] { 0xED, ob.Value };

            var model = cpuZ80.Initialize(buffer);

            cpuZ80.FillRegisters();
            cpuZ80.Registers.A = value;

            model.ClockGen.SquareWave(8);

            return cpuZ80;
        }
    }
}
