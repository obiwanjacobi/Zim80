using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using Jacobi.Zim80.Components.CpuZ80.UnitTests;
using FluentAssertions;

namespace Jacobi.Zim80.Components.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class MathIndirectInstructionTest
    {
        private const ushort Address = 0x0005;
        private const byte Offset = 0xFF;   //-1

        private const byte Value = 0xAA;

        [TestMethod]
        public void Add_HL_()
        {
            Add();
        }

        [TestMethod]
        public void Add_IXd_()
        {
            Add(extension: 0xDD);
        }

        [TestMethod]
        public void Add_IYd_()
        {
            Add(extension: 0xFD);
        }

        private void Add(byte extension = 0)
        {
            var ob = OpcodeByte.New(x: 2, z: 6, y: (byte)MathOperations.Add);
            var expectedValue = (byte)(CpuZ80TestExtensions.MagicValue + Value);

            var cpu = TestMathOperation(ob, expectedValue, carry: false, extension: extension);

            var flags = cpu.Registers.Flags;
            flags.S.Should().Be(true);
            flags.Z.Should().BeFalse();
            flags.C.Should().BeFalse();
            flags.H.Should().BeTrue();
            flags.N.Should().BeFalse();
            flags.PV.Should().BeFalse();
        }
        
        [TestMethod]
        public void Adc_HL_nc()
        {
            Adc(carry: false);
        }

        [TestMethod]
        public void Adc_IXd_nc()
        {
            Adc(carry: false, extension: 0xDD);
        }

        [TestMethod]
        public void Adc_IYd_nc()
        {
            Adc(carry: false, extension: 0xFD);
        }

        [TestMethod]
        public void Adc_HL_c()
        {
            Adc(carry: true);
        }

        [TestMethod]
        public void Adc_IXd_c()
        {
            Adc(carry: true, extension: 0xDD);
        }

        [TestMethod]
        public void Adc_IYd_c()
        {
            Adc(carry: true, extension: 0xFD);
        }

        private void Adc(bool carry, byte extension = 0)
        {
            var ob = OpcodeByte.New(x: 2, z: 6, y: (byte)MathOperations.AddWithCarry);
            var expectedValue = (byte)(CpuZ80TestExtensions.MagicValue + Value);
            if (carry) expectedValue += 1;

            var cpu = TestMathOperation(ob, expectedValue, carry: carry, extension: extension);

            var flags = cpu.Registers.Flags;
            flags.S.Should().Be(true);
            flags.Z.Should().BeFalse();
            flags.C.Should().BeFalse();
            flags.H.Should().BeTrue();
            flags.N.Should().BeFalse();
            flags.PV.Should().BeFalse();
        }

        [TestMethod]
        public void Sub_HL_()
        {
            Sub();
        }

        [TestMethod]
        public void Sub_IXd_()
        {
            Sub(extension: 0xDD);
        }

        [TestMethod]
        public void Sub_IYd_()
        {
            Sub(extension: 0xFD);
        }

        private void Sub(byte extension = 0)
        {
            var ob = OpcodeByte.New(x: 2, z: 6, y: (byte)MathOperations.Subtract);
            var expectedValue = unchecked((byte)(CpuZ80TestExtensions.MagicValue - Value));

            var cpu = TestMathOperation(ob, expectedValue, carry: false, extension: extension);

            var flags = cpu.Registers.Flags;
            flags.S.Should().Be(true);
            flags.Z.Should().Be(false);
            flags.C.Should().Be(true);
            flags.H.Should().Be(false);
            flags.N.Should().Be(true);
            flags.PV.Should().Be(false);
        }

        [TestMethod]
        public void Sbc_HL_nc()
        {
            Sbc(carry: false);
        }

        [TestMethod]
        public void Sbc_IXd_nc()
        {
            Sbc(carry: false, extension: 0xDD);
        }

        [TestMethod]
        public void Sbc_IYd_nc()
        {
            Sbc(carry: false, extension: 0xFD);
        }

        [TestMethod]
        public void Sbc_HL_c()
        {
            Sbc(carry: true);
        }

        [TestMethod]
        public void Sbc_IXd_c()
        {
            Sbc(carry: true, extension: 0xDD);
        }

        [TestMethod]
        public void Sbc_IYd_c()
        {
            Sbc(carry: true, extension: 0xFD);
        }

        private void Sbc(bool carry, byte extension = 0)
        {
            var ob = OpcodeByte.New(x: 2, z: 6, y: (byte)MathOperations.SubtractWithCarry);
            var expectedValue = unchecked((byte)(CpuZ80TestExtensions.MagicValue - Value));
            if (carry) expectedValue -= 1;

            var cpu = TestMathOperation(ob, expectedValue, carry: carry, extension: extension);

            var flags = cpu.Registers.Flags;
            flags.S.Should().Be(!carry);
            flags.Z.Should().BeFalse();
            flags.C.Should().Be(true);
            flags.H.Should().Be(false);
            flags.N.Should().Be(true);
            flags.PV.Should().BeFalse();
        }

        [TestMethod]
        public void And_HL_()
        {
            And();
        }

        [TestMethod]
        public void And_IXd_()
        {
            And(extension: 0xDD);
        }

        [TestMethod]
        public void And_IYd_()
        {
            And(extension: 0xFD);
        }

        private void And(byte extension = 0)
        {
            var ob = OpcodeByte.New(x: 2, z: 6, y: (byte)MathOperations.And);
            var expectedValue = unchecked((byte)(CpuZ80TestExtensions.MagicValue & Value));

            var cpu = TestMathOperation(ob, expectedValue, carry: false, extension: extension);

            var flags = cpu.Registers.Flags;
            flags.S.Should().Be(false);
            flags.Z.Should().BeFalse();
            flags.C.Should().Be(false);
            flags.H.Should().Be(true);
            flags.N.Should().Be(false);
            flags.PV.Should().BeFalse();
        }

        [TestMethod]
        public void Xor_HL_()
        {
            Xor();
        }

        [TestMethod]
        public void Xor_IXd_()
        {
            Xor(extension: 0xDD);
        }

        [TestMethod]
        public void Xor_IYd_()
        {
            Xor(extension: 0xFD);
        }

        private void Xor(byte extension = 0)
        {
            var ob = OpcodeByte.New(x: 2, z: 6, y: (byte)MathOperations.ExlusiveOr);
            var expectedValue = unchecked((byte)(CpuZ80TestExtensions.MagicValue ^ Value));

            var cpu = TestMathOperation(ob, expectedValue, carry: false, extension: extension);

            var flags = cpu.Registers.Flags;
            flags.S.Should().Be(true);
            flags.Z.Should().BeFalse();
            flags.C.Should().Be(false);
            flags.H.Should().Be(false);
            flags.N.Should().Be(false);
            flags.PV.Should().BeFalse();
        }

        [TestMethod]
        public void Or_HL_()
        {
            Or();
        }

        [TestMethod]
        public void Or_IXd_()
        {
            Or(extension: 0xDD);
        }

        [TestMethod]
        public void Or_IYd_()
        {
            Or(extension: 0xFD);
        }

        private void Or(byte extension = 0)
        {
            var ob = OpcodeByte.New(x: 2, z: 6, y: (byte)MathOperations.Or);
            var expectedValue = unchecked((byte)(CpuZ80TestExtensions.MagicValue | Value));

            var cpu = TestMathOperation(ob, expectedValue, carry: false, extension: extension);

            var flags = cpu.Registers.Flags;
            flags.S.Should().Be(true);
            flags.Z.Should().BeFalse();
            flags.C.Should().Be(false);
            flags.H.Should().Be(false);
            flags.N.Should().Be(false);
            flags.PV.Should().BeFalse();
        }

        [TestMethod]
        public void Cp_HL_()
        {
            Cp();
        }

        [TestMethod]
        public void Cp_IXd_()
        {
            Cp(extension: 0xDD);
        }

        [TestMethod]
        public void Cp_IYd_()
        {
            Cp(extension: 0xFD);
        }

        private void Cp(byte extension = 0)
        {
            var ob = OpcodeByte.New(x: 2, z: 6, y: (byte)MathOperations.Compare);
            var expectedValue = CpuZ80TestExtensions.MagicValue;

            var cpu = TestMathOperation(ob, expectedValue, carry: false, extension: extension);

            var flags = cpu.Registers.Flags;
            flags.S.Should().Be(true);
            flags.Z.Should().BeFalse();
            flags.C.Should().Be(true);
            flags.H.Should().Be(false);
            flags.N.Should().Be(true);
            flags.PV.Should().BeFalse();
        }

        private CpuZ80 TestMathOperation(OpcodeByte ob, byte expectedValue, bool carry, byte extension = 0)
        {
            var cpu = ExecuteTest(ob,
                (cpuZ80) => {
                    cpuZ80.Registers.Flags.C = carry;
                    //cpuZ80.Registers.A = Value;

                    if (extension == 0) cpuZ80.Registers.HL = Address;
                    if (extension == 0xDD) cpuZ80.Registers.IX = Address;
                    if (extension == 0xFD) cpuZ80.Registers.IY = Address;

                }, extension);

            cpu.Registers.A.Should().Be(expectedValue);

            return cpu;
        }

        private static CpuZ80 ExecuteTest(OpcodeByte ob, Action<CpuZ80> preTest, byte extension = 0)
        {
            var cpuZ80 = new CpuZ80();
            var buffer = extension == 0 ? new byte[] { ob.Value, 0, 0, 0, 0, Value } :
                             new byte[] { extension, ob.Value, Offset, 0, Value, 0 };
            var model = cpuZ80.Initialize(buffer);

            cpuZ80.FillRegisters();
            preTest(cpuZ80);

            model.ClockGen.SquareWave(extension == 0 ? 7 : 19);

            return cpuZ80;
        }
    }
}
