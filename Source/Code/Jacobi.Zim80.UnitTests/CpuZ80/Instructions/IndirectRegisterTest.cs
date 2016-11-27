using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.Test;
using Jacobi.Zim80.CpuZ80.Opcodes;
using Jacobi.Zim80.Memory;
using Jacobi.Zim80.CpuZ80.UnitTests;
using Jacobi.Zim80.Memory.UnitTests;

namespace Jacobi.Zim80.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class IndirectRegisterTest
    {
        [TestMethod]
        public void IncHL()
        {
            var ob = OpcodeByte.New(z: 4, y: 6);

            var model = ExecuteTest(ob, (m) => {
                m.Cpu.FillRegisters(hl: 0x10);
                var writer = new MemoryWriter<BusData16, BusData8>(m.Memory);
                writer[new BusData16(0x10)] = new BusData8(0xAA);
            });

            model.Cpu.AssertRegisters(hl: 0x10);
            model.Memory.Assert(0x10, 0xAB);
        }

        [TestMethod]
        public void DecHL()
        {
            var ob = OpcodeByte.New(z: 5, y: 6);

            var model = ExecuteTest(ob, (m) => {
                m.Cpu.FillRegisters(hl: 0x10);
                var writer = new MemoryWriter<BusData16, BusData8>(m.Memory);
                writer[new BusData16(0x10)] = new BusData8(0xAA);
            });

            model.Cpu.AssertRegisters(hl: 0x10);
            model.Memory.Assert(0x10, 0xA9);
        }

        [TestMethod]
        public void IncIXd()
        {
            var ob = OpcodeByte.New(z: 4, y: 6);

            // INC (IX-1)
            var model = ExecuteTest(ob, (m) => {
                m.Cpu.FillRegisters(ix: 0x10);
                var writer = new MemoryWriter<BusData16, BusData8>(m.Memory);
                writer[new BusData16(0x0F)] = new BusData8(0xAA);
                writer[new BusData16(0x10)] = new BusData8(0x55);
            }, 0xDD, -1);

            model.Cpu.AssertRegisters(ix: 0x10);
            model.Memory.Assert(0x0F, 0xAB);
            model.Memory.Assert(0x10, 0x55);
        }

        [TestMethod]
        public void DecIXd()
        {
            var ob = OpcodeByte.New(z: 5, y: 6);

            // DEC (IX-1)
            var model = ExecuteTest(ob, (m) => {
                m.Cpu.FillRegisters(ix: 0x10);
                var writer = new MemoryWriter<BusData16, BusData8>(m.Memory);
                writer[new BusData16(0x0F)] = new BusData8(0xAA);
                writer[new BusData16(0x10)] = new BusData8(0x55);
            }, 0xDD, -1);

            model.Cpu.AssertRegisters(ix: 0x10);
            model.Memory.Assert(0x0F, 0xA9);
            model.Memory.Assert(0x10, 0x55);
        }

        [TestMethod]
        public void IncIYd()
        {
            var ob = OpcodeByte.New(z: 4, y: 6);

            // INC (IY-1)
            var model = ExecuteTest(ob, (m) => {
                m.Cpu.FillRegisters(iy: 0x10);
                var writer = new MemoryWriter<BusData16, BusData8>(m.Memory);
                writer[new BusData16(0x0F)] = new BusData8(0xAA);
                writer[new BusData16(0x10)] = new BusData8(0x55);
            }, 0xFD, -1);

            model.Cpu.AssertRegisters(iy: 0x10);
            model.Memory.Assert(0x0F, 0xAB);
            model.Memory.Assert(0x10, 0x55);
        }

        private static SimulationModel ExecuteTest(OpcodeByte ob,
                Action<SimulationModel> preTest, byte extension = 0, sbyte? d = null)
        {
            var cpuZ80 = new CpuZ80();
            var model = cpuZ80.Initialize(null);

            var writer = new MemoryWriter<BusData16, BusData8>(model.Memory);
            writer.Fill(0x48, new BusData8(0));

            if (extension == 0)
                writer[new BusData16(0)] = new BusData8(ob.Value);
            else
            {
                writer[new BusData16(0)] = new BusData8(extension);
                writer[new BusData16(1)] = new BusData8(ob.Value);
                writer[new BusData16(2)] = new BusData8((byte)d);
            }

            preTest(model);

            model.ClockGen.SquareWave(extension == 0 ? 11 : 23);

            return model;
        }
    }
}
