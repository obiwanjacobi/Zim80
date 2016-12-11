using Jacobi.Zim80.CpuZ80.Opcodes;
using Jacobi.Zim80.CpuZ80.UnitTests;
using Jacobi.Zim80.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Jacobi.Zim80.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class LoadRegister16InstructionTest
    {
        [TestMethod]
        public void LdSpHl()
        {
            var ob = OpcodeByte.New(x: 3, z: 1, q: 1, p: 3);

            var cpu = ExecuteTest(ob, (z80) =>
                {
                    z80.Registers.HL = 0;
                });

            cpu.AssertRegisters(sp: 0, hl: 0);
        }

        [TestMethod]
        public void LdSpIx()
        {
            var ob = OpcodeByte.New(x: 3, z: 1, q: 1, p: 3);

            var cpu = ExecuteTest(ob, (z80) =>
            {
                z80.Registers.HL = 0;
            }, 0xDD);

            cpu.AssertRegisters(sp: 0, hl: 0);
        }

        [TestMethod]
        public void LdSpIy()
        {
            var ob = OpcodeByte.New(x: 3, z: 1, q: 1, p: 3);

            var cpu = ExecuteTest(ob, (z80) =>
            {
                z80.Registers.HL = 0;
            }, 0xFD);

            cpu.AssertRegisters(sp: 0, hl: 0);
        }

        private static CpuZ80 ExecuteTest(OpcodeByte ob, Action<CpuZ80> preTest, byte extension = 0)
        {
            var cpuZ80 = new CpuZ80();
            byte[] buffer = extension == 0 ?
                new byte[] { ob.Value } :
                new byte[] { extension, ob.Value };

            var model = cpuZ80.Initialize(buffer);

            cpuZ80.FillRegisters();
            preTest(cpuZ80);

            model.ClockGen.SquareWave(extension == 0 ? 6 : 10);

            Console.WriteLine(model.LogicAnalyzer.ToWaveJson());

            return cpuZ80;
        }
    }
}
