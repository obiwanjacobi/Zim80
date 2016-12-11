using Jacobi.Zim80.CpuZ80.Opcodes;
using Jacobi.Zim80.CpuZ80.UnitTests;
using Jacobi.Zim80.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Jacobi.Zim80.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class JumpRelativeInstructionTest
    {
        [TestMethod]
        public void Jr()
        {
            var opcode = OpcodeByte.New(y: 3);
            var cpu = ExecuteTest(opcode);

            cpu.AssertRegisters(pc: 0);
        }

        [TestMethod]
        public void Jrnz_Z()
        {
            var opcode = OpcodeByte.New(y: 4);
            var cpu = ExecuteTest(opcode, z: true);

            cpu.AssertRegisters(pc: 2);
        }

        [TestMethod]
        public void Jrnz_NZ()
        {
            var opcode = OpcodeByte.New(y: 4);
            var cpu = ExecuteTest(opcode);

            cpu.AssertRegisters(pc: 0);
        }

        [TestMethod]
        public void Jrz_Z()
        {
            var opcode = OpcodeByte.New(y: 5);
            var cpu = ExecuteTest(opcode, z: true);

            cpu.AssertRegisters(pc: 0);
        }

        [TestMethod]
        public void Jrz_NZ()
        {
            var opcode = OpcodeByte.New(y: 5);
            var cpu = ExecuteTest(opcode);

            cpu.AssertRegisters(pc: 2);
        }

        [TestMethod]
        public void Jrnc_C()
        {
            var opcode = OpcodeByte.New(y: 6);
            var cpu = ExecuteTest(opcode, c: true);

            cpu.AssertRegisters(pc: 2);
        }

        [TestMethod]
        public void Jrnc_NC()
        {
            var opcode = OpcodeByte.New(y: 6);
            var cpu = ExecuteTest(opcode);

            cpu.AssertRegisters(pc: 0);
        }

        [TestMethod]
        public void Jrc_C()
        {
            var opcode = OpcodeByte.New(y: 7);
            var cpu = ExecuteTest(opcode, c: true);

            cpu.AssertRegisters(pc: 0);
        }

        [TestMethod]
        public void Jrc_NC()
        {
            var opcode = OpcodeByte.New(y: 7);
            var cpu = ExecuteTest(opcode);

            cpu.AssertRegisters(pc: 2);
        }

        private static CpuZ80 ExecuteTest(OpcodeByte ob,
                                bool c = false, bool z = false)
        {
            var d = unchecked((byte)-2);

            var cpuZ80 = new CpuZ80();
            var model = cpuZ80.Initialize(new byte[] { ob.Value, d });

            cpuZ80.Registers.Flags.Z = z;
            cpuZ80.Registers.Flags.C = c;

            cpuZ80.FillRegisters();

            model.ClockGen.SquareWave(12);

            Console.WriteLine(model.LogicAnalyzer.ToWaveJson());

            return cpuZ80;
        }
    }
}
