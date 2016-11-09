using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using Jacobi.Zim80.Components.CpuZ80.UnitTests;

namespace Jacobi.Zim80.Components.CpuZ80.Instructions.UnitTests
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

            model.ClockGen.BlockWave(12);

            return cpuZ80;
        }
    }
}
