using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.Model;
using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using Jacobi.Zim80.Components.CpuZ80.UnitTests;
using Jacobi.Zim80.Components.Memory;
using System.Linq;

namespace Jacobi.Zim80.Components.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class RetInstructionTest
    {
        private const ushort Stack = 0x20;

        [TestMethod]
        public void RET()
        {
            var ret = OpcodeByte.New(x: 3, z: 1, q: 1);
            var model = ExecuteTest(ret, (_) => { }, conditionMet:true);

            model.Cpu.AssertRegisters(sp: Stack + 2, pc: 0xAA55);
        }

        [TestMethod]
        public void RETnz_Z()
        {
            var ret = OpcodeByte.New(x: 3, z: 0, y: 0);
            var model = ExecuteTest(ret, (m) => m.Cpu.Registers.Flags.Z = true, conditionMet: false);

            model.Cpu.AssertRegisters(sp: Stack, pc: 1);
        }

        [TestMethod]
        public void RETnz_NZ()
        {
            var ret = OpcodeByte.New(x: 3, z: 0, y: 0);
            var model = ExecuteTest(ret, (m) => m.Cpu.Registers.Flags.Z = false, conditionMet: true);

            model.Cpu.AssertRegisters(sp: Stack + 2, pc: 0xAA55);
        }

        [TestMethod]
        public void RETz_NZ()
        {
            var ret = OpcodeByte.New(x: 3, z: 0, y: 1);
            var model = ExecuteTest(ret, (m) => m.Cpu.Registers.Flags.Z = false, conditionMet: false);

            model.Cpu.AssertRegisters(sp: Stack, pc: 1);
        }

        [TestMethod]
        public void RETz_Z()
        {
            var ret = OpcodeByte.New(x: 3, z: 0, y: 1);
            var model = ExecuteTest(ret, (m) => m.Cpu.Registers.Flags.Z = true, conditionMet: true);

            model.Cpu.AssertRegisters(sp: Stack + 2, pc: 0xAA55);
        }

        [TestMethod]
        public void RETnc_C()
        {
            var ret = OpcodeByte.New(x: 3, z: 0, y: 2);
            var model = ExecuteTest(ret, (m) => m.Cpu.Registers.Flags.C = true, conditionMet: false);

            model.Cpu.AssertRegisters(sp: Stack, pc: 1);
        }

        [TestMethod]
        public void RETnc_NC()
        {
            var ret = OpcodeByte.New(x: 3, z: 0, y: 2);
            var model = ExecuteTest(ret, (m) => m.Cpu.Registers.Flags.C = false, conditionMet: true);

            model.Cpu.AssertRegisters(sp: Stack + 2, pc: 0xAA55);
        }

        [TestMethod]
        public void RETc_NC()
        {
            var ret = OpcodeByte.New(x: 3, z: 0, y: 3);
            var model = ExecuteTest(ret, (m) => m.Cpu.Registers.Flags.C = false, conditionMet: false);

            model.Cpu.AssertRegisters(sp: Stack, pc: 1);
        }

        [TestMethod]
        public void RETc_C()
        {
            var ret = OpcodeByte.New(x: 3, z: 0, y: 3);
            var model = ExecuteTest(ret, (m) => m.Cpu.Registers.Flags.C = true, conditionMet: true);

            model.Cpu.AssertRegisters(sp: Stack + 2, pc: 0xAA55);
        }

        [TestMethod]
        public void RETpo_NP()
        {
            var ret = OpcodeByte.New(x: 3, z: 0, y: 4);
            var model = ExecuteTest(ret, (m) => m.Cpu.Registers.Flags.PV = true, conditionMet: false);

            model.Cpu.AssertRegisters(sp: Stack, pc: 1);
        }

        [TestMethod]
        public void RETpo_P()
        {
            var ret = OpcodeByte.New(x: 3, z: 0, y: 4);
            var model = ExecuteTest(ret, (m) => m.Cpu.Registers.Flags.PV = false, conditionMet: true);

            model.Cpu.AssertRegisters(sp: Stack + 2, pc: 0xAA55);
        }

        [TestMethod]
        public void RETpe_P()
        {
            var ret = OpcodeByte.New(x: 3, z: 0, y: 5);
            var model = ExecuteTest(ret, (m) => m.Cpu.Registers.Flags.PV = false, conditionMet: false);

            model.Cpu.AssertRegisters(sp: Stack, pc: 1);
        }

        [TestMethod]
        public void RETpe_NP()
        {
            var ret = OpcodeByte.New(x: 3, z: 0, y: 5);
            var model = ExecuteTest(ret, (m) => m.Cpu.Registers.Flags.PV = true, conditionMet: true);

            model.Cpu.AssertRegisters(sp: Stack + 2, pc: 0xAA55);
        }

        [TestMethod]
        public void RETp_NS()
        {
            var ret = OpcodeByte.New(x: 3, z: 0, y: 6);
            var model = ExecuteTest(ret, (m) => m.Cpu.Registers.Flags.S = true, conditionMet: false);

            model.Cpu.AssertRegisters(sp: Stack, pc: 1);
        }

        [TestMethod]
        public void RETp_S()
        {
            var ret = OpcodeByte.New(x: 3, z: 0, y: 6);
            var model = ExecuteTest(ret, (m) => m.Cpu.Registers.Flags.S = false, conditionMet: true);

            model.Cpu.AssertRegisters(sp: Stack + 2, pc: 0xAA55);
        }

        [TestMethod]
        public void RETm_S()
        {
            var ret = OpcodeByte.New(x: 3, z: 0, y: 7);
            var model = ExecuteTest(ret, (m) => m.Cpu.Registers.Flags.S = false, conditionMet: false);

            model.Cpu.AssertRegisters(sp: Stack, pc: 1);
        }

        [TestMethod]
        public void RETm_NS()
        {
            var ret = OpcodeByte.New(x: 3, z: 0, y: 7);
            var model = ExecuteTest(ret, (m) => m.Cpu.Registers.Flags.S = true, conditionMet: true);

            model.Cpu.AssertRegisters(sp: Stack + 2, pc: 0xAA55);
        }

        private static SimulationModel ExecuteTest(OpcodeByte ret, Action<SimulationModel> preTest, bool conditionMet, byte extension = 0)
        {
            var cpuZ80 = new CpuZ80();
            var model = cpuZ80.Initialize(null);

            var writer = new MemoryWriter<BusData16, BusData8>(model.Memory);
            writer.Fill(0x48, new BusData8(0));
            if (extension == 0)
                writer[new BusData16(0)] = new BusData8(ret.Value);
            else
            {
                writer[new BusData16(0)] = new BusData8(extension);
                writer[new BusData16(1)] = new BusData8(ret.Value);
            }
            writer[new BusData16(Stack)] = new BusData8(0x55);
            writer[new BusData16(Stack + 1)] = new BusData8(0xAA);

            model.Cpu.FillRegisters(sp: Stack);

            if (preTest != null) preTest(model);

            var def = OpcodeDefinition.Find(ret, extension == 0 ? null : new OpcodeByte(extension));
            model.ClockGen.BlockWave(conditionMet ? def.Cycles.Sum() : def.AltCycles.Sum());

            return model;
        }
    }
}
