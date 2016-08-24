using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using Jacobi.Zim80.Components.CpuZ80.UnitTests;
using Jacobi.Zim80.Components.Memory;
using Jacobi.Zim80.Model;

namespace Jacobi.Zim80.Components.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class PopInstructionTest
    {
        private const ushort Stack = 0x20;

        [TestMethod]
        public void PopBC()
        {
            var pop = OpcodeByte.New(x:3, z:1, p:0);
            var model = ExecuteTest(pop);

            model.Cpu.AssertRegisters(sp:Stack + 2, bc: 0xAA55);
        }

        [TestMethod]
        public void PopDE()
        {
            var pop = OpcodeByte.New(x: 3, z: 1, p: 1);
            var model = ExecuteTest(pop);

            model.Cpu.AssertRegisters(sp: Stack + 2, de: 0xAA55);
        }

        [TestMethod]
        public void PopHL()
        {
            var pop = OpcodeByte.New(x: 3, z: 1, p: 2);
            var model = ExecuteTest(pop);

            model.Cpu.AssertRegisters(sp: Stack + 2, hl: 0xAA55);
        }

        [TestMethod]
        public void PopAF_NotTestingF()
        {
            var pop = OpcodeByte.New(x: 3, z: 1, p: 3);
            var model = ExecuteTest(pop);

            model.Cpu.AssertRegisters(sp: Stack + 2, a: 0xAA);
        }

        private static SimulationModel ExecuteTest(OpcodeByte pop)
        {
            var cpuZ80 = new CpuZ80();
            var model = cpuZ80.Initialize(null);

            var writer = new MemoryWriter<BusData16, BusData8>(model.Memory);
            writer.Fill(0x48, new BusData8(0));
            writer[new BusData16(0)] = new BusData8(pop.Value);
            writer[new BusData16(Stack)] = new BusData8(0x55);
            writer[new BusData16(Stack + 1)] = new BusData8(0xAA);

            model.Cpu.FillRegisters(sp: Stack);
            model.ClockGen.BlockWave(10);

            return model;
        }
    }
}
