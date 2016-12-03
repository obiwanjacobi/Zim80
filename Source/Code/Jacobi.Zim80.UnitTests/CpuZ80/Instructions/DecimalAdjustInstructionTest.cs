using FluentAssertions;
using Jacobi.Zim80.CpuZ80.Opcodes;
using Jacobi.Zim80.CpuZ80.UnitTests;
using Jacobi.Zim80.Diagnostics;
using Jacobi.Zim80.UnitTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Jacobi.Zim80.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class DecimalAdjustInstructionTest
    {
        private const byte Value1 = 0x55;
        private const byte Value2 = 0xAA;

        private const byte LoOverflow = 0x5A;
        private const byte HiOverflow = 0xA5;

        private const byte LoCorrection = 0x06;
        private const byte HiCorrection = 0x60;

        [TestMethod]
        public void CPL()
        {
            var ob = OpcodeByte.New(z: 7, y: 5);
            var cpu = ExecuteTest(ob, (cpuZ80) =>
                {
                    cpuZ80.Registers.A = Value1;
                });

            cpu.Registers.A.Should().Be(unchecked((byte)~Value1));
        }

        [TestMethod]
        public void DAA_NoFlags_NoOverflow()
        {
            var ob = OpcodeByte.New(z: 7, y: 4);
            var cpu = ExecuteTest(ob, (cpuZ80) =>
            {
                cpuZ80.Registers.A = Value1;
            });

            cpu.Registers.A.Should().Be(Value1);
        }

        [TestMethod]
        public void DAA_NoFlags_LoOverflow()
        {
            var ob = OpcodeByte.New(z: 7, y: 4);
            var cpu = ExecuteTest(ob, (cpuZ80) =>
            {
                cpuZ80.Registers.A = LoOverflow;
            });

            cpu.Registers.A.Should().Be(LoOverflow + LoCorrection);
            cpu.Registers.Flags.C.Should().Be(false);
            cpu.Registers.Flags.H.Should().Be(true);
            cpu.Registers.Flags.N.Should().Be(false);
            cpu.Registers.Flags.Z.Should().Be(false);
            cpu.Registers.Flags.S.Should().Be(false);
        }

        [TestMethod]
        public void DAA_N_LoOverflow()
        {
            var ob = OpcodeByte.New(z: 7, y: 4);
            var cpu = ExecuteTest(ob, (cpuZ80) =>
            {
                cpuZ80.Registers.A = LoOverflow;
                cpuZ80.Registers.Flags.N = true;
            });

            cpu.Registers.A.Should().Be(LoOverflow - LoCorrection);
            cpu.Registers.Flags.C.Should().Be(false);
            cpu.Registers.Flags.H.Should().Be(false);
            cpu.Registers.Flags.N.Should().Be(true);
            cpu.Registers.Flags.Z.Should().Be(false);
            cpu.Registers.Flags.S.Should().Be(false);
        }

        [TestMethod]
        public void DAA_H_NoOverflow()
        {
            var ob = OpcodeByte.New(z: 7, y: 4);
            var cpu = ExecuteTest(ob, (cpuZ80) =>
            {
                cpuZ80.Registers.A = Value1;
                cpuZ80.Registers.Flags.H = true;
            });

            cpu.Registers.A.Should().Be(Value1 + LoCorrection);
            cpu.Registers.Flags.C.Should().Be(false);
            cpu.Registers.Flags.H.Should().Be(true);
            cpu.Registers.Flags.N.Should().Be(false);
            cpu.Registers.Flags.Z.Should().Be(false);
            cpu.Registers.Flags.S.Should().Be(false);
        }

        [TestMethod]
        public void DAA_NoFlags_HiOverflow()
        {
            var ob = OpcodeByte.New(z: 7, y: 4);
            var cpu = ExecuteTest(ob, (cpuZ80) =>
            {
                cpuZ80.Registers.A = HiOverflow;
            });

            cpu.Registers.A.Should().Be(unchecked((byte)(HiOverflow + HiCorrection)));
            cpu.Registers.Flags.C.Should().Be(true);
            cpu.Registers.Flags.H.Should().Be(true);
            cpu.Registers.Flags.N.Should().Be(false);
            cpu.Registers.Flags.Z.Should().Be(false);
            cpu.Registers.Flags.S.Should().Be(false);
        }

        private static CpuZ80 ExecuteTest(OpcodeByte ob, Action<CpuZ80> preTest)
        {
            var cpuZ80 = new CpuZ80();
            byte[] buffer = new byte[] { ob.Value };
            var model = cpuZ80.Initialize(buffer);

            cpuZ80.FillRegisters();
            preTest(cpuZ80);

            model.ClockGen.SquareWave(4);

            Console.WriteLine(model.LogicAnalyzer.ToWaveJson());

            return cpuZ80;
        }
    }
}
