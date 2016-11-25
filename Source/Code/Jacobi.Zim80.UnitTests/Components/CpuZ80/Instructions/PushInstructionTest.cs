using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using Jacobi.Zim80.Components.CpuZ80.UnitTests;
using Jacobi.Zim80.Components.Memory;
using Jacobi.Zim80.Model;
using System;
using Jacobi.Zim80.Components.Memory.UnitTests;

namespace Jacobi.Zim80.Components.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class PushInstructionTest
    {
        private const ushort Stack = 0x20;

        [TestMethod]
        public void PushBC()
        {
            var push = OpcodeByte.New(x:3, z:5, p:0);
            var model = ExecuteTest(push, 
                            (m) => m.Cpu.FillRegisters(sp:Stack, bc:0xAA55));

            model.Cpu.AssertRegisters(sp:Stack - 2, bc: 0xAA55);
            model.Memory.Assert(Stack - 1, 0xAA);
            model.Memory.Assert(Stack - 2, 0x55);
        }

        [TestMethod]
        public void PushDE()
        {
            var push = OpcodeByte.New(x: 3, z: 5, p: 1);
            var model = ExecuteTest(push,
                            (m) => m.Cpu.FillRegisters(sp: Stack, de: 0xAA55));

            model.Cpu.AssertRegisters(sp: Stack - 2, de: 0xAA55);
            model.Memory.Assert(Stack - 1, 0xAA);
            model.Memory.Assert(Stack - 2, 0x55);
        }

        [TestMethod]
        public void PushHL()
        {
            var push = OpcodeByte.New(x: 3, z: 5, p: 2);
            var model = ExecuteTest(push,
                            (m) => m.Cpu.FillRegisters(sp: Stack, hl: 0xAA55));

            model.Cpu.AssertRegisters(sp: Stack - 2, hl: 0xAA55);
            model.Memory.Assert(Stack - 1, 0xAA);
            model.Memory.Assert(Stack - 2, 0x55);
        }

        [TestMethod]
        public void PushAF_NotTestingF()
        {
            var push = OpcodeByte.New(x: 3, z: 5, p: 3);
            var model = ExecuteTest(push,
                            (m) => m.Cpu.FillRegisters(sp: Stack, a: 0xAA));

            model.Cpu.AssertRegisters(sp: Stack - 2, a: 0xAA);
            model.Memory.Assert(Stack - 1, 0xAA);
        }

        [TestMethod]
        public void PushIX()
        {
            var push = OpcodeByte.New(x: 3, z: 5, p: 2);
            var model = ExecuteTest(push,
                            (m) => m.Cpu.FillRegisters(sp: Stack, ix: 0xAA55), 0xDD);

            model.Cpu.AssertRegisters(sp: Stack - 2, ix: 0xAA55);
            model.Memory.Assert(Stack - 1, 0xAA);
            model.Memory.Assert(Stack - 2, 0x55);
        }

        [TestMethod]
        public void PushIY()
        {
            var push = OpcodeByte.New(x: 3, z: 5, p: 2);
            var model = ExecuteTest(push,
                            (m) => m.Cpu.FillRegisters(sp: Stack, iy: 0xAA55), 0xFD);

            model.Cpu.AssertRegisters(sp: Stack - 2, iy: 0xAA55);
            model.Memory.Assert(Stack - 1, 0xAA);
            model.Memory.Assert(Stack - 2, 0x55);
        }

        private static SimulationModel ExecuteTest(OpcodeByte push, 
                Action<SimulationModel> preTest, byte extension = 0)
        {
            var cpuZ80 = new CpuZ80();
            var model = cpuZ80.Initialize(null);

            var writer = new MemoryWriter<BusData16, BusData8>(model.Memory);
            writer.Fill(0x48, new BusData8(0));

            if (extension == 0)
                writer[new BusData16(0)] = new BusData8(push.Value);
            else
            {
                writer[new BusData16(0)] = new BusData8(extension);
                writer[new BusData16(1)] = new BusData8(push.Value);
            }

            preTest(model);

            model.ClockGen.SquareWave(extension == 0 ? 11 : 15);

            return model;
        }
    }
}
