using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using Jacobi.Zim80.Components.CpuZ80.UnitTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using static Jacobi.Zim80.Components.CpuZ80.Registers;

namespace Jacobi.Zim80.Components.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class JumpInstructionTest
    {
        private const ushort Address = 0x03;
        private const ushort JumpAddress = 0xAA55;
        private const byte AddressLsb = 0x55;
        private const byte AddressMsb = 0xAA;

        [TestMethod]
        public void JPnn()
        {
            var ob = OpcodeByte.New(x: 3, z: 3, y: 0);

            var cpu = ExecuteTest(ob, (r) => true);

            cpu.AssertRegisters(pc: JumpAddress);
        }

        

        [TestMethod]
        public void JPnn_nz_NZ()
        {
            var ob = OpcodeByte.New(x: 3, z: 2, y: 0);

            var cpu = ExecuteTest(ob, (r) => {
                return true;
            });

            cpu.AssertRegisters(pc: JumpAddress);
        }

        [TestMethod]
        public void JPnn_nz_Z()
        {
            var ob = OpcodeByte.New(x: 3, z: 2, y: 0);

            var cpu = ExecuteTest(ob, (r) => {
                r.Flags.Z = true;
                return false;
            });

            cpu.AssertRegisters(pc: Address);
        }

        [TestMethod]
        public void JPnn_z_NZ()
        {
            var ob = OpcodeByte.New(x: 3, z: 2, y: 1);

            var cpu = ExecuteTest(ob, (r) => {
                return false;
            });

            cpu.AssertRegisters(pc: Address);
        }

        [TestMethod]
        public void JPnn_z_Z()
        {
            var ob = OpcodeByte.New(x: 3, z: 2, y: 1);

            var cpu = ExecuteTest(ob, (r) => {
                r.Flags.Z = true;
                return true;
            });

            cpu.AssertRegisters(pc: JumpAddress);
        }

        [TestMethod]
        public void JPnn_nc_NC()
        {
            var ob = OpcodeByte.New(x: 3, z: 2, y: 2);

            var cpu = ExecuteTest(ob, (r) => {
                return true;
            });

            cpu.AssertRegisters(pc: JumpAddress);
        }

        [TestMethod]
        public void JPnn_nc_C()
        {
            var ob = OpcodeByte.New(x: 3, z: 2, y: 2);

            var cpu = ExecuteTest(ob, (r) => {
                r.Flags.C = true;
                return false;
            });

            cpu.AssertRegisters(pc: Address);
        }

        [TestMethod]
        public void JPnn_c_NC()
        {
            var ob = OpcodeByte.New(x: 3, z: 2, y: 3);

            var cpu = ExecuteTest(ob, (r) => {
                return false;
            });

            cpu.AssertRegisters(pc: Address);
        }

        [TestMethod]
        public void JPnn_c_C()
        {
            var ob = OpcodeByte.New(x: 3, z: 2, y: 3);

            var cpu = ExecuteTest(ob, (r) => {
                r.Flags.C = true;
                return true;
            });

            cpu.AssertRegisters(pc: JumpAddress);
        }

        [TestMethod]
        public void JPnn_po_NP()
        {
            var ob = OpcodeByte.New(x: 3, z: 2, y: 4);

            var cpu = ExecuteTest(ob, (r) => {
                return true;
            });

            cpu.AssertRegisters(pc: JumpAddress);
        }

        [TestMethod]
        public void JPnn_po_P()
        {
            var ob = OpcodeByte.New(x: 3, z: 2, y: 4);

            var cpu = ExecuteTest(ob, (r) => {
                r.Flags.PV = true;
                return false;
            });

            cpu.AssertRegisters(pc: Address);
        }

        [TestMethod]
        public void JPnn_pe_NP()
        {
            var ob = OpcodeByte.New(x: 3, z: 2, y: 5);

            var cpu = ExecuteTest(ob, (r) => {
                return false;
            });

            cpu.AssertRegisters(pc: Address);
        }

        [TestMethod]
        public void JPnn_pe_P()
        {
            var ob = OpcodeByte.New(x: 3, z: 2, y: 5);

            var cpu = ExecuteTest(ob, (r) => {
                r.Flags.PV = true;
                return true;
            });

            cpu.AssertRegisters(pc: JumpAddress);
        }

        [TestMethod]
        public void JPnn_p_NS()
        {
            var ob = OpcodeByte.New(x: 3, z: 2, y: 6);

            var cpu = ExecuteTest(ob, (r) => {
                return true;
            });

            cpu.AssertRegisters(pc: JumpAddress);
        }

        [TestMethod]
        public void JPnn_p_S()
        {
            var ob = OpcodeByte.New(x: 3, z: 2, y: 6);

            var cpu = ExecuteTest(ob, (r) => {
                r.Flags.S = true;
                return false;
            });

            cpu.AssertRegisters(pc: Address);
        }

        [TestMethod]
        public void JPnn_m_NS()
        {
            var ob = OpcodeByte.New(x: 3, z: 2, y: 7);

            var cpu = ExecuteTest(ob, (r) => {
                return false;
            });

            cpu.AssertRegisters(pc: Address);
        }

        [TestMethod]
        public void JPnn_m_S()
        {
            var ob = OpcodeByte.New(x: 3, z: 2, y: 7);

            var cpu = ExecuteTest(ob, (r) => {
                r.Flags.S = true;
                return true;
            });

            cpu.AssertRegisters(pc: JumpAddress);
        }

        private static CpuZ80 ExecuteTest(OpcodeByte ob, Func<RegisterSet, bool> preTest)
        {
            var cpuZ80 = new CpuZ80();
            var buffer = new[] { ob.Value, AddressLsb, AddressMsb };
            var model = cpuZ80.Initialize(buffer);

            model.Cpu.FillRegisters();
            var conditionMet = preTest(cpuZ80.Registers.PrimarySet);

            var def = OpcodeDefinition.Find(ob);
            model.ClockGen.BlockWave(def.Cycles.Sum());

            return cpuZ80;
        }
    }
}
