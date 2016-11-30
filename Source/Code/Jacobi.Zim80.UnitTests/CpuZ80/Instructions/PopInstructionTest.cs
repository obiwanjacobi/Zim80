using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.CpuZ80.Opcodes;
using Jacobi.Zim80.CpuZ80.UnitTests;
using Jacobi.Zim80.Test;
using System;
using Jacobi.Zim80.UnitTests;

namespace Jacobi.Zim80.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class PopInstructionTest
    {
        private const ushort Stack = 0x04;
        private const ushort Value = 0xAA55;

        [TestMethod]
        public void PopBC()
        {
            var pop = OpcodeByte.New(x:3, z:1, p:0);
            var model = ExecuteTest(pop);

            model.Cpu.AssertRegisters(sp:Stack + 2, bc: Value);
        }

        [TestMethod]
        public void PopDE()
        {
            var pop = OpcodeByte.New(x: 3, z: 1, p: 1);
            var model = ExecuteTest(pop);

            model.Cpu.AssertRegisters(sp: Stack + 2, de: Value);
        }

        [TestMethod]
        public void PopHL()
        {
            var pop = OpcodeByte.New(x: 3, z: 1, p: 2);
            var model = ExecuteTest(pop);

            model.Cpu.AssertRegisters(sp: Stack + 2, hl: Value);
        }

        [TestMethod]
        public void PopAF_NotTestingF()
        {
            var pop = OpcodeByte.New(x: 3, z: 1, p: 3);
            var model = ExecuteTest(pop);

            model.Cpu.AssertRegisters(sp: Stack + 2, a: 0xAA);
        }

        [TestMethod]
        public void PopIX()
        {
            var pop = OpcodeByte.New(x: 3, z: 1, p: 2);
            var model = ExecuteTest(pop, 0xDD);

            model.Cpu.AssertRegisters(sp: Stack + 2, ix: Value);
        }

        [TestMethod]
        public void PopIY()
        {
            var pop = OpcodeByte.New(x: 3, z: 1, p: 2);
            var model = ExecuteTest(pop, 0xFD);

            model.Cpu.AssertRegisters(sp: Stack + 2, iy: Value);
        }

        private static SimulationModel ExecuteTest(OpcodeByte pop, byte extension = 0)
        {
            var cpuZ80 = new CpuZ80();
            var buffer = (extension == 0) ?
                new byte[] { pop.Value, 0, 0, 0, 0x55, 0xAA } :
                new byte[] { extension, pop.Value, 0, 0, 0x55, 0xAA };
            var model = cpuZ80.Initialize(buffer);

            model.Cpu.FillRegisters(sp: Stack);
            model.ClockGen.SquareWave(extension == 0 ? 10 : 14);

            Console.WriteLine(model.LogicAnalyzer.ToWaveJson());

            return model;
        }
    }
}
