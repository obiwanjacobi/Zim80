using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using Jacobi.Zim80.Components.CpuZ80.UnitTests;

namespace Jacobi.Zim80.Components.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class JumpHLInstructionTest
    {
        private const ushort JumpAddress = 0xAA55;

        [TestMethod]
        public void JPHL()
        {
            var ob = OpcodeByte.New(x: 3, z: 1, q: 1, p: 2);

            var cpu = ExecuteTest(ob, (r) => {
                r.HL = JumpAddress;
            });

            cpu.AssertRegisters(pc: JumpAddress, hl: JumpAddress);
        }

        private static CpuZ80 ExecuteTest(OpcodeByte ob, Action<Registers.RegisterSet> preTest)
        {
            var cpuZ80 = new CpuZ80();
            var buffer = new[] { ob.Value };
            var model = cpuZ80.Initialize(buffer);

            model.Cpu.FillRegisters();
            preTest(cpuZ80.Registers.PrimarySet);

            model.ClockGen.BlockWave(4);

            return cpuZ80;
        }
    }
}
