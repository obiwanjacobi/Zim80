using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.CpuZ80.Opcodes;
using Jacobi.Zim80.CpuZ80.UnitTests;
using System;
using FluentAssertions;

namespace Jacobi.Zim80.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class LoadSystemRegisterInstructionTest
    {
        private const byte Value = 0x55;

        [TestMethod]
        public void LdIA()
        {
            var ob = OpcodeByte.New(x: 1, z: 7, y: 0);

            var cpu = ExecuteTest(ob, (z80) =>
                {
                    z80.Registers.A = Value;
                });

            cpu.AssertRegisters(a: Value, i: Value);
        }

        [TestMethod]
        public void LdRA()
        {
            var ob = OpcodeByte.New(x: 1, z: 7, y: 1);

            var cpu = ExecuteTest(ob, (z80) =>
            {
                z80.Registers.A = Value;
            });

            cpu.AssertRegisters(a: Value, r: Value);
        }

        [TestMethod]
        public void LdAI()
        {
            var ob = OpcodeByte.New(x: 1, z: 7, y: 2);

            var cpu = ExecuteTest(ob, (z80) =>
            {
                z80.Registers.Interrupt.IFF2 = true;
                z80.Registers.I = Value;
            });

            cpu.AssertRegisters(a: Value, i: Value);
            cpu.Registers.Flags.PV.Should().Be(true);
        }

        [TestMethod]
        public void LdAR()
        {
            var ob = OpcodeByte.New(x: 1, z: 7, y: 3);

            var cpu = ExecuteTest(ob, (z80) =>
            {
                z80.Registers.Interrupt.IFF2 = true;
                z80.Registers.R = Value;
            });

            // R is incremented twice for the 2 opcode byte
            cpu.AssertRegisters(a: Value + 2, r: Value + 2);
            cpu.Registers.Flags.PV.Should().Be(true);
        }

        private static CpuZ80 ExecuteTest(OpcodeByte ob, Action<CpuZ80> preTest)
        {
            var cpuZ80 = new CpuZ80();
            byte[] buffer = new byte[] { 0xED, ob.Value };

            var model = cpuZ80.Initialize(buffer);

            cpuZ80.FillRegisters();
            preTest(cpuZ80);

            model.ClockGen.SquareWave(9);

            return cpuZ80;
        }
    }
}
