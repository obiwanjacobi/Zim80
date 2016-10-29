using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using Jacobi.Zim80.Components.CpuZ80.UnitTests;
using FluentAssertions;
using Jacobi.Zim80.Model;
using Jacobi.Zim80.Components.Memory.UnitTests;

namespace Jacobi.Zim80.Components.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class ShiftRotateIndirectInstructionTest
    {
        private const ushort Address = 0x05;
        private const byte Offset = 0x01;

        private const byte Value1 = 0x5E;
        private const byte Value2 = 0xAF;

        private const byte ExpectedValue1L = 0xBC;
        private const byte ExpectedValue2L = 0x5E; //+ carry

        private const byte ExpectedValue1R = 0x2F;
        private const byte ExpectedValue2R = 0x57; // + carry

        [TestMethod]
        public void RRD()
        {
            var ob = OpcodeByte.New(x: 1, z: 7, y: 4);
            var model = ExecuteTest(ob, 0xAA, (cpu) =>
            {
                cpu.Registers.PrimarySet.HL = Address;
                cpu.Registers.PrimarySet.A = 0x55;

            }, group: 0xED);

            model.Memory.Assert(Address, 0x5A);
            model.Cpu.Registers.PrimarySet.A.Should().Be(0x5A);
            model.Cpu.Registers.PrimarySet.Flags.S.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.Z.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.C.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.N.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.H.Should().Be(false);
        }

        [TestMethod]
        public void RLD()
        {
            var ob = OpcodeByte.New(x: 1, z: 7, y: 5);
            var model = ExecuteTest(ob, 0xAA, (cpu) =>
            {
                cpu.Registers.PrimarySet.HL = Address;
                cpu.Registers.PrimarySet.A = 0x55;

            }, group: 0xED);

            model.Memory.Assert(Address, 0xA5);
            model.Cpu.Registers.PrimarySet.A.Should().Be(0x5A);
            model.Cpu.Registers.PrimarySet.Flags.S.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.Z.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.C.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.N.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.H.Should().Be(false);
        }

        [TestMethod]
        public void RLC_HL_nc()
        {
            var ob = OpcodeByte.New(x: 0, z: 6, y: 0);
            var model = ExecuteTest(ob, Value1, (cpu) => 
            {
                cpu.Registers.PrimarySet.HL = Address;
                cpu.Registers.PrimarySet.Flags.Z = false;
                cpu.Registers.PrimarySet.Flags.S = true;
            });

            model.Memory.Assert(Address, ExpectedValue1L);
            model.Cpu.Registers.PrimarySet.Flags.S.Should().Be(true);
            model.Cpu.Registers.PrimarySet.Flags.Z.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.C.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.N.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.H.Should().Be(false);
        }

        [TestMethod]
        public void RLC_HL_c()
        {
            var ob = OpcodeByte.New(x: 0, z: 6, y: 0);
            var model = ExecuteTest(ob, Value2, (cpu) =>
            {
                cpu.Registers.PrimarySet.HL = Address;
                cpu.Registers.PrimarySet.Flags.Z = false;
                cpu.Registers.PrimarySet.Flags.S = true;
            });

            model.Memory.Assert(Address, ExpectedValue2L | 0x01);
            model.Cpu.Registers.PrimarySet.Flags.S.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.Z.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.C.Should().Be(true);
            model.Cpu.Registers.PrimarySet.Flags.N.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.H.Should().Be(false);
        }

        [TestMethod]
        public void RRC_HL_nc()
        {
            var ob = OpcodeByte.New(x: 0, z: 6, y: 1);
            var model = ExecuteTest(ob, Value1, (cpu) =>
            {
                cpu.Registers.PrimarySet.HL = Address;
                cpu.Registers.PrimarySet.Flags.Z = false;
                cpu.Registers.PrimarySet.Flags.S = true;
            });

            model.Memory.Assert(Address, ExpectedValue1R);
            model.Cpu.Registers.PrimarySet.Flags.S.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.Z.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.C.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.N.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.H.Should().Be(false);
        }

        [TestMethod]
        public void RRC_HL_c()
        {
            var ob = OpcodeByte.New(x: 0, z: 6, y: 1);
            var model = ExecuteTest(ob, Value2, (cpu) =>
            {
                cpu.Registers.PrimarySet.HL = Address;
                cpu.Registers.PrimarySet.Flags.Z = false;
                cpu.Registers.PrimarySet.Flags.S = true;
            });

            model.Memory.Assert(Address, ExpectedValue2R | 0x80);
            model.Cpu.Registers.PrimarySet.Flags.S.Should().Be(true);
            model.Cpu.Registers.PrimarySet.Flags.Z.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.C.Should().Be(true);
            model.Cpu.Registers.PrimarySet.Flags.N.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.H.Should().Be(false);
        }

        [TestMethod]
        public void RL_HL_nc_nc()
        {
            var ob = OpcodeByte.New(x: 0, z: 6, y: 2);
            var model = ExecuteTest(ob, Value1, (cpu) =>
            {
                cpu.Registers.PrimarySet.HL = Address;
            });

            model.Memory.Assert(Address, ExpectedValue1L);
            model.Cpu.Registers.PrimarySet.Flags.S.Should().Be(true);
            model.Cpu.Registers.PrimarySet.Flags.Z.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.C.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.N.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.H.Should().Be(false);
        }

        [TestMethod]
        public void RL_HL_c_nc()
        {
            var ob = OpcodeByte.New(x: 0, z: 6, y: 2);
            var model = ExecuteTest(ob, Value2, (cpu) =>
            {
                cpu.Registers.PrimarySet.HL = Address;
            });

            model.Memory.Assert(Address, ExpectedValue2L);
            model.Cpu.Registers.PrimarySet.Flags.S.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.Z.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.C.Should().Be(true);
            model.Cpu.Registers.PrimarySet.Flags.N.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.H.Should().Be(false);
        }

        [TestMethod]
        public void RL_HL_nc_c()
        {
            var ob = OpcodeByte.New(x: 0, z: 6, y: 2);
            var model = ExecuteTest(ob, Value1, (cpu) =>
            {
                cpu.Registers.PrimarySet.HL = Address;
                cpu.Registers.PrimarySet.Flags.C = true;
            });

            model.Memory.Assert(Address, ExpectedValue1L | 0x01);
            model.Cpu.Registers.PrimarySet.Flags.S.Should().Be(true);
            model.Cpu.Registers.PrimarySet.Flags.Z.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.C.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.N.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.H.Should().Be(false);
        }

        [TestMethod]
        public void RL_HL_c_c()
        {
            var ob = OpcodeByte.New(x: 0, z: 6, y: 2);
            var model = ExecuteTest(ob, Value2, (cpu) =>
            {
                cpu.Registers.PrimarySet.HL = Address;
                cpu.Registers.PrimarySet.Flags.C = true;
            });

            model.Memory.Assert(Address, ExpectedValue2L | 0x01);
            model.Cpu.Registers.PrimarySet.Flags.S.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.Z.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.C.Should().Be(true);
            model.Cpu.Registers.PrimarySet.Flags.N.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.H.Should().Be(false);
        }

        [TestMethod]
        public void RR_HL_nc_nc()
        {
            var ob = OpcodeByte.New(x: 0, z: 6, y: 3);
            var model = ExecuteTest(ob, Value1, (cpu) =>
            {
                cpu.Registers.PrimarySet.HL = Address;
            });

            model.Memory.Assert(Address, ExpectedValue1R);
            model.Cpu.Registers.PrimarySet.Flags.S.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.Z.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.C.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.N.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.H.Should().Be(false);
        }

        [TestMethod]
        public void RR_HL_c_nc()
        {
            var ob = OpcodeByte.New(x: 0, z: 6, y: 3);
            var model = ExecuteTest(ob, Value2, (cpu) =>
            {
                cpu.Registers.PrimarySet.HL = Address;
            });

            model.Memory.Assert(Address, ExpectedValue2R);
            model.Cpu.Registers.PrimarySet.Flags.S.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.Z.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.C.Should().Be(true);
            model.Cpu.Registers.PrimarySet.Flags.N.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.H.Should().Be(false);
        }

        [TestMethod]
        public void RR_HL_nc_c()
        {
            var ob = OpcodeByte.New(x: 0, z: 6, y: 3);
            var model = ExecuteTest(ob, Value1, (cpu) =>
            {
                cpu.Registers.PrimarySet.HL = Address;
                cpu.Registers.PrimarySet.Flags.C = true;
            });

            model.Memory.Assert(Address, ExpectedValue1R | 0x80);
            model.Cpu.Registers.PrimarySet.Flags.S.Should().Be(true);
            model.Cpu.Registers.PrimarySet.Flags.Z.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.C.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.N.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.H.Should().Be(false);
        }

        [TestMethod]
        public void RR_HL_c_c()
        {
            var ob = OpcodeByte.New(x: 0, z: 6, y: 3);
            var model = ExecuteTest(ob, Value2, (cpu) =>
            {
                cpu.Registers.PrimarySet.HL = Address;
                cpu.Registers.PrimarySet.Flags.C = true;
            });

            model.Memory.Assert(Address, ExpectedValue2R | 0x80);
            model.Cpu.Registers.PrimarySet.Flags.S.Should().Be(true);
            model.Cpu.Registers.PrimarySet.Flags.Z.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.C.Should().Be(true);
            model.Cpu.Registers.PrimarySet.Flags.N.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.H.Should().Be(false);
        }

        [TestMethod]
        public void SLA_HL_nc()
        {
            var ob = OpcodeByte.New(x: 0, z: 6, y: 4);
            var model = ExecuteTest(ob, Value1, (cpu) =>
            {
                cpu.Registers.PrimarySet.HL = Address;
            });

            model.Memory.Assert(Address, ExpectedValue1L);
            model.Cpu.Registers.PrimarySet.Flags.S.Should().Be(true);
            model.Cpu.Registers.PrimarySet.Flags.Z.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.C.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.N.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.H.Should().Be(false);
        }

        [TestMethod]
        public void SLA_HL_c()
        {
            var ob = OpcodeByte.New(x: 0, z: 6, y: 4);
            var model = ExecuteTest(ob, Value2, (cpu) =>
            {
                cpu.Registers.PrimarySet.HL = Address;
            });

            model.Memory.Assert(Address, ExpectedValue2L);
            model.Cpu.Registers.PrimarySet.Flags.S.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.Z.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.C.Should().Be(true);
            model.Cpu.Registers.PrimarySet.Flags.N.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.H.Should().Be(false);
        }

        [TestMethod]
        public void SRA_HL_nc()
        {
            var ob = OpcodeByte.New(x: 0, z: 6, y: 5);
            var model = ExecuteTest(ob, Value1, (cpu) =>
            {
                cpu.Registers.PrimarySet.HL = Address;
            });

            model.Memory.Assert(Address, ExpectedValue1R);
            model.Cpu.Registers.PrimarySet.Flags.S.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.Z.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.C.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.N.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.H.Should().Be(false);
        }

        [TestMethod]
        public void SRA_HL_c()
        {
            var ob = OpcodeByte.New(x: 0, z: 6, y: 5);
            var model = ExecuteTest(ob, Value2, (cpu) =>
            {
                cpu.Registers.PrimarySet.HL = Address;
            });

            model.Memory.Assert(Address, ExpectedValue2R | 0x80);
            model.Cpu.Registers.PrimarySet.Flags.S.Should().Be(true);
            model.Cpu.Registers.PrimarySet.Flags.Z.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.C.Should().Be(true);
            model.Cpu.Registers.PrimarySet.Flags.N.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.H.Should().Be(false);
        }

        [TestMethod]
        public void SLL_HL_nc()
        {
            var ob = OpcodeByte.New(x: 0, z: 6, y: 6);
            var model = ExecuteTest(ob, Value1, (cpu) =>
            {
                cpu.Registers.PrimarySet.HL = Address;
            });

            model.Memory.Assert(Address, ExpectedValue1L);
            model.Cpu.Registers.PrimarySet.Flags.S.Should().Be(true);
            model.Cpu.Registers.PrimarySet.Flags.Z.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.C.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.N.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.H.Should().Be(false);
        }

        [TestMethod]
        public void SLL_HL_c()
        {
            var ob = OpcodeByte.New(x: 0, z: 6, y: 6);
            var model = ExecuteTest(ob, Value2, (cpu) =>
            {
                cpu.Registers.PrimarySet.HL = Address;
            });

            model.Memory.Assert(Address, ExpectedValue2L);
            model.Cpu.Registers.PrimarySet.Flags.S.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.Z.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.C.Should().Be(true);
            model.Cpu.Registers.PrimarySet.Flags.N.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.H.Should().Be(false);
        }

        [TestMethod]
        public void SRL_HL_nc()
        {
            var ob = OpcodeByte.New(x: 0, z: 6, y: 7);
            var model = ExecuteTest(ob, Value1, (cpu) =>
            {
                cpu.Registers.PrimarySet.HL = Address;
            });

            model.Memory.Assert(Address, ExpectedValue1R);
            model.Cpu.Registers.PrimarySet.Flags.S.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.Z.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.C.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.N.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.H.Should().Be(false);
        }

        [TestMethod]
        public void SRL_HL_c()
        {
            var ob = OpcodeByte.New(x: 0, z: 6, y: 7);
            var model = ExecuteTest(ob, Value2, (cpu) =>
            {
                cpu.Registers.PrimarySet.HL = Address;
            });

            model.Memory.Assert(Address, ExpectedValue2R);
            model.Cpu.Registers.PrimarySet.Flags.S.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.Z.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.C.Should().Be(true);
            model.Cpu.Registers.PrimarySet.Flags.N.Should().Be(false);
            model.Cpu.Registers.PrimarySet.Flags.H.Should().Be(false);
        }

        private static SimulationModel ExecuteTest(OpcodeByte ob, byte value, Action<CpuZ80> preTest, byte extension = 0, byte group = 0xCB)
        {
            var cpuZ80 = new CpuZ80();
            byte[] buffer = extension == 0 ?
                    new byte[] { group, ob.Value, 0, 0, 0, value }:
                    new byte[] { extension, group, ob.Value, Offset, 0, 0, value };
            var model = cpuZ80.Initialize(buffer);

            cpuZ80.FillRegisters();
            preTest(cpuZ80);

            long clocks = group == 0xCB ? 15 : 18;
            if (extension != 0) clocks += 8;
            model.ClockGen.BlockWave(clocks);

            return model;
        }
    }
}
