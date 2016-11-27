using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.CpuZ80.Opcodes;
using Jacobi.Zim80.CpuZ80.UnitTests;

namespace Jacobi.Zim80.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class JumpRegister16InstructionTest
    {
        private const ushort JumpAddress = 0xAA55;

        [TestMethod]
        public void JP_HL()
        {
            var ob = OpcodeByte.New(x: 3, z: 1, q: 1, p: 2);

            var cpu = ExecuteTest(ob, (r) => {
                r.HL = JumpAddress;
            });

            cpu.AssertRegisters(pc: JumpAddress, hl: JumpAddress);
        }

        [TestMethod]
        public void JP_IX()
        {
            var ob = OpcodeByte.New(x: 3, z: 1, q: 1, p: 2);

            var cpu = ExecuteTest(ob, (r) => {
                r.IX = JumpAddress;
            }, 0xDD);

            cpu.AssertRegisters(pc: JumpAddress, ix: JumpAddress);
        }

        [TestMethod]
        public void JP_IY()
        {
            var ob = OpcodeByte.New(x: 3, z: 1, q: 1, p: 2);

            var cpu = ExecuteTest(ob, (r) => {
                r.IY = JumpAddress;
            }, 0xFD);

            cpu.AssertRegisters(pc: JumpAddress, iy: JumpAddress);
        }

        private static CpuZ80 ExecuteTest(OpcodeByte ob, Action<Registers> preTest, byte extension = 0)
        {
            var cpuZ80 = new CpuZ80();
            var buffer = extension == 0 ?
                    new[] { ob.Value } :
                    new[] { extension, ob.Value };
            var model = cpuZ80.Initialize(buffer);

            model.Cpu.FillRegisters();
            preTest(cpuZ80.Registers);

            model.ClockGen.SquareWave(extension == 0 ? 4 : 8);

            return cpuZ80;
        }
    }
}
