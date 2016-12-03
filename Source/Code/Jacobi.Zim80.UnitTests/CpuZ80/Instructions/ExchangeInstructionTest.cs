using Jacobi.Zim80.CpuZ80.Opcodes;
using Jacobi.Zim80.CpuZ80.UnitTests;
using Jacobi.Zim80.Diagnostics;
using Jacobi.Zim80.UnitTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Jacobi.Zim80.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class ExchangeInstructionTest
    {
        [TestMethod]
        public void ExAFAF()
        {
            var ob = OpcodeByte.New(x: 0, z: 0, y: 1);

            var cpuZ80 = new CpuZ80();
            var model = cpuZ80.Initialize(new byte[] { ob.Value });

            cpuZ80.FillRegisters(a: 0x55, a_a: 0xAA);

            model.ClockGen.SquareWave(4);

            cpuZ80.AssertRegisters(a: 0xAA, a_a: 0x55);
        }

        [TestMethod]
        public void ExDE_HL()
        {
            var ob = OpcodeByte.New(x: 3, z: 3, y: 5);

            var cpuZ80 = new CpuZ80();
            var model = cpuZ80.Initialize(new byte[] { ob.Value });

            cpuZ80.FillRegisters(de: 0x1234, hl:0x9876);

            model.ClockGen.SquareWave(4);

            cpuZ80.AssertRegisters(hl: 0x1234, de: 0x9876);
        }

        [TestMethod]
        public void Exx()
        {
            var ob = OpcodeByte.New(x: 3, z: 1, q: 1, p: 1);

            var cpuZ80 = new CpuZ80();
            var model = cpuZ80.Initialize(new byte[] { ob.Value });

            cpuZ80.FillRegisters(bc: 0x1234, de:0x5678, hl: 0x9ABC,
                a_bc: 0x4321, a_de: 8765, a_hl: 0xCBA9);

            model.ClockGen.SquareWave(4);

            Console.WriteLine(model.LogicAnalyzer.ToWaveJson());

            cpuZ80.AssertRegisters(a_bc: 0x1234, a_de: 0x5678, a_hl: 0x9ABC,
                bc: 0x4321, de: 8765, hl: 0xCBA9);
        }
    }
}
