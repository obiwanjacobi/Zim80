using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using Jacobi.Zim80.Components.CpuZ80.UnitTests;
using FluentAssertions;

namespace Jacobi.Zim80.Components.CpuZ80.Instructions.UnitTests
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
                    cpuZ80.Registers.PrimarySet.A = Value1;
                    cpuZ80.Registers.PrimarySet.Flags.Z = true;
                    cpuZ80.Registers.PrimarySet.Flags.S = true;
                });

            cpu.AssertRegisters(a: ExpectedValue1L);
            cpu.Registers.PrimarySet.Flags.S.Should().BeTrue();
            cpu.Registers.PrimarySet.Flags.Z.Should().BeTrue();
            cpu.Registers.PrimarySet.Flags.C.Should().BeFalse();
            cpu.Registers.PrimarySet.Flags.N.Should().BeFalse();
            cpu.Registers.PrimarySet.Flags.H.Should().BeFalse();
        }

        [TestMethod]
        public void RLCA_c()
        {
            var ob = OpcodeByte.New(z: 7, y: 0);
            var cpu = ExecuteTest(ob, (cpuZ80) =>
            {
                cpuZ80.Registers.PrimarySet.A = Value2;
                cpuZ80.Registers.PrimarySet.Flags.Z = false;
                cpuZ80.Registers.PrimarySet.Flags.S = true;
            });

            cpu.AssertRegisters(a: ExpectedValue2L | 0x01);
            cpu.Registers.PrimarySet.Flags.S.Should().BeTrue();
            cpu.Registers.PrimarySet.Flags.Z.Should().BeFalse();
            cpu.Registers.PrimarySet.Flags.C.Should().BeTrue();
            cpu.Registers.PrimarySet.Flags.N.Should().BeFalse();
            cpu.Registers.PrimarySet.Flags.H.Should().BeFalse();
        }

        [TestMethod]
        public void RRCA_nc()
        {
            var ob = OpcodeByte.New(z: 7, y: 1);
            var cpu = ExecuteTest(ob, (cpuZ80) =>
            {
                cpuZ80.Registers.PrimarySet.A = Value1;
                cpuZ80.Registers.PrimarySet.Flags.Z = true;
                cpuZ80.Registers.PrimarySet.Flags.S = false;
            });

            cpu.AssertRegisters(a: ExpectedValue1R);
            cpu.Registers.PrimarySet.Flags.S.Should().BeFalse();
            cpu.Registers.PrimarySet.Flags.Z.Should().BeTrue();
            cpu.Registers.PrimarySet.Flags.C.Should().BeFalse();
            cpu.Registers.PrimarySet.Flags.N.Should().BeFalse();
            cpu.Registers.PrimarySet.Flags.H.Should().BeFalse();
        }

        [TestMethod]
        public void RRCA_c()
        {
            var ob = OpcodeByte.New(z: 7, y: 1);
            var cpu = ExecuteTest(ob, (cpuZ80) =>
            {
                cpuZ80.Registers.PrimarySet.A = Value2;
                cpuZ80.Registers.PrimarySet.Flags.Z = true;
                cpuZ80.Registers.PrimarySet.Flags.S = false;
            });

            cpu.AssertRegisters(a: ExpectedValue2R | 0x80);
            cpu.Registers.PrimarySet.Flags.S.Should().BeFalse();
            cpu.Registers.PrimarySet.Flags.Z.Should().BeTrue();
            cpu.Registers.PrimarySet.Flags.C.Should().BeTrue();
            cpu.Registers.PrimarySet.Flags.N.Should().BeFalse();
            cpu.Registers.PrimarySet.Flags.H.Should().BeFalse();
        }

        [TestMethod]
        public void RLA_nc_nc()
        {
            var ob = OpcodeByte.New(z: 7, y: 2);
            var cpu = ExecuteTest(ob, (cpuZ80) =>
            {
                cpuZ80.Registers.PrimarySet.A = Value1;
                cpuZ80.Registers.PrimarySet.Flags.Z = false;
                cpuZ80.Registers.PrimarySet.Flags.S = true;
            });

            cpu.AssertRegisters(a: ExpectedValue1L);
            cpu.Registers.PrimarySet.Flags.S.Should().BeTrue();
            cpu.Registers.PrimarySet.Flags.Z.Should().BeFalse();
            cpu.Registers.PrimarySet.Flags.C.Should().BeFalse();
            cpu.Registers.PrimarySet.Flags.N.Should().BeFalse();
            cpu.Registers.PrimarySet.Flags.H.Should().BeFalse();
        }

        [TestMethod]
        public void RLA_nc_c()
        {
            var ob = OpcodeByte.New(z: 7, y: 2);
            var cpu = ExecuteTest(ob, (cpuZ80) =>
            {
                cpuZ80.Registers.PrimarySet.A = Value1;
                cpuZ80.Registers.PrimarySet.Flags.Z = false;
                cpuZ80.Registers.PrimarySet.Flags.S = true;
                cpuZ80.Registers.PrimarySet.Flags.C = true;
            });

            cpu.AssertRegisters(a: ExpectedValue1L | 0x01);
            cpu.Registers.PrimarySet.Flags.S.Should().BeTrue();
            cpu.Registers.PrimarySet.Flags.Z.Should().BeFalse();
            cpu.Registers.PrimarySet.Flags.C.Should().BeFalse();
            cpu.Registers.PrimarySet.Flags.N.Should().BeFalse();
            cpu.Registers.PrimarySet.Flags.H.Should().BeFalse();
        }

        [TestMethod]
        public void RLA_c_nc()
        {
            var ob = OpcodeByte.New(z: 7, y: 2);
            var cpu = ExecuteTest(ob, (cpuZ80) =>
            {
                cpuZ80.Registers.PrimarySet.A = Value2;
                cpuZ80.Registers.PrimarySet.Flags.Z = false;
                cpuZ80.Registers.PrimarySet.Flags.S = true;
            });

            cpu.AssertRegisters(a: ExpectedValue2L);
            cpu.Registers.PrimarySet.Flags.S.Should().BeTrue();
            cpu.Registers.PrimarySet.Flags.Z.Should().BeFalse();
            cpu.Registers.PrimarySet.Flags.C.Should().BeTrue();
            cpu.Registers.PrimarySet.Flags.N.Should().BeFalse();
            cpu.Registers.PrimarySet.Flags.H.Should().BeFalse();
        }

        [TestMethod]
        public void RLA_c_c()
        {
            var ob = OpcodeByte.New(z: 7, y: 2);
            var cpu = ExecuteTest(ob, (cpuZ80) =>
            {
                cpuZ80.Registers.PrimarySet.A = Value2;
                cpuZ80.Registers.PrimarySet.Flags.Z = false;
                cpuZ80.Registers.PrimarySet.Flags.S = true;
                cpuZ80.Registers.PrimarySet.Flags.C = true;
            });

            cpu.AssertRegisters(a: ExpectedValue2L | 0x01);
            cpu.Registers.PrimarySet.Flags.S.Should().BeTrue();
            cpu.Registers.PrimarySet.Flags.Z.Should().BeFalse();
            cpu.Registers.PrimarySet.Flags.C.Should().BeTrue();
            cpu.Registers.PrimarySet.Flags.N.Should().BeFalse();
            cpu.Registers.PrimarySet.Flags.H.Should().BeFalse();
        }

        [TestMethod]
        public void RRA_nc_nc()
        {
            var ob = OpcodeByte.New(z: 7, y: 3);
            var cpu = ExecuteTest(ob, (cpuZ80) =>
            {
                cpuZ80.Registers.PrimarySet.A = Value1;
                cpuZ80.Registers.PrimarySet.Flags.Z = false;
                cpuZ80.Registers.PrimarySet.Flags.S = true;
            });

            cpu.AssertRegisters(a: ExpectedValue1R);
            cpu.Registers.PrimarySet.Flags.S.Should().BeTrue();
            cpu.Registers.PrimarySet.Flags.Z.Should().BeFalse();
            cpu.Registers.PrimarySet.Flags.C.Should().BeFalse();
            cpu.Registers.PrimarySet.Flags.N.Should().BeFalse();
            cpu.Registers.PrimarySet.Flags.H.Should().BeFalse();
        }

        [TestMethod]
        public void RRA_nc_c()
        {
            var ob = OpcodeByte.New(z: 7, y: 3);
            var cpu = ExecuteTest(ob, (cpuZ80) =>
            {
                cpuZ80.Registers.PrimarySet.A = Value1;
                cpuZ80.Registers.PrimarySet.Flags.Z = false;
                cpuZ80.Registers.PrimarySet.Flags.S = true;
                cpuZ80.Registers.PrimarySet.Flags.C = true;
            });

            cpu.AssertRegisters(a: ExpectedValue1R | 0x80);
            cpu.Registers.PrimarySet.Flags.S.Should().BeTrue();
            cpu.Registers.PrimarySet.Flags.Z.Should().BeFalse();
            cpu.Registers.PrimarySet.Flags.C.Should().BeFalse();
            cpu.Registers.PrimarySet.Flags.N.Should().BeFalse();
            cpu.Registers.PrimarySet.Flags.H.Should().BeFalse();
        }

        [TestMethod]
        public void RRA_c_nc()
        {
            var ob = OpcodeByte.New(z: 7, y: 3);
            var cpu = ExecuteTest(ob, (cpuZ80) =>
            {
                cpuZ80.Registers.PrimarySet.A = Value2;
                cpuZ80.Registers.PrimarySet.Flags.Z = false;
                cpuZ80.Registers.PrimarySet.Flags.S = true;
            });

            cpu.AssertRegisters(a: ExpectedValue2R);
            cpu.Registers.PrimarySet.Flags.S.Should().BeTrue();
            cpu.Registers.PrimarySet.Flags.Z.Should().BeFalse();
            cpu.Registers.PrimarySet.Flags.C.Should().BeTrue();
            cpu.Registers.PrimarySet.Flags.N.Should().BeFalse();
            cpu.Registers.PrimarySet.Flags.H.Should().BeFalse();
        }

        [TestMethod]
        public void RRLA_c_c()
        {
            var ob = OpcodeByte.New(z: 7, y: 3);
            var cpu = ExecuteTest(ob, (cpuZ80) =>
            {
                cpuZ80.Registers.PrimarySet.A = Value2;
                cpuZ80.Registers.PrimarySet.Flags.Z = false;
                cpuZ80.Registers.PrimarySet.Flags.S = true;
                cpuZ80.Registers.PrimarySet.Flags.C = true;
            });

            cpu.AssertRegisters(a: ExpectedValue2R | 0x80);
            cpu.Registers.PrimarySet.Flags.S.Should().BeTrue();
            cpu.Registers.PrimarySet.Flags.Z.Should().BeFalse();
            cpu.Registers.PrimarySet.Flags.C.Should().BeTrue();
            cpu.Registers.PrimarySet.Flags.N.Should().BeFalse();
            cpu.Registers.PrimarySet.Flags.H.Should().BeFalse();
        }

        [TestMethod]
        public void RLC_nc()
        {
            ForEachRegister((reg) =>
            {
                var ob = OpcodeByte.New(z: (byte)reg, y: 0);
                var cpu = ExecuteTest(ob, (cpuZ80) =>
                {
                    cpuZ80.Registers.PrimarySet[reg] = Value1;
                }, 0xCB);

                cpu.Registers.PrimarySet[reg].Should().Be(ExpectedValue1L);
                cpu.Registers.PrimarySet.Flags.S.Should().BeTrue();
                cpu.Registers.PrimarySet.Flags.Z.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.C.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.N.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.H.Should().BeFalse();
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
                    cpuZ80.Registers.PrimarySet[reg] = Value2;
                }, 0xCB);

                cpu.Registers.PrimarySet[reg].Should().Be(ExpectedValue2L | 0x01);
                cpu.Registers.PrimarySet.Flags.S.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.Z.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.C.Should().BeTrue();
                cpu.Registers.PrimarySet.Flags.N.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.H.Should().BeFalse();
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
                    cpuZ80.Registers.PrimarySet[reg] = Value1;
                }, 0xCB);

                cpu.Registers.PrimarySet[reg].Should().Be(ExpectedValue1R);
                cpu.Registers.PrimarySet.Flags.S.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.Z.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.C.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.N.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.H.Should().BeFalse();
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
                    cpuZ80.Registers.PrimarySet[reg] = Value2;
                }, 0xCB);

                cpu.Registers.PrimarySet[reg].Should().Be(ExpectedValue2R | 0x80);
                cpu.Registers.PrimarySet.Flags.S.Should().BeTrue();
                cpu.Registers.PrimarySet.Flags.Z.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.C.Should().BeTrue();
                cpu.Registers.PrimarySet.Flags.N.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.H.Should().BeFalse();
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
                    cpuZ80.Registers.PrimarySet[reg] = Value1;
                }, 0xCB);

                cpu.Registers.PrimarySet[reg].Should().Be(ExpectedValue1L);
                cpu.Registers.PrimarySet.Flags.S.Should().BeTrue();
                cpu.Registers.PrimarySet.Flags.Z.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.C.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.N.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.H.Should().BeFalse();
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
                    cpuZ80.Registers.PrimarySet[reg] = Value2;
                }, 0xCB);

                cpu.Registers.PrimarySet[reg].Should().Be(ExpectedValue2L);
                cpu.Registers.PrimarySet.Flags.S.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.Z.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.C.Should().BeTrue();
                cpu.Registers.PrimarySet.Flags.N.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.H.Should().BeFalse();
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
                    cpuZ80.Registers.PrimarySet[reg] = Value1;
                    cpuZ80.Registers.PrimarySet.Flags.C = true;
                }, 0xCB);

                cpu.Registers.PrimarySet[reg].Should().Be(ExpectedValue1L | 0x01);
                cpu.Registers.PrimarySet.Flags.S.Should().BeTrue();
                cpu.Registers.PrimarySet.Flags.Z.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.C.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.N.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.H.Should().BeFalse();
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
                    cpuZ80.Registers.PrimarySet[reg] = Value2;
                    cpuZ80.Registers.PrimarySet.Flags.C = true;
                }, 0xCB);

                cpu.Registers.PrimarySet[reg].Should().Be(ExpectedValue2L | 0x01);
                cpu.Registers.PrimarySet.Flags.S.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.Z.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.C.Should().BeTrue();
                cpu.Registers.PrimarySet.Flags.N.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.H.Should().BeFalse();
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
                    cpuZ80.Registers.PrimarySet[reg] = Value1;
                }, 0xCB);

                cpu.Registers.PrimarySet[reg].Should().Be(ExpectedValue1R);
                cpu.Registers.PrimarySet.Flags.S.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.Z.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.C.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.N.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.H.Should().BeFalse();
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
                    cpuZ80.Registers.PrimarySet[reg] = Value2;
                }, 0xCB);

                cpu.Registers.PrimarySet[reg].Should().Be(ExpectedValue2R);
                cpu.Registers.PrimarySet.Flags.S.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.Z.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.C.Should().BeTrue();
                cpu.Registers.PrimarySet.Flags.N.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.H.Should().BeFalse();
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
                    cpuZ80.Registers.PrimarySet[reg] = Value1;
                    cpuZ80.Registers.PrimarySet.Flags.C = true;
                }, 0xCB);

                cpu.Registers.PrimarySet[reg].Should().Be(ExpectedValue1R | 0x80);
                cpu.Registers.PrimarySet.Flags.S.Should().BeTrue();
                cpu.Registers.PrimarySet.Flags.Z.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.C.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.N.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.H.Should().BeFalse();
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
                    cpuZ80.Registers.PrimarySet[reg] = Value2;
                    cpuZ80.Registers.PrimarySet.Flags.C = true;
                }, 0xCB);

                cpu.Registers.PrimarySet[reg].Should().Be(ExpectedValue2R | 0x80);
                cpu.Registers.PrimarySet.Flags.S.Should().BeTrue();
                cpu.Registers.PrimarySet.Flags.Z.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.C.Should().BeTrue();
                cpu.Registers.PrimarySet.Flags.N.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.H.Should().BeFalse();
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
                    cpuZ80.Registers.PrimarySet[reg] = Value1;
                }, 0xCB);

                cpu.Registers.PrimarySet[reg].Should().Be(ExpectedValue1L);
                cpu.Registers.PrimarySet.Flags.S.Should().BeTrue();
                cpu.Registers.PrimarySet.Flags.Z.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.C.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.N.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.H.Should().BeFalse();
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
                    cpuZ80.Registers.PrimarySet[reg] = Value2;
                }, 0xCB);

                cpu.Registers.PrimarySet[reg].Should().Be(ExpectedValue2L);
                cpu.Registers.PrimarySet.Flags.S.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.Z.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.C.Should().BeTrue();
                cpu.Registers.PrimarySet.Flags.N.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.H.Should().BeFalse();
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
                    cpuZ80.Registers.PrimarySet[reg] = Value1;
                }, 0xCB);

                cpu.Registers.PrimarySet[reg].Should().Be(ExpectedValue1R);
                cpu.Registers.PrimarySet.Flags.S.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.Z.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.C.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.N.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.H.Should().BeFalse();
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
                    cpuZ80.Registers.PrimarySet[reg] = Value2;
                }, 0xCB);

                cpu.Registers.PrimarySet[reg].Should().Be(ExpectedValue2R | 0x80);
                cpu.Registers.PrimarySet.Flags.S.Should().BeTrue();
                cpu.Registers.PrimarySet.Flags.Z.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.C.Should().BeTrue();
                cpu.Registers.PrimarySet.Flags.N.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.H.Should().BeFalse();
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
                    cpuZ80.Registers.PrimarySet[reg] = Value1;
                }, 0xCB);

                cpu.Registers.PrimarySet[reg].Should().Be(ExpectedValue1L | 0x01);
                cpu.Registers.PrimarySet.Flags.S.Should().BeTrue();
                cpu.Registers.PrimarySet.Flags.Z.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.C.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.N.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.H.Should().BeFalse();
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
                    cpuZ80.Registers.PrimarySet[reg] = Value2;
                }, 0xCB);

                cpu.Registers.PrimarySet[reg].Should().Be(ExpectedValue2L | 0x01);
                cpu.Registers.PrimarySet.Flags.S.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.Z.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.C.Should().BeTrue();
                cpu.Registers.PrimarySet.Flags.N.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.H.Should().BeFalse();
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
                    cpuZ80.Registers.PrimarySet[reg] = Value1;
                }, 0xCB);

                cpu.Registers.PrimarySet[reg].Should().Be(ExpectedValue1R);
                cpu.Registers.PrimarySet.Flags.S.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.Z.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.C.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.N.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.H.Should().BeFalse();
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
                    cpuZ80.Registers.PrimarySet[reg] = Value2;
                }, 0xCB);

                cpu.Registers.PrimarySet[reg].Should().Be(ExpectedValue2R);
                cpu.Registers.PrimarySet.Flags.S.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.Z.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.C.Should().BeTrue();
                cpu.Registers.PrimarySet.Flags.N.Should().BeFalse();
                cpu.Registers.PrimarySet.Flags.H.Should().BeFalse();
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
            byte[] buffer;

            if (extension == 0)
                buffer = new byte[] { ob.Value };
            else
                buffer = new byte[] { extension, ob.Value };

            var model = cpuZ80.Initialize(buffer);

            cpuZ80.FillRegisters();
            preTest(cpuZ80);

            model.ClockGen.BlockWave(extension == 0 ? 4 : 8);

            return cpuZ80;
        }
    }
}
