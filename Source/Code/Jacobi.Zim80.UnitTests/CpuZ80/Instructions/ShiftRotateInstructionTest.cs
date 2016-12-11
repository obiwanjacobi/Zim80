using FluentAssertions;
using Jacobi.Zim80.CpuZ80.Opcodes;
using Jacobi.Zim80.CpuZ80.UnitTests;
using Jacobi.Zim80.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Jacobi.Zim80.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class ShiftRotateInstructionTest
    {
        private const byte Value1 = 0x5E;
        private const byte Value2 = 0xAF;

        private const byte ExpectedValue1L = 0xBC;
        private const byte ExpectedValue2L = 0x5E; //+ carry

        private const byte ExpectedValue1R = 0x2F; 
        private const byte ExpectedValue2R = 0x57; // + carry

        [TestMethod]
        public void RLCA_nc()
        {
            var ob = OpcodeByte.New(z: 7, y: 0);
            var cpu = ExecuteTest(ob, (cpuZ80) =>
                {
                    cpuZ80.Registers.A = Value1;
                    cpuZ80.Registers.Flags.Z = true;
                    cpuZ80.Registers.Flags.S = true;
                });

            cpu.AssertRegisters(a: ExpectedValue1L);
            cpu.Registers.Flags.S.Should().BeTrue();
            cpu.Registers.Flags.Z.Should().BeTrue();
            cpu.Registers.Flags.C.Should().BeFalse();
            cpu.Registers.Flags.N.Should().BeFalse();
            cpu.Registers.Flags.H.Should().BeFalse();
        }

        [TestMethod]
        public void RLCA_c()
        {
            var ob = OpcodeByte.New(z: 7, y: 0);
            var cpu = ExecuteTest(ob, (cpuZ80) =>
            {
                cpuZ80.Registers.A = Value2;
                cpuZ80.Registers.Flags.Z = false;
                cpuZ80.Registers.Flags.S = true;
            });

            cpu.AssertRegisters(a: ExpectedValue2L | 0x01);
            cpu.Registers.Flags.S.Should().BeTrue();
            cpu.Registers.Flags.Z.Should().BeFalse();
            cpu.Registers.Flags.C.Should().BeTrue();
            cpu.Registers.Flags.N.Should().BeFalse();
            cpu.Registers.Flags.H.Should().BeFalse();
        }

        [TestMethod]
        public void RRCA_nc()
        {
            var ob = OpcodeByte.New(z: 7, y: 1);
            var cpu = ExecuteTest(ob, (cpuZ80) =>
            {
                cpuZ80.Registers.A = Value1;
                cpuZ80.Registers.Flags.Z = true;
                cpuZ80.Registers.Flags.S = false;
            });

            cpu.AssertRegisters(a: ExpectedValue1R);
            cpu.Registers.Flags.S.Should().BeFalse();
            cpu.Registers.Flags.Z.Should().BeTrue();
            cpu.Registers.Flags.C.Should().BeFalse();
            cpu.Registers.Flags.N.Should().BeFalse();
            cpu.Registers.Flags.H.Should().BeFalse();
        }

        [TestMethod]
        public void RRCA_c()
        {
            var ob = OpcodeByte.New(z: 7, y: 1);
            var cpu = ExecuteTest(ob, (cpuZ80) =>
            {
                cpuZ80.Registers.A = Value2;
                cpuZ80.Registers.Flags.Z = true;
                cpuZ80.Registers.Flags.S = false;
            });

            cpu.AssertRegisters(a: ExpectedValue2R | 0x80);
            cpu.Registers.Flags.S.Should().BeFalse();
            cpu.Registers.Flags.Z.Should().BeTrue();
            cpu.Registers.Flags.C.Should().BeTrue();
            cpu.Registers.Flags.N.Should().BeFalse();
            cpu.Registers.Flags.H.Should().BeFalse();
        }

        [TestMethod]
        public void RLA_nc_nc()
        {
            var ob = OpcodeByte.New(z: 7, y: 2);
            var cpu = ExecuteTest(ob, (cpuZ80) =>
            {
                cpuZ80.Registers.A = Value1;
                cpuZ80.Registers.Flags.Z = false;
                cpuZ80.Registers.Flags.S = true;
            });

            cpu.AssertRegisters(a: ExpectedValue1L);
            cpu.Registers.Flags.S.Should().BeTrue();
            cpu.Registers.Flags.Z.Should().BeFalse();
            cpu.Registers.Flags.C.Should().BeFalse();
            cpu.Registers.Flags.N.Should().BeFalse();
            cpu.Registers.Flags.H.Should().BeFalse();
        }

        [TestMethod]
        public void RLA_nc_c()
        {
            var ob = OpcodeByte.New(z: 7, y: 2);
            var cpu = ExecuteTest(ob, (cpuZ80) =>
            {
                cpuZ80.Registers.A = Value1;
                cpuZ80.Registers.Flags.Z = false;
                cpuZ80.Registers.Flags.S = true;
                cpuZ80.Registers.Flags.C = true;
            });

            cpu.AssertRegisters(a: ExpectedValue1L | 0x01);
            cpu.Registers.Flags.S.Should().BeTrue();
            cpu.Registers.Flags.Z.Should().BeFalse();
            cpu.Registers.Flags.C.Should().BeFalse();
            cpu.Registers.Flags.N.Should().BeFalse();
            cpu.Registers.Flags.H.Should().BeFalse();
        }

        [TestMethod]
        public void RLA_c_nc()
        {
            var ob = OpcodeByte.New(z: 7, y: 2);
            var cpu = ExecuteTest(ob, (cpuZ80) =>
            {
                cpuZ80.Registers.A = Value2;
                cpuZ80.Registers.Flags.Z = false;
                cpuZ80.Registers.Flags.S = true;
            });

            cpu.AssertRegisters(a: ExpectedValue2L);
            cpu.Registers.Flags.S.Should().BeTrue();
            cpu.Registers.Flags.Z.Should().BeFalse();
            cpu.Registers.Flags.C.Should().BeTrue();
            cpu.Registers.Flags.N.Should().BeFalse();
            cpu.Registers.Flags.H.Should().BeFalse();
        }

        [TestMethod]
        public void RLA_c_c()
        {
            var ob = OpcodeByte.New(z: 7, y: 2);
            var cpu = ExecuteTest(ob, (cpuZ80) =>
            {
                cpuZ80.Registers.A = Value2;
                cpuZ80.Registers.Flags.Z = false;
                cpuZ80.Registers.Flags.S = true;
                cpuZ80.Registers.Flags.C = true;
            });

            cpu.AssertRegisters(a: ExpectedValue2L | 0x01);
            cpu.Registers.Flags.S.Should().BeTrue();
            cpu.Registers.Flags.Z.Should().BeFalse();
            cpu.Registers.Flags.C.Should().BeTrue();
            cpu.Registers.Flags.N.Should().BeFalse();
            cpu.Registers.Flags.H.Should().BeFalse();
        }

        [TestMethod]
        public void RRA_nc_nc()
        {
            var ob = OpcodeByte.New(z: 7, y: 3);
            var cpu = ExecuteTest(ob, (cpuZ80) =>
            {
                cpuZ80.Registers.A = Value1;
                cpuZ80.Registers.Flags.Z = false;
                cpuZ80.Registers.Flags.S = true;
            });

            cpu.AssertRegisters(a: ExpectedValue1R);
            cpu.Registers.Flags.S.Should().BeTrue();
            cpu.Registers.Flags.Z.Should().BeFalse();
            cpu.Registers.Flags.C.Should().BeFalse();
            cpu.Registers.Flags.N.Should().BeFalse();
            cpu.Registers.Flags.H.Should().BeFalse();
        }

        [TestMethod]
        public void RRA_nc_c()
        {
            var ob = OpcodeByte.New(z: 7, y: 3);
            var cpu = ExecuteTest(ob, (cpuZ80) =>
            {
                cpuZ80.Registers.A = Value1;
                cpuZ80.Registers.Flags.Z = false;
                cpuZ80.Registers.Flags.S = true;
                cpuZ80.Registers.Flags.C = true;
            });

            cpu.AssertRegisters(a: ExpectedValue1R | 0x80);
            cpu.Registers.Flags.S.Should().BeTrue();
            cpu.Registers.Flags.Z.Should().BeFalse();
            cpu.Registers.Flags.C.Should().BeFalse();
            cpu.Registers.Flags.N.Should().BeFalse();
            cpu.Registers.Flags.H.Should().BeFalse();
        }

        [TestMethod]
        public void RRA_c_nc()
        {
            var ob = OpcodeByte.New(z: 7, y: 3);
            var cpu = ExecuteTest(ob, (cpuZ80) =>
            {
                cpuZ80.Registers.A = Value2;
                cpuZ80.Registers.Flags.Z = false;
                cpuZ80.Registers.Flags.S = true;
            });

            cpu.AssertRegisters(a: ExpectedValue2R);
            cpu.Registers.Flags.S.Should().BeTrue();
            cpu.Registers.Flags.Z.Should().BeFalse();
            cpu.Registers.Flags.C.Should().BeTrue();
            cpu.Registers.Flags.N.Should().BeFalse();
            cpu.Registers.Flags.H.Should().BeFalse();
        }

        [TestMethod]
        public void RRLA_c_c()
        {
            var ob = OpcodeByte.New(z: 7, y: 3);
            var cpu = ExecuteTest(ob, (cpuZ80) =>
            {
                cpuZ80.Registers.A = Value2;
                cpuZ80.Registers.Flags.Z = false;
                cpuZ80.Registers.Flags.S = true;
                cpuZ80.Registers.Flags.C = true;
            });

            cpu.AssertRegisters(a: ExpectedValue2R | 0x80);
            cpu.Registers.Flags.S.Should().BeTrue();
            cpu.Registers.Flags.Z.Should().BeFalse();
            cpu.Registers.Flags.C.Should().BeTrue();
            cpu.Registers.Flags.N.Should().BeFalse();
            cpu.Registers.Flags.H.Should().BeFalse();
        }

        [TestMethod]
        public void RLC_nc()
        {
            ForEachRegister((reg) =>
            {
                var ob = OpcodeByte.New(z: (byte)reg, y: 0);
                var cpu = ExecuteTest(ob, (cpuZ80) =>
                {
                    cpuZ80.Registers[reg] = Value1;
                }, 0xCB);

                cpu.Registers[reg].Should().Be(ExpectedValue1L);
                cpu.Registers.Flags.S.Should().BeTrue();
                cpu.Registers.Flags.Z.Should().BeFalse();
                cpu.Registers.Flags.C.Should().BeFalse();
                cpu.Registers.Flags.N.Should().BeFalse();
                cpu.Registers.Flags.H.Should().BeFalse();
            });
        }

        [TestMethod]
        public void RLC_c()
        {
            ForEachRegister((reg) =>
            {
                var ob = OpcodeByte.New(z: (byte)reg, y: 0);
                var cpu = ExecuteTest(ob, (cpuZ80) =>
                {
                    cpuZ80.Registers[reg] = Value2;
                }, 0xCB);

                cpu.Registers[reg].Should().Be(ExpectedValue2L | 0x01);
                cpu.Registers.Flags.S.Should().BeFalse();
                cpu.Registers.Flags.Z.Should().BeFalse();
                cpu.Registers.Flags.C.Should().BeTrue();
                cpu.Registers.Flags.N.Should().BeFalse();
                cpu.Registers.Flags.H.Should().BeFalse();
            });
        }

        [TestMethod]
        public void RRC_nc()
        {
            ForEachRegister((reg) =>
            {
                var ob = OpcodeByte.New(z: (byte)reg, y: 1);
                var cpu = ExecuteTest(ob, (cpuZ80) =>
                {
                    cpuZ80.Registers[reg] = Value1;
                }, 0xCB);

                cpu.Registers[reg].Should().Be(ExpectedValue1R);
                cpu.Registers.Flags.S.Should().BeFalse();
                cpu.Registers.Flags.Z.Should().BeFalse();
                cpu.Registers.Flags.C.Should().BeFalse();
                cpu.Registers.Flags.N.Should().BeFalse();
                cpu.Registers.Flags.H.Should().BeFalse();
            });
        }

        [TestMethod]
        public void RRC_c()
        {
            ForEachRegister((reg) =>
            {
                var ob = OpcodeByte.New(z: (byte)reg, y: 1);
                var cpu = ExecuteTest(ob, (cpuZ80) =>
                {
                    cpuZ80.Registers[reg] = Value2;
                }, 0xCB);

                cpu.Registers[reg].Should().Be(ExpectedValue2R | 0x80);
                cpu.Registers.Flags.S.Should().BeTrue();
                cpu.Registers.Flags.Z.Should().BeFalse();
                cpu.Registers.Flags.C.Should().BeTrue();
                cpu.Registers.Flags.N.Should().BeFalse();
                cpu.Registers.Flags.H.Should().BeFalse();
            });
        }

        [TestMethod]
        public void RL_nc_nc()
        {
            ForEachRegister((reg) =>
            {
                var ob = OpcodeByte.New(z: (byte)reg, y: 2);
                var cpu = ExecuteTest(ob, (cpuZ80) =>
                {
                    cpuZ80.Registers[reg] = Value1;
                }, 0xCB);

                cpu.Registers[reg].Should().Be(ExpectedValue1L);
                cpu.Registers.Flags.S.Should().BeTrue();
                cpu.Registers.Flags.Z.Should().BeFalse();
                cpu.Registers.Flags.C.Should().BeFalse();
                cpu.Registers.Flags.N.Should().BeFalse();
                cpu.Registers.Flags.H.Should().BeFalse();
            });
        }

        [TestMethod]
        public void RL_c_nc()
        {
            ForEachRegister((reg) =>
            {
                var ob = OpcodeByte.New(z: (byte)reg, y: 2);
                var cpu = ExecuteTest(ob, (cpuZ80) =>
                {
                    cpuZ80.Registers[reg] = Value2;
                }, 0xCB);

                cpu.Registers[reg].Should().Be(ExpectedValue2L);
                cpu.Registers.Flags.S.Should().BeFalse();
                cpu.Registers.Flags.Z.Should().BeFalse();
                cpu.Registers.Flags.C.Should().BeTrue();
                cpu.Registers.Flags.N.Should().BeFalse();
                cpu.Registers.Flags.H.Should().BeFalse();
            });
        }

        [TestMethod]
        public void RL_nc_c()
        {
            ForEachRegister((reg) =>
            {
                var ob = OpcodeByte.New(z: (byte)reg, y: 2);
                var cpu = ExecuteTest(ob, (cpuZ80) =>
                {
                    cpuZ80.Registers[reg] = Value1;
                    cpuZ80.Registers.Flags.C = true;
                }, 0xCB);

                cpu.Registers[reg].Should().Be(ExpectedValue1L | 0x01);
                cpu.Registers.Flags.S.Should().BeTrue();
                cpu.Registers.Flags.Z.Should().BeFalse();
                cpu.Registers.Flags.C.Should().BeFalse();
                cpu.Registers.Flags.N.Should().BeFalse();
                cpu.Registers.Flags.H.Should().BeFalse();
            });
        }

        [TestMethod]
        public void RL_c_c()
        {
            ForEachRegister((reg) =>
            {
                var ob = OpcodeByte.New(z: (byte)reg, y: 2);
                var cpu = ExecuteTest(ob, (cpuZ80) =>
                {
                    cpuZ80.Registers[reg] = Value2;
                    cpuZ80.Registers.Flags.C = true;
                }, 0xCB);

                cpu.Registers[reg].Should().Be(ExpectedValue2L | 0x01);
                cpu.Registers.Flags.S.Should().BeFalse();
                cpu.Registers.Flags.Z.Should().BeFalse();
                cpu.Registers.Flags.C.Should().BeTrue();
                cpu.Registers.Flags.N.Should().BeFalse();
                cpu.Registers.Flags.H.Should().BeFalse();
            });
        }

        [TestMethod]
        public void RR_nc_nc()
        {
            ForEachRegister((reg) =>
            {
                var ob = OpcodeByte.New(z: (byte)reg, y: 3);
                var cpu = ExecuteTest(ob, (cpuZ80) =>
                {
                    cpuZ80.Registers[reg] = Value1;
                }, 0xCB);

                cpu.Registers[reg].Should().Be(ExpectedValue1R);
                cpu.Registers.Flags.S.Should().BeFalse();
                cpu.Registers.Flags.Z.Should().BeFalse();
                cpu.Registers.Flags.C.Should().BeFalse();
                cpu.Registers.Flags.N.Should().BeFalse();
                cpu.Registers.Flags.H.Should().BeFalse();
            });
        }

        [TestMethod]
        public void RR_c_nc()
        {
            ForEachRegister((reg) =>
            {
                var ob = OpcodeByte.New(z: (byte)reg, y: 3);
                var cpu = ExecuteTest(ob, (cpuZ80) =>
                {
                    cpuZ80.Registers[reg] = Value2;
                }, 0xCB);

                cpu.Registers[reg].Should().Be(ExpectedValue2R);
                cpu.Registers.Flags.S.Should().BeFalse();
                cpu.Registers.Flags.Z.Should().BeFalse();
                cpu.Registers.Flags.C.Should().BeTrue();
                cpu.Registers.Flags.N.Should().BeFalse();
                cpu.Registers.Flags.H.Should().BeFalse();
            });
        }

        [TestMethod]
        public void RR_nc_c()
        {
            ForEachRegister((reg) =>
            {
                var ob = OpcodeByte.New(z: (byte)reg, y: 3);
                var cpu = ExecuteTest(ob, (cpuZ80) =>
                {
                    cpuZ80.Registers[reg] = Value1;
                    cpuZ80.Registers.Flags.C = true;
                }, 0xCB);

                cpu.Registers[reg].Should().Be(ExpectedValue1R | 0x80);
                cpu.Registers.Flags.S.Should().BeTrue();
                cpu.Registers.Flags.Z.Should().BeFalse();
                cpu.Registers.Flags.C.Should().BeFalse();
                cpu.Registers.Flags.N.Should().BeFalse();
                cpu.Registers.Flags.H.Should().BeFalse();
            });
        }

        [TestMethod]
        public void RR_c_c()
        {
            ForEachRegister((reg) =>
            {
                var ob = OpcodeByte.New(z: (byte)reg, y: 3);
                var cpu = ExecuteTest(ob, (cpuZ80) =>
                {
                    cpuZ80.Registers[reg] = Value2;
                    cpuZ80.Registers.Flags.C = true;
                }, 0xCB);

                cpu.Registers[reg].Should().Be(ExpectedValue2R | 0x80);
                cpu.Registers.Flags.S.Should().BeTrue();
                cpu.Registers.Flags.Z.Should().BeFalse();
                cpu.Registers.Flags.C.Should().BeTrue();
                cpu.Registers.Flags.N.Should().BeFalse();
                cpu.Registers.Flags.H.Should().BeFalse();
            });
        }

        [TestMethod]
        public void SLA_nc()
        {
            ForEachRegister((reg) =>
            {
                var ob = OpcodeByte.New(z: (byte)reg, y: 4);
                var cpu = ExecuteTest(ob, (cpuZ80) =>
                {
                    cpuZ80.Registers[reg] = Value1;
                }, 0xCB);

                cpu.Registers[reg].Should().Be(ExpectedValue1L);
                cpu.Registers.Flags.S.Should().BeTrue();
                cpu.Registers.Flags.Z.Should().BeFalse();
                cpu.Registers.Flags.C.Should().BeFalse();
                cpu.Registers.Flags.N.Should().BeFalse();
                cpu.Registers.Flags.H.Should().BeFalse();
            });
        }

        [TestMethod]
        public void SLA_c()
        {
            ForEachRegister((reg) =>
            {
                var ob = OpcodeByte.New(z: (byte)reg, y: 4);
                var cpu = ExecuteTest(ob, (cpuZ80) =>
                {
                    cpuZ80.Registers[reg] = Value2;
                }, 0xCB);

                cpu.Registers[reg].Should().Be(ExpectedValue2L);
                cpu.Registers.Flags.S.Should().BeFalse();
                cpu.Registers.Flags.Z.Should().BeFalse();
                cpu.Registers.Flags.C.Should().BeTrue();
                cpu.Registers.Flags.N.Should().BeFalse();
                cpu.Registers.Flags.H.Should().BeFalse();
            });
        }

        [TestMethod]
        public void SRA_nc()
        {
            ForEachRegister((reg) =>
            {
                var ob = OpcodeByte.New(z: (byte)reg, y: 5);
                var cpu = ExecuteTest(ob, (cpuZ80) =>
                {
                    cpuZ80.Registers[reg] = Value1;
                }, 0xCB);

                cpu.Registers[reg].Should().Be(ExpectedValue1R);
                cpu.Registers.Flags.S.Should().BeFalse();
                cpu.Registers.Flags.Z.Should().BeFalse();
                cpu.Registers.Flags.C.Should().BeFalse();
                cpu.Registers.Flags.N.Should().BeFalse();
                cpu.Registers.Flags.H.Should().BeFalse();
            });
        }

        [TestMethod]
        public void SRA_c()
        {
            ForEachRegister((reg) =>
            {
                var ob = OpcodeByte.New(z: (byte)reg, y: 5);
                var cpu = ExecuteTest(ob, (cpuZ80) =>
                {
                    cpuZ80.Registers[reg] = Value2;
                }, 0xCB);

                cpu.Registers[reg].Should().Be(ExpectedValue2R | 0x80);
                cpu.Registers.Flags.S.Should().BeTrue();
                cpu.Registers.Flags.Z.Should().BeFalse();
                cpu.Registers.Flags.C.Should().BeTrue();
                cpu.Registers.Flags.N.Should().BeFalse();
                cpu.Registers.Flags.H.Should().BeFalse();
            });
        }

        [TestMethod]
        public void SLL_nc()
        {
            ForEachRegister((reg) =>
            {
                var ob = OpcodeByte.New(z: (byte)reg, y: 6);
                var cpu = ExecuteTest(ob, (cpuZ80) =>
                {
                    cpuZ80.Registers[reg] = Value1;
                }, 0xCB);

                cpu.Registers[reg].Should().Be(ExpectedValue1L | 0x01);
                cpu.Registers.Flags.S.Should().BeTrue();
                cpu.Registers.Flags.Z.Should().BeFalse();
                cpu.Registers.Flags.C.Should().BeFalse();
                cpu.Registers.Flags.N.Should().BeFalse();
                cpu.Registers.Flags.H.Should().BeFalse();
            });
        }

        [TestMethod]
        public void SLL_c()
        {
            ForEachRegister((reg) =>
            {
                var ob = OpcodeByte.New(z: (byte)reg, y: 6);
                var cpu = ExecuteTest(ob, (cpuZ80) =>
                {
                    cpuZ80.Registers[reg] = Value2;
                }, 0xCB);

                cpu.Registers[reg].Should().Be(ExpectedValue2L | 0x01);
                cpu.Registers.Flags.S.Should().BeFalse();
                cpu.Registers.Flags.Z.Should().BeFalse();
                cpu.Registers.Flags.C.Should().BeTrue();
                cpu.Registers.Flags.N.Should().BeFalse();
                cpu.Registers.Flags.H.Should().BeFalse();
            });
        }

        [TestMethod]
        public void SRL_nc()
        {
            ForEachRegister((reg) =>
            {
                var ob = OpcodeByte.New(z: (byte)reg, y: 7);
                var cpu = ExecuteTest(ob, (cpuZ80) =>
                {
                    cpuZ80.Registers[reg] = Value1;
                }, 0xCB);

                cpu.Registers[reg].Should().Be(ExpectedValue1R);
                cpu.Registers.Flags.S.Should().BeFalse();
                cpu.Registers.Flags.Z.Should().BeFalse();
                cpu.Registers.Flags.C.Should().BeFalse();
                cpu.Registers.Flags.N.Should().BeFalse();
                cpu.Registers.Flags.H.Should().BeFalse();
            });
        }

        [TestMethod]
        public void SRL_c()
        {
            ForEachRegister((reg) =>
            {
                var ob = OpcodeByte.New(z: (byte)reg, y: 7);
                var cpu = ExecuteTest(ob, (cpuZ80) =>
                {
                    cpuZ80.Registers[reg] = Value2;
                }, 0xCB);

                cpu.Registers[reg].Should().Be(ExpectedValue2R);
                cpu.Registers.Flags.S.Should().BeFalse();
                cpu.Registers.Flags.Z.Should().BeFalse();
                cpu.Registers.Flags.C.Should().BeTrue();
                cpu.Registers.Flags.N.Should().BeFalse();
                cpu.Registers.Flags.H.Should().BeFalse();
            });
        }

        private static void ForEachRegister(Action<Register8Table> action)
        {
            for (Register8Table reg = Register8Table.B; reg <= Register8Table.A; reg++)
            {
                if (reg == Register8Table.HL) continue;
                action(reg);
            }
        }

        private static CpuZ80 ExecuteTest(OpcodeByte ob, Action<CpuZ80> preTest, byte extension = 0)
        {
            var cpuZ80 = new CpuZ80();
            byte[] buffer = (extension == 0) ?
                buffer = new byte[] { ob.Value } :
                new byte[] { extension, ob.Value };

            var model = cpuZ80.Initialize(buffer);

            cpuZ80.FillRegisters();
            preTest(cpuZ80);

            model.ClockGen.SquareWave(extension == 0 ? 4 : 8);

            Console.WriteLine(model.LogicAnalyzer.ToWaveJson());

            return cpuZ80;
        }
    }
}
