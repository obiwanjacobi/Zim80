using Jacobi.Zim80.CpuZ80.Opcodes;
using Jacobi.Zim80.CpuZ80.UnitTests;
using Jacobi.Zim80.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Jacobi.Zim80.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class InInstructionTest
    {
        private const byte IoAddress = 0x02;
        private const byte Value = 0x55;

        [TestMethod]
        public void InA_n_()
        {
            var ob = OpcodeByte.New(x: 3, z: 3, y: 3);
            var model = ExecuteTest(ob, (cpu) => cpu.Registers.A = 0);

            model.Cpu.AssertRegisters(a: Value);
        }

        [TestMethod]
        public void InB_C_()
        {
            var ob = OpcodeByte.New(x: 1, z: 0, y: 0);
            var model = ExecuteTest(ob, (cpu) => {
                    cpu.Registers.BC = IoAddress;
                }, 0xED);

            model.Cpu.AssertRegisters(bc: Value << 8 | IoAddress);
        }

        [TestMethod]
        public void InC_C_()
        {
            var ob = OpcodeByte.New(x: 1, z: 0, y: 1);
            var model = ExecuteTest(ob, (cpu) => {
                cpu.Registers.BC = IoAddress;
            }, 0xED);

            model.Cpu.AssertRegisters(bc: Value);
        }

        [TestMethod]
        public void InD_C_()
        {
            var ob = OpcodeByte.New(x: 1, z: 0, y: 2);
            var model = ExecuteTest(ob, (cpu) => {
                cpu.Registers.BC = IoAddress;
            }, 0xED);

            model.Cpu.AssertRegisters(de: Value << 8 | CpuZ80TestExtensions.MagicValue, bc: IoAddress);
        }

        [TestMethod]
        public void InE_C_()
        {
            var ob = OpcodeByte.New(x: 1, z: 0, y: 3);
            var model = ExecuteTest(ob, (cpu) => {
                cpu.Registers.BC = IoAddress;
            }, 0xED);

            model.Cpu.AssertRegisters(de: Value, bc: IoAddress);
        }

        [TestMethod]
        public void InH_C_()
        {
            var ob = OpcodeByte.New(x: 1, z: 0, y: 4);
            var model = ExecuteTest(ob, (cpu) => {
                cpu.Registers.BC = IoAddress;
            }, 0xED);

            model.Cpu.AssertRegisters(hl: Value << 8 | CpuZ80TestExtensions.MagicValue, bc: IoAddress);
        }

        [TestMethod]
        public void InL_C_()
        {
            var ob = OpcodeByte.New(x: 1, z: 0, y: 5);
            var model = ExecuteTest(ob, (cpu) => {
                cpu.Registers.BC = IoAddress;
            }, 0xED);

            model.Cpu.AssertRegisters(hl: Value, bc: IoAddress);
        }

        [TestMethod]
        public void In_C_()
        {
            var ob = OpcodeByte.New(x: 1, z: 0, y: 6);
            var model = ExecuteTest(ob, (cpu) => {
                cpu.Registers.BC = IoAddress;
            }, 0xED);

            model.Cpu.AssertRegisters(bc: IoAddress);
        }

        [TestMethod]
        public void InA_C_()
        {
            var ob = OpcodeByte.New(x: 1, z: 0, y: 7);
            var model = ExecuteTest(ob, (cpu) => {
                cpu.Registers.BC = IoAddress;
            }, 0xED);

            model.Cpu.AssertRegisters(a: Value, bc: IoAddress);
        }


        private SimulationModel ExecuteTest(OpcodeByte ob, Action<CpuZ80> preTest, byte extension = 0)
        {
            var cpu = new CpuZ80();

            var buffer = extension == 0 ?
                new byte[] { ob.Value, IoAddress } :
                new byte[] { extension, ob.Value };

            var model = cpu.Initialize(buffer, new byte[] { 0, 0, Value, 0, 0 });

            cpu.FillRegisters();
            preTest(cpu);

            model.ClockGen.SquareWave(extension == 0 ? 11 : 12);

            return model;
        }
    }
}
