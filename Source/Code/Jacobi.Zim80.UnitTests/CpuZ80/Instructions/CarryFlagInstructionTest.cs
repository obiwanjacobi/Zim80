using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.CpuZ80.Opcodes;
using Jacobi.Zim80.CpuZ80.UnitTests;
using FluentAssertions;
using System;
using Jacobi.Zim80.UnitTests;
using Jacobi.Zim80.Diagnostics;

namespace Jacobi.Zim80.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class CarryFlagInstructionTest
    {
        [TestMethod]
        public void SCF_nc()
        {
            var ob = OpcodeByte.New(z: 7, y: 6);
            var cpu = ExecuteTest(ob, carry: false);

            cpu.AssertRegisters();
            cpu.Registers.Flags.C.Should().Be(true);
        }

        [TestMethod]
        public void SCF_c()
        {
            var ob = OpcodeByte.New(z: 7, y: 6);
            var cpu = ExecuteTest(ob, carry: true);

            cpu.AssertRegisters();
            cpu.Registers.Flags.C.Should().Be(true);
        }

        [TestMethod]
        public void CCF_nc()
        {
            var ob = OpcodeByte.New(z: 7, y: 7);
            var cpu = ExecuteTest(ob, carry: false);

            cpu.AssertRegisters();
            cpu.Registers.Flags.C.Should().Be(true);
        }

        [TestMethod]
        public void CCF_c()
        {
            var ob = OpcodeByte.New(z: 7, y: 7);
            var cpu = ExecuteTest(ob, carry: true);

            cpu.AssertRegisters();
            cpu.Registers.Flags.C.Should().Be(false);
        }

        private static CpuZ80 ExecuteTest(OpcodeByte ob, bool carry)
        {
            var cpuZ80 = new CpuZ80();
            byte[] buffer = new byte[] { ob.Value };
            var model = cpuZ80.Initialize(buffer);

            cpuZ80.FillRegisters();
            cpuZ80.Registers.Flags.C = carry;

            model.ClockGen.SquareWave(4);

            Console.WriteLine(model.LogicAnalyzer.ToWaveJson());

            return cpuZ80;
        }
    }
}
