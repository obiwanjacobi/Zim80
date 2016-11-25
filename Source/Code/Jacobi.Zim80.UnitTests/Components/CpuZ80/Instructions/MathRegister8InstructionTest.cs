using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using Jacobi.Zim80.Model;
using Jacobi.Zim80.Components.CpuZ80.UnitTests;
using FluentAssertions;

namespace Jacobi.Zim80.Components.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class MathRegister8InstructionTest
    {
        private const byte Value = 0x55;

        [TestMethod]
        public void Add_reg()
        {
            var expectedValue = (byte)(CpuZ80TestExtensions.MagicValue + Value);
            var expectedValueA = (byte)(Value + Value);

            TestMathOperation(MathOperations.Add, expectedValue, expectedValueA,
                (flags, reg) => 
                {
                    flags.S.Should().Be(reg == Register8Table.A);
                    flags.Z.Should().BeFalse();
                    flags.C.Should().BeFalse();
                    flags.H.Should().BeTrue();
                    flags.N.Should().BeFalse();
                    flags.PV.Should().BeFalse();
                }, carry: false);
        }

        [TestMethod]
        public void Adc_reg_nc()
        {
            var expectedValue = (byte)(CpuZ80TestExtensions.MagicValue + Value);
            var expectedValueA = (byte)(Value + Value);

            TestMathOperation(MathOperations.AddWithCarry, expectedValue, expectedValueA,
                (flags, reg) =>
                {
                    flags.S.Should().Be(reg == Register8Table.A);
                    flags.Z.Should().BeFalse();
                    flags.C.Should().BeFalse();
                    flags.H.Should().BeTrue();
                    flags.N.Should().BeFalse();
                    flags.PV.Should().BeFalse();
                }, carry: false);
        }

        [TestMethod]
        public void Adc_reg_c()
        {
            var expectedValue = (byte)(CpuZ80TestExtensions.MagicValue + Value + 1);
            var expectedValueA = (byte)(Value + Value + 1);

            TestMathOperation(MathOperations.AddWithCarry, expectedValue, expectedValueA,
                (flags, reg) =>
                {
                    flags.S.Should().BeTrue();
                    flags.Z.Should().BeFalse();
                    flags.C.Should().BeFalse();
                    flags.H.Should().BeTrue();
                    flags.N.Should().BeFalse();
                    flags.PV.Should().BeFalse();
                }, carry: true);
        }

        [TestMethod]
        public void Sub_reg()
        {
            var expectedValue = unchecked ((byte)(CpuZ80TestExtensions.MagicValue - Value));
            var expectedValueA = (byte)0;

            TestMathOperation(MathOperations.Subtract, expectedValue, expectedValueA,
                (flags, reg) => 
                {
                    bool isA = reg == Register8Table.A;

                    flags.S.Should().Be(!isA);
                    flags.Z.Should().Be(isA);
                    flags.C.Should().Be(!isA);
                    flags.H.Should().Be(isA);
                    flags.N.Should().BeTrue();
                    flags.PV.Should().Be(!isA);

                }, carry: false);
        }

        [TestMethod]
        public void Sbc_reg_nc()
        {
            var expectedValue = unchecked((byte)(CpuZ80TestExtensions.MagicValue - Value));
            var expectedValueA = (byte)0;

            TestMathOperation(MathOperations.SubtractWithCarry, expectedValue, expectedValueA,
                (flags, reg) =>
                {
                    bool isA = reg == Register8Table.A;

                    flags.S.Should().Be(!isA);
                    flags.Z.Should().Be(isA);
                    flags.C.Should().Be(!isA);
                    flags.H.Should().Be(isA);
                    flags.N.Should().BeTrue();
                    flags.PV.Should().Be(!isA);

                }, carry: false);
        }

        [TestMethod]
        public void Sbc_reg_c()
        {
            var expectedValue = unchecked((byte)(CpuZ80TestExtensions.MagicValue - Value - 1));
            var expectedValueA = (byte)0xFF;

            TestMathOperation(MathOperations.SubtractWithCarry, expectedValue, expectedValueA,
                (flags, reg) =>
                {
                    flags.S.Should().BeTrue();
                    flags.Z.Should().BeFalse();
                    flags.C.Should().BeTrue();
                    flags.H.Should().BeFalse();
                    flags.N.Should().BeTrue();
                    flags.PV.Should().BeTrue();

                }, carry: true);
        }

        [TestMethod]
        public void And_reg()
        {
            var expectedValue = (byte)(CpuZ80TestExtensions.MagicValue & Value);
            var expectedValueA = (byte)(Value & Value);

            TestMathOperation(MathOperations.And, expectedValue, expectedValueA,
                (flags, reg) =>
                {
                    flags.S.Should().BeFalse();
                    flags.Z.Should().Be(reg != Register8Table.A);
                    flags.C.Should().BeFalse();
                    flags.H.Should().BeTrue();
                    flags.N.Should().BeFalse();
                    flags.PV.Should().BeFalse();

                }, carry: false);
        }

        [TestMethod]
        public void Xor_reg()
        {
            var expectedValue = (byte)(CpuZ80TestExtensions.MagicValue ^ Value);
            var expectedValueA = (byte)(Value ^ Value);

            TestMathOperation(MathOperations.ExlusiveOr, expectedValue, expectedValueA,
                (flags, reg) =>
                {
                    var isA = reg == Register8Table.A;
                    flags.S.Should().BeFalse();
                    flags.Z.Should().Be(isA);
                    flags.C.Should().BeFalse();
                    flags.H.Should().BeFalse();
                    flags.N.Should().BeFalse();
                    flags.PV.Should().Be(isA);

                }, carry: false);
        }

        [TestMethod]
        public void Or_reg()
        {
            var expectedValue = (byte)(CpuZ80TestExtensions.MagicValue | Value);
            var expectedValueA = (byte)(Value | Value);

            TestMathOperation(MathOperations.Or, expectedValue, expectedValueA,
                (flags, reg) =>
                {
                    flags.S.Should().BeFalse();
                    flags.Z.Should().BeFalse();
                    flags.C.Should().BeFalse();
                    flags.H.Should().BeFalse();
                    flags.N.Should().BeFalse();
                    flags.PV.Should().BeFalse();

                }, carry: false);
        }

        [TestMethod]
        public void Cp_reg()
        {
            var expectedValue = CpuZ80TestExtensions.MagicValue;
            var expectedValueA = Value;

            TestMathOperation(MathOperations.Compare, expectedValue, expectedValueA,
                (flags, reg) =>
                {
                    bool isA = reg == Register8Table.A;

                    flags.S.Should().Be(!isA);
                    flags.Z.Should().Be(isA);
                    flags.C.Should().Be(!isA);
                    flags.H.Should().Be(isA);
                    flags.N.Should().BeTrue();
                    flags.PV.Should().Be(!isA);

                }, carry: false);
        }

        private void TestMathOperation(MathOperations mathOp, byte expectedValue, 
            byte expectedValueA, Action<RegisterFlags, Register8Table> assertFlags, bool carry)
        {
            for (Register8Table reg = Register8Table.B; reg <= Register8Table.A; reg++)
            {
                if (reg == Register8Table.HL) continue;

                var cpu = ExecuteTest(mathOp, reg,
                    (m) =>
                    {
                        m.Cpu.Registers.Flags.C = carry;
                        m.Cpu.Registers[reg] = Value;
                    });

                if (reg == Register8Table.A)
                {
                    cpu.Registers.A.Should().Be(expectedValueA);
                }
                else
                {
                    cpu.Registers.A.Should().Be(expectedValue);
                    cpu.Registers[reg].Should().Be(Value);
                }

                assertFlags(cpu.Registers.Flags, reg);
            }
        }

        private static CpuZ80 ExecuteTest(MathOperations mathOp, Register8Table register, Action<SimulationModel> fnPreTest)
        {
            var ob = OpcodeByte.New(x: 2, z: (byte)register, y: (byte)mathOp);
            return ExecuteTest(ob, fnPreTest);
        }

        private static CpuZ80 ExecuteTest(OpcodeByte ob, Action<SimulationModel> fnPreTest)
        {
            var cpuZ80 = new CpuZ80();
            byte[] buffer = new byte[] { ob.Value };
            var model = cpuZ80.Initialize(buffer);

            cpuZ80.FillRegisters();
            fnPreTest(model);

            model.ClockGen.SquareWave(4);

            return cpuZ80;
        }
    }
}
