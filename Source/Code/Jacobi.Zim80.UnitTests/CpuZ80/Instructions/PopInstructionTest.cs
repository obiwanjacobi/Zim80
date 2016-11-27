using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.CpuZ80.Opcodes;
using Jacobi.Zim80.CpuZ80.UnitTests;
using Jacobi.Zim80.Memory;
using Jacobi.Zim80.Test;

namespace Jacobi.Zim80.CpuZ80.Instructions.UnitTests
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

        [TestMethod]
        public void PopIX()
        {
            var pop = OpcodeByte.New(x: 3, z: 1, p: 2);
            var model = ExecuteTest(pop, 0xDD);

            model.Cpu.AssertRegisters(sp: Stack + 2, ix: 0xAA55);
        }

        [TestMethod]
        public void PopIY()
        {
            var pop = OpcodeByte.New(x: 3, z: 1, p: 2);
            var model = ExecuteTest(pop, 0xFD);

            model.Cpu.AssertRegisters(sp: Stack + 2, iy: 0xAA55);
        }

        private static SimulationModel ExecuteTest(OpcodeByte pop, byte extension = 0)
        {
            var cpuZ80 = new CpuZ80();
            var model = cpuZ80.Initialize(null);

            var writer = new MemoryWriter<BusData16, BusData8>(model.Memory);
            writer.Fill(0x48, new BusData8(0));
            if (extension == 0)
                writer[new BusData16(0)] = new BusData8(pop.Value);
            else
            {
                writer[new BusData16(0)] = new BusData8(extension);
                writer[new BusData16(1)] = new BusData8(pop.Value);
            }
            writer[new BusData16(Stack)] = new BusData8(0x55);
            writer[new BusData16(Stack + 1)] = new BusData8(0xAA);

            model.Cpu.FillRegisters(sp: Stack);
            model.ClockGen.SquareWave(extension == 0 ? 10 : 14);

            return model;
        }
    }
}
