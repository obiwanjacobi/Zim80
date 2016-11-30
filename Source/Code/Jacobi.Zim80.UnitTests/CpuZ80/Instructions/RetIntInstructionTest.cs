using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.CpuZ80.Opcodes;
using Jacobi.Zim80.Test;
using Jacobi.Zim80.CpuZ80.UnitTests;
using Jacobi.Zim80.Memory;
using Jacobi.Zim80.UnitTests;

namespace Jacobi.Zim80.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class RetIntInstructionTest
    {
        private const ushort Stack = 0x04;
        private const ushort Expected = 0xAA55;

        [TestMethod]
        public void RETN()
        {
            var ret = OpcodeByte.New(x: 1, z: 5, y: 0);
            var model = ExecuteTest(ret);

            model.Cpu.AssertRegisters(sp: Stack + 2, pc: Expected);
        }

        [TestMethod]
        public void RETI()
        {
            var ret = OpcodeByte.New(x: 1, z: 5, y: 1);
            var model = ExecuteTest(ret);

            model.Cpu.AssertRegisters(sp: Stack + 2, pc: Expected);
        }

        private static SimulationModel ExecuteTest(OpcodeByte ret)
        {
            var cpuZ80 = new CpuZ80();
            var buffer = new byte[] { 0xED, ret.Value, 0, 0, 0x55, 0xAA };
            var model = cpuZ80.Initialize(buffer);

            model.Cpu.FillRegisters(sp: Stack);

            model.ClockGen.SquareWave(14);

            Console.WriteLine(model.LogicAnalyzer.ToWaveJson());

            return model;
        }
    }
}
