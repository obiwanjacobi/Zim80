using FluentAssertions;
using Jacobi.Zim80.CpuZ80.Opcodes;
using Jacobi.Zim80.CpuZ80.UnitTests;
using Jacobi.Zim80.Diagnostics;
using Jacobi.Zim80.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Jacobi.Zim80.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class MathDirectInstructionTest
    {
        private const byte Value1 = 0x55;
        private const byte Value2 = 0xEA;

        [TestMethod]
        public void ADDn_nc_nc()
        {
            byte expected = CpuZ80TestExtensions.MagicValue + Value1;

            var cpu = TestMathOperation(MathOperations.Add, Value1, expected, false);

            cpu.Registers.Flags.C.Should().Be(false);
        }

        [TestMethod]
        public void ADDn_nc_c()
        {
            byte expected = CpuZ80TestExtensions.MagicValue + Value1;

            var cpu = TestMathOperation(MathOperations.Add, Value1, expected, true);

            cpu.Registers.Flags.C.Should().Be(false);
        }

        [TestMethod]
        public void ADDn_c_nc()
        {
            byte expected = unchecked((byte)(CpuZ80TestExtensions.MagicValue + Value2));

            var cpu = TestMathOperation(MathOperations.Add, Value2, expected, false);

            cpu.Registers.Flags.C.Should().Be(true);
        }

        [TestMethod]
        public void ADDn_c_c()
        {
            byte expected = unchecked((byte)(CpuZ80TestExtensions.MagicValue + Value2));

            var cpu = TestMathOperation(MathOperations.Add, Value2, expected, true);

            cpu.Registers.Flags.C.Should().Be(true);
        }

        [TestMethod]
        public void ADCn_nc_nc()
        {
            byte expected = CpuZ80TestExtensions.MagicValue + Value1;

            var cpu = TestMathOperation(MathOperations.AddWithCarry, Value1, expected, false);

            cpu.Registers.Flags.C.Should().Be(false);
        }

        [TestMethod]
        public void ADCn_nc_c()
        {
            byte expected = CpuZ80TestExtensions.MagicValue + Value1 + 1;

            var cpu = TestMathOperation(MathOperations.AddWithCarry, Value1, expected, true);

            cpu.Registers.Flags.C.Should().Be(false);
        }

        [TestMethod]
        public void ADCn_c_nc()
        {
            byte expected = unchecked((byte)(CpuZ80TestExtensions.MagicValue + Value2));

            var cpu = TestMathOperation(MathOperations.AddWithCarry, Value2, expected, false);

            cpu.Registers.Flags.C.Should().Be(true);
        }

        [TestMethod]
        public void ADCn_c_c()
        {
            byte expected = unchecked((byte)(CpuZ80TestExtensions.MagicValue + Value2 + 1));

            var cpu = TestMathOperation(MathOperations.AddWithCarry, Value2, expected, true);

            cpu.Registers.Flags.C.Should().Be(true);
        }

        [TestMethod]
        public void SUBn_nc_nc()
        {
            byte expected = unchecked((byte)(CpuZ80TestExtensions.MagicValue - Value1));

            var cpu = TestMathOperation(MathOperations.Subtract, Value1, expected, false);

            cpu.Registers.Flags.C.Should().Be(true);
        }

        [TestMethod]
        public void SUBn_nc_c()
        {
            byte expected = unchecked((byte)(CpuZ80TestExtensions.MagicValue - Value1));

            var cpu = TestMathOperation(MathOperations.Subtract, Value1, expected, true);

            cpu.Registers.Flags.C.Should().Be(true);
        }

        [TestMethod]
        public void SUBn_c_nc()
        {
            byte expected = unchecked((byte)(CpuZ80TestExtensions.MagicValue -Value2));

            var cpu = TestMathOperation(MathOperations.Subtract, Value2, expected, false);

            cpu.Registers.Flags.C.Should().Be(true);
        }

        [TestMethod]
        public void SUBn_c_c()
        {
            byte expected = unchecked((byte)(CpuZ80TestExtensions.MagicValue - Value2));

            var cpu = TestMathOperation(MathOperations.Subtract, Value2, expected, true);

            cpu.Registers.Flags.C.Should().Be(true);
        }

        [TestMethod]
        public void SBCn_nc_nc()
        {
            byte expected = unchecked((byte)(CpuZ80TestExtensions.MagicValue - Value1));

            var cpu = TestMathOperation(MathOperations.SubtractWithCarry, Value1, expected, false);

            cpu.Registers.Flags.C.Should().Be(true);
        }

        [TestMethod]
        public void SBCn_nc_c()
        {
            byte expected = unchecked((byte)(CpuZ80TestExtensions.MagicValue - Value1 - 1));

            var cpu = TestMathOperation(MathOperations.SubtractWithCarry, Value1, expected, true);

            cpu.Registers.Flags.C.Should().Be(true);
        }

        [TestMethod]
        public void SBCn_c_nc()
        {
            byte expected = unchecked((byte)(CpuZ80TestExtensions.MagicValue - Value2));

            var cpu = TestMathOperation(MathOperations.SubtractWithCarry, Value2, expected, false);

            cpu.Registers.Flags.C.Should().Be(true);
        }

        [TestMethod]
        public void SBCn_c_c()
        {
            byte expected = unchecked((byte)(CpuZ80TestExtensions.MagicValue - Value2 - 1));

            var cpu = TestMathOperation(MathOperations.SubtractWithCarry, Value2, expected, true);

            cpu.Registers.Flags.C.Should().Be(true);
        }

        [TestMethod]
        public void ANDn_nc_nc()
        {
            byte expected = CpuZ80TestExtensions.MagicValue & Value1;

            var cpu = TestMathOperation(MathOperations.And, Value1, expected, false);

            cpu.Registers.Flags.C.Should().Be(false);
        }

        [TestMethod]
        public void ANDn_nc_c()
        {
            byte expected = CpuZ80TestExtensions.MagicValue & Value1;

            var cpu = TestMathOperation(MathOperations.And, Value1, expected, true);

            cpu.Registers.Flags.C.Should().Be(false);
        }

        [TestMethod]
        public void ANDn_c_nc()
        {
            byte expected = CpuZ80TestExtensions.MagicValue & Value2;

            var cpu = TestMathOperation(MathOperations.And, Value2, expected, false);

            cpu.Registers.Flags.C.Should().Be(false);
        }

        [TestMethod]
        public void ANDn_c_c()
        {
            byte expected = CpuZ80TestExtensions.MagicValue & Value2;

            var cpu = TestMathOperation(MathOperations.And, Value2, expected, true);

            cpu.Registers.Flags.C.Should().Be(false);
        }

        [TestMethod]
        public void XORn_nc_nc()
        {
            byte expected = CpuZ80TestExtensions.MagicValue ^ Value1;

            var cpu = TestMathOperation(MathOperations.ExlusiveOr, Value1, expected, false);

            cpu.Registers.Flags.C.Should().Be(false);
        }

        [TestMethod]
        public void XORn_nc_c()
        {
            byte expected = CpuZ80TestExtensions.MagicValue ^ Value1;

            var cpu = TestMathOperation(MathOperations.ExlusiveOr, Value1, expected, true);

            cpu.Registers.Flags.C.Should().Be(false);
        }

        [TestMethod]
        public void XORn_c_nc()
        {
            byte expected = CpuZ80TestExtensions.MagicValue ^ Value2;

            var cpu = TestMathOperation(MathOperations.ExlusiveOr, Value2, expected, false);

            cpu.Registers.Flags.C.Should().Be(false);
        }

        [TestMethod]
        public void XORn_c_c()
        {
            byte expected = CpuZ80TestExtensions.MagicValue ^ Value2;

            var cpu = TestMathOperation(MathOperations.ExlusiveOr, Value2, expected, true);

            cpu.Registers.Flags.C.Should().Be(false);
        }

        [TestMethod]
        public void ORn_nc_nc()
        {
            byte expected = CpuZ80TestExtensions.MagicValue | Value1;

            var cpu = TestMathOperation(MathOperations.Or, Value1, expected, false);

            cpu.Registers.Flags.C.Should().Be(false);
        }

        [TestMethod]
        public void ORn_nc_c()
        {
            byte expected = CpuZ80TestExtensions.MagicValue | Value1;

            var cpu = TestMathOperation(MathOperations.Or, Value1, expected, true);

            cpu.Registers.Flags.C.Should().Be(false);
        }

        [TestMethod]
        public void ORn_c_nc()
        {
            byte expected = CpuZ80TestExtensions.MagicValue | Value2;

            var cpu = TestMathOperation(MathOperations.Or, Value2, expected, false);

            cpu.Registers.Flags.C.Should().Be(false);
        }

        [TestMethod]
        public void ORn_c_c()
        {
            byte expected = CpuZ80TestExtensions.MagicValue | Value2;

            var cpu = TestMathOperation(MathOperations.Or, Value2, expected, true);

            cpu.Registers.Flags.C.Should().Be(false);
        }


        [TestMethod]
        public void CPn_nc_nc()
        {
            var cpu = TestMathOperation(MathOperations.Compare, Value1, null, false);

            cpu.Registers.Flags.C.Should().Be(true);
        }

        [TestMethod]
        public void CPn_nc_c()
        {
            var cpu = TestMathOperation(MathOperations.Compare, Value1, null, true);

            cpu.Registers.Flags.C.Should().Be(true);
            cpu.Registers.Flags.Z.Should().Be(false);
        }

        [TestMethod]
        public void CPn_c_nc()
        {
            var cpu = TestMathOperation(MathOperations.Compare, Value2, null, false);

            cpu.Registers.Flags.C.Should().Be(true);
            cpu.Registers.Flags.Z.Should().Be(false);
        }

        [TestMethod]
        public void CPn_c_c()
        {
            var cpu = TestMathOperation(MathOperations.Compare, Value2, null, true);

            cpu.Registers.Flags.C.Should().Be(true);
            cpu.Registers.Flags.Z.Should().Be(false);
        }

        [TestMethod]
        public void CPn_z()
        {
            var cpu = TestMathOperation(MathOperations.Compare, CpuZ80TestExtensions.MagicValue, null, true);

            cpu.Registers.Flags.Z.Should().Be(true);
        }


        private CpuZ80 TestMathOperation(MathOperations mathOp, byte value, byte? expectedValue,
            bool carry)
        {
            var ob = OpcodeByte.New(x: 3, z: 6, y: (byte)mathOp);

            var model = ExecuteTest(ob, value, carry);

            if (expectedValue.HasValue)
                model.Cpu.Registers.A.Should().Be(expectedValue);

            return model.Cpu;
        }

        private static SimulationModel ExecuteTest(OpcodeByte ob, byte value, bool carry)
        {
            var cpuZ80 = new CpuZ80();
            cpuZ80.Registers.Flags.C = carry;

            byte[] buffer = new byte[] { ob.Value, value };
            var model = cpuZ80.Initialize(buffer);

            cpuZ80.FillRegisters();

            model.ClockGen.SquareWave(7);

            Console.WriteLine(model.LogicAnalyzer.ToWaveJson());

            return model;
        }
    }
}
