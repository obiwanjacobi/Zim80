using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.Test;
using Jacobi.Zim80.CpuZ80.Opcodes;
using Jacobi.Zim80.CpuZ80.UnitTests;
using System.Linq;
using Jacobi.Zim80.UnitTests;
using Jacobi.Zim80.Diagnostics;

namespace Jacobi.Zim80.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class RetInstructionTest
    {
        private const ushort Stack = 0x04;
        private const ushort Value = 0xAA55;

        [TestMethod]
        public void RET()
        {
            var ret = OpcodeByte.New(x: 3, z: 1, q: 1);
            var model = ExecuteTest(ret, (_) => { }, conditionMet:true);

            model.Cpu.AssertRegisters(sp: Stack + 2, pc: Value);
        }

        [TestMethod]
        public void RETnz_Z()
        {
            var ret = OpcodeByte.New(x: 3, z: 0, y: 0);
            var model = ExecuteTest(ret, (m) => m.Cpu.Registers.Flags.Z = true, 
                conditionMet: false);

            model.Cpu.AssertRegisters(sp: Stack, pc: 1);
        }

        [TestMethod]
        public void RETnz_NZ()
        {
            var ret = OpcodeByte.New(x: 3, z: 0, y: 0);
            var model = ExecuteTest(ret, (m) => m.Cpu.Registers.Flags.Z = false, 
                conditionMet: true);

            model.Cpu.AssertRegisters(sp: Stack + 2, pc: Value);
        }

        [TestMethod]
        public void RETz_NZ()
        {
            var ret = OpcodeByte.New(x: 3, z: 0, y: 1);
            var model = ExecuteTest(ret, (m) => m.Cpu.Registers.Flags.Z = false, 
                conditionMet: false);

            model.Cpu.AssertRegisters(sp: Stack, pc: 1);
        }

        [TestMethod]
        public void RETz_Z()
        {
            var ret = OpcodeByte.New(x: 3, z: 0, y: 1);
            var model = ExecuteTest(ret, (m) => m.Cpu.Registers.Flags.Z = true, 
                conditionMet: true);

            model.Cpu.AssertRegisters(sp: Stack + 2, pc: Value);
        }

        [TestMethod]
        public void RETnc_C()
        {
            var ret = OpcodeByte.New(x: 3, z: 0, y: 2);
            var model = ExecuteTest(ret, (m) => m.Cpu.Registers.Flags.C = true, 
                conditionMet: false);

            model.Cpu.AssertRegisters(sp: Stack, pc: 1);
        }

        [TestMethod]
        public void RETnc_NC()
        {
            var ret = OpcodeByte.New(x: 3, z: 0, y: 2);
            var model = ExecuteTest(ret, (m) => m.Cpu.Registers.Flags.C = false, 
                conditionMet: true);

            model.Cpu.AssertRegisters(sp: Stack + 2, pc: Value);
        }

        [TestMethod]
        public void RETc_NC()
        {
            var ret = OpcodeByte.New(x: 3, z: 0, y: 3);
            var model = ExecuteTest(ret, (m) => m.Cpu.Registers.Flags.C = false, 
                conditionMet: false);

            model.Cpu.AssertRegisters(sp: Stack, pc: 1);
        }

        [TestMethod]
        public void RETc_C()
        {
            var ret = OpcodeByte.New(x: 3, z: 0, y: 3);
            var model = ExecuteTest(ret, (m) => m.Cpu.Registers.Flags.C = true, 
                conditionMet: true);

            model.Cpu.AssertRegisters(sp: Stack + 2, pc: Value);
        }

        [TestMethod]
        public void RETpo_NP()
        {
            var ret = OpcodeByte.New(x: 3, z: 0, y: 4);
            var model = ExecuteTest(ret, (m) => m.Cpu.Registers.Flags.PV = true, 
                conditionMet: false);

            model.Cpu.AssertRegisters(sp: Stack, pc: 1);
        }

        [TestMethod]
        public void RETpo_P()
        {
            var ret = OpcodeByte.New(x: 3, z: 0, y: 4);
            var model = ExecuteTest(ret, (m) => m.Cpu.Registers.Flags.PV = false, 
                conditionMet: true);

            model.Cpu.AssertRegisters(sp: Stack + 2, pc: Value);
        }

        [TestMethod]
        public void RETpe_P()
        {
            var ret = OpcodeByte.New(x: 3, z: 0, y: 5);
            var model = ExecuteTest(ret, (m) => m.Cpu.Registers.Flags.PV = false, 
                conditionMet: false);

            model.Cpu.AssertRegisters(sp: Stack, pc: 1);
        }

        [TestMethod]
        public void RETpe_NP()
        {
            var ret = OpcodeByte.New(x: 3, z: 0, y: 5);
            var model = ExecuteTest(ret, (m) => m.Cpu.Registers.Flags.PV = true, 
                conditionMet: true);

            model.Cpu.AssertRegisters(sp: Stack + 2, pc: Value);
        }

        [TestMethod]
        public void RETp_NS()
        {
            var ret = OpcodeByte.New(x: 3, z: 0, y: 6);
            var model = ExecuteTest(ret, (m) => m.Cpu.Registers.Flags.S = true, 
                conditionMet: false);

            model.Cpu.AssertRegisters(sp: Stack, pc: 1);
        }

        [TestMethod]
        public void RETp_S()
        {
            var ret = OpcodeByte.New(x: 3, z: 0, y: 6);
            var model = ExecuteTest(ret, (m) => m.Cpu.Registers.Flags.S = false, 
                conditionMet: true);

            model.Cpu.AssertRegisters(sp: Stack + 2, pc: Value);
        }

        [TestMethod]
        public void RETm_S()
        {
            var ret = OpcodeByte.New(x: 3, z: 0, y: 7);
            var model = ExecuteTest(ret, (m) => m.Cpu.Registers.Flags.S = false, 
                conditionMet: false);

            model.Cpu.AssertRegisters(sp: Stack, pc: 1);
        }

        [TestMethod]
        public void RETm_NS()
        {
            var ret = OpcodeByte.New(x: 3, z: 0, y: 7);
            var model = ExecuteTest(ret, (m) => m.Cpu.Registers.Flags.S = true, 
                conditionMet: true);

            model.Cpu.AssertRegisters(sp: Stack + 2, pc: Value);
        }

        private static SimulationModel ExecuteTest(OpcodeByte ret, 
            Action<SimulationModel> preTest, bool conditionMet, 
            byte extension = 0)
        {
            var cpuZ80 = new CpuZ80();

            var buffer = (extension == 0) ?
                new byte[] { ret.Value, 0, 0, 0, 0x55, 0xAA } :
                new byte[] { extension, ret.Value, 0, 0, 0x55, 0xAA };
            var model = cpuZ80.Initialize(buffer);

            model.Cpu.FillRegisters(sp: Stack);

            preTest(model);

            var def = OpcodeDefinition.Find(ret, extension == 0 ? null : new OpcodeByte(extension));
            model.ClockGen.SquareWave(conditionMet ? def.Cycles.Sum() : def.AltCycles.Sum());

            Console.WriteLine(model.LogicAnalyzer.ToWaveJson());

            return model;
        }
    }
}
