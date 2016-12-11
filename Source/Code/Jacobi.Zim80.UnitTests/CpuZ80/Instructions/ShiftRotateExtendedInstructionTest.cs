using FluentAssertions;
using Jacobi.Zim80.CpuZ80.Opcodes;
using Jacobi.Zim80.CpuZ80.UnitTests;
using Jacobi.Zim80.Diagnostics;
using Jacobi.Zim80.Memory.UnitTests;
using Jacobi.Zim80.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Jacobi.Zim80.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class ShiftRotateExtendedInstructionTest
    {
        private const ushort Address = 0x05;
        private const byte Offset = 0xFF;   //-1
        private const byte ExpectedAddress = 0x04;

        private const byte Value1 = 0x5E;
        private const byte Value2 = 0xAF;

        private const byte ExpectedValue1L = 0xBC;
        private const byte ExpectedValue2L = 0x5E; //+ carry

        private const byte ExpectedValue1R = 0x2F;
        private const byte ExpectedValue2R = 0x57; // + carry

        private const byte IX = 0xDD;
        private const byte IY = 0xFD;

        [TestMethod]
        public void RLC_IXd_nc()
        {
            ExecuteTestAllRegs(IX, ShiftRotateOperations.RotateLeftCarry, Value1, 
                ExpectedValue1L, expectedCarry: false, expectedSigned: true);
        }

        [TestMethod]
        public void RLC_IXd_c()
        {
            ExecuteTestAllRegs(IX, ShiftRotateOperations.RotateLeftCarry, Value2,
                ExpectedValue2L | 0x01, expectedCarry: true, expectedSigned: false);
        }

        [TestMethod]
        public void RRC_IXd_nc()
        {
            ExecuteTestAllRegs(IX, ShiftRotateOperations.RotateRightCarry, Value1,
                ExpectedValue1R, expectedCarry: false, expectedSigned: false);
        }

        [TestMethod]
        public void RRC_IXd_c()
        {
            ExecuteTestAllRegs(IX, ShiftRotateOperations.RotateRightCarry, Value2,
                ExpectedValue2R | 0x80, expectedCarry: true, expectedSigned: true);
        }

        [TestMethod]
        public void RL_IXd_nc_nc()
        {
            ExecuteTestAllRegs(IX, ShiftRotateOperations.RotateLeft, Value1,
                ExpectedValue1L, expectedCarry: false, expectedSigned: true, 
                carry: false);
        }

        [TestMethod]
        public void RL_IXd_c_nc()
        {
            ExecuteTestAllRegs(IX, ShiftRotateOperations.RotateLeft, Value2,
                ExpectedValue2L, expectedCarry: true, expectedSigned: false, 
                carry: false);
        }

        [TestMethod]
        public void RL_IXd_nc_c()
        {
            ExecuteTestAllRegs(IX, ShiftRotateOperations.RotateLeft, Value1,
                ExpectedValue1L | 0x01, expectedCarry: false, expectedSigned: true, 
                carry: true);
        }

        [TestMethod]
        public void RL_IXd_c_c()
        {
            ExecuteTestAllRegs(IX, ShiftRotateOperations.RotateLeft, Value2,
                ExpectedValue2L | 0x01, expectedCarry: true, expectedSigned: false, 
                carry: true);
        }

        [TestMethod]
        public void RR_IXd_nc_nc()
        {
            ExecuteTestAllRegs(IX, ShiftRotateOperations.RotateRight, Value1,
                ExpectedValue1R, expectedCarry: false, expectedSigned: false, 
                carry: false);
        }

        [TestMethod]
        public void RR_IXd_c_nc()
        {
            ExecuteTestAllRegs(IX, ShiftRotateOperations.RotateRight, Value2,
                ExpectedValue2R, expectedCarry: true, expectedSigned: false, 
                carry: false);
        }

        [TestMethod]
        public void RR_IXd_nc_c()
        {
            ExecuteTestAllRegs(IX, ShiftRotateOperations.RotateRight, Value1,
                ExpectedValue1R | 0x80, expectedCarry: false, expectedSigned: true, 
                carry: true);
        }

        [TestMethod]
        public void RR_IXd_c_c()
        {
            ExecuteTestAllRegs(IX, ShiftRotateOperations.RotateRight, Value2,
                ExpectedValue2R | 0x80, expectedCarry: true, expectedSigned: true, 
                carry: true);
        }

        [TestMethod]
        public void SLA_IXd_nc()
        {
            ExecuteTestAllRegs(IX, ShiftRotateOperations.ShiftLeftArithmetic, Value1,
                ExpectedValue1L, expectedCarry: false, expectedSigned: true);
        }

        [TestMethod]
        public void SLA_IXd_c()
        {
            ExecuteTestAllRegs(IX, ShiftRotateOperations.ShiftLeftArithmetic, Value2,
                ExpectedValue2L, expectedCarry: true, expectedSigned: false);
        }

        [TestMethod]
        public void SRA_IXd_nc()
        {
            ExecuteTestAllRegs(IX, ShiftRotateOperations.ShiftRightArithmetic, Value1,
                ExpectedValue1R, expectedCarry: false, expectedSigned: false);
        }

        [TestMethod]
        public void SRA_IXd_c()
        {
            ExecuteTestAllRegs(IX, ShiftRotateOperations.ShiftRightArithmetic, Value2,
                ExpectedValue2R | 0x80, expectedCarry: true, expectedSigned: true);
        }

        [TestMethod]
        public void SLL_IXd_nc()
        {
            ExecuteTestAllRegs(IX, ShiftRotateOperations.ShiftLeftLogical, Value1,
                ExpectedValue1L | 0x01, expectedCarry: false, expectedSigned: true);
        }

        [TestMethod]
        public void SLL_IXd_c()
        {
            ExecuteTestAllRegs(IX, ShiftRotateOperations.ShiftLeftLogical, Value2,
                ExpectedValue2L | 0x01, expectedCarry: true, expectedSigned: false);
        }

        [TestMethod]
        public void SRL_IXd_nc()
        {
            ExecuteTestAllRegs(IX, ShiftRotateOperations.ShiftRightLogical, Value1,
                ExpectedValue1R | 0x01, expectedCarry: false, expectedSigned: false);
        }

        [TestMethod]
        public void SRL_IXd_c()
        {
            ExecuteTestAllRegs(IX, ShiftRotateOperations.ShiftRightLogical, Value2,
                ExpectedValue2R | 0x01, expectedCarry: true, expectedSigned: false);
        }

        //
        //  IY tests
        //

        [TestMethod]
        public void RLC_IYd_nc()
        {
            ExecuteTestAllRegs(IY, ShiftRotateOperations.RotateLeftCarry, Value1,
                ExpectedValue1L, expectedCarry: false, expectedSigned: true);
        }

        [TestMethod]
        public void RLC_IYd_c()
        {
            ExecuteTestAllRegs(IY, ShiftRotateOperations.RotateLeftCarry, Value2,
                ExpectedValue2L | 0x01, expectedCarry: true, expectedSigned: false);
        }

        [TestMethod]
        public void RRC_IYd_nc()
        {
            ExecuteTestAllRegs(IY, ShiftRotateOperations.RotateRightCarry, Value1,
                ExpectedValue1R, expectedCarry: false, expectedSigned: false);
        }

        [TestMethod]
        public void RRC_IYd_c()
        {
            ExecuteTestAllRegs(IY, ShiftRotateOperations.RotateRightCarry, Value2,
                ExpectedValue2R | 0x80, expectedCarry: true, expectedSigned: true);
        }

        [TestMethod]
        public void RL_IYd_nc_nc()
        {
            ExecuteTestAllRegs(IY, ShiftRotateOperations.RotateLeft, Value1,
                ExpectedValue1L, expectedCarry: false, expectedSigned: true,
                carry: false);
        }

        [TestMethod]
        public void RL_IYd_c_nc()
        {
            ExecuteTestAllRegs(IY, ShiftRotateOperations.RotateLeft, Value2,
                ExpectedValue2L, expectedCarry: true, expectedSigned: false,
                carry: false);
        }

        [TestMethod]
        public void RL_IYd_nc_c()
        {
            ExecuteTestAllRegs(IY, ShiftRotateOperations.RotateLeft, Value1,
                ExpectedValue1L | 0x01, expectedCarry: false, expectedSigned: true,
                carry: true);
        }

        [TestMethod]
        public void RL_IYd_c_c()
        {
            ExecuteTestAllRegs(IY, ShiftRotateOperations.RotateLeft, Value2,
                ExpectedValue2L | 0x01, expectedCarry: true, expectedSigned: false,
                carry: true);
        }

        [TestMethod]
        public void RR_IYd_nc_nc()
        {
            ExecuteTestAllRegs(IY, ShiftRotateOperations.RotateRight, Value1,
                ExpectedValue1R, expectedCarry: false, expectedSigned: false,
                carry: false);
        }

        [TestMethod]
        public void RR_IYd_c_nc()
        {
            ExecuteTestAllRegs(IY, ShiftRotateOperations.RotateRight, Value2,
                ExpectedValue2R, expectedCarry: true, expectedSigned: false,
                carry: false);
        }

        [TestMethod]
        public void RR_IYd_nc_c()
        {
            ExecuteTestAllRegs(IY, ShiftRotateOperations.RotateRight, Value1,
                ExpectedValue1R | 0x80, expectedCarry: false, expectedSigned: true,
                carry: true);
        }

        [TestMethod]
        public void RR_IYd_c_c()
        {
            ExecuteTestAllRegs(IY, ShiftRotateOperations.RotateRight, Value2,
                ExpectedValue2R | 0x80, expectedCarry: true, expectedSigned: true,
                carry: true);
        }

        [TestMethod]
        public void SLA_IYd_nc()
        {
            ExecuteTestAllRegs(IY, ShiftRotateOperations.ShiftLeftArithmetic, Value1,
                ExpectedValue1L, expectedCarry: false, expectedSigned: true);
        }

        [TestMethod]
        public void SLA_IYd_c()
        {
            ExecuteTestAllRegs(IY, ShiftRotateOperations.ShiftLeftArithmetic, Value2,
                ExpectedValue2L, expectedCarry: true, expectedSigned: false);
        }

        [TestMethod]
        public void SRA_IYd_nc()
        {
            ExecuteTestAllRegs(IY, ShiftRotateOperations.ShiftRightArithmetic, Value1,
                ExpectedValue1R, expectedCarry: false, expectedSigned: false);
        }

        [TestMethod]
        public void SRA_IYd_c()
        {
            ExecuteTestAllRegs(IY, ShiftRotateOperations.ShiftRightArithmetic, Value2,
                ExpectedValue2R | 0x80, expectedCarry: true, expectedSigned: true);
        }

        [TestMethod]
        public void SLL_IYd_nc()
        {
            ExecuteTestAllRegs(IY, ShiftRotateOperations.ShiftLeftLogical, Value1,
                ExpectedValue1L | 0x01, expectedCarry: false, expectedSigned: true);
        }

        [TestMethod]
        public void SLL_IYd_c()
        {
            ExecuteTestAllRegs(IY, ShiftRotateOperations.ShiftLeftLogical, Value2,
                ExpectedValue2L | 0x01, expectedCarry: true, expectedSigned: false);
        }

        [TestMethod]
        public void SRL_IYd_nc()
        {
            ExecuteTestAllRegs(IY, ShiftRotateOperations.ShiftRightLogical, Value1,
                ExpectedValue1R | 0x01, expectedCarry: false, expectedSigned: false);
        }

        [TestMethod]
        public void SRL_IYd_c()
        {
            ExecuteTestAllRegs(IY, ShiftRotateOperations.ShiftRightLogical, Value2,
                ExpectedValue2R | 0x01, expectedCarry: true, expectedSigned: false);
        }

        private void ExecuteTestAllRegs(byte extension, ShiftRotateOperations y, byte value,
            byte expectedValue, bool expectedCarry, bool expectedSigned, 
            bool carry = false)
        {
            ForEachRegister((reg) =>
            {
                var ob = OpcodeByte.New(x: 0, z: (byte)reg, y: (byte)y);
                var model = ExecuteTest(ob, value, (cpu) =>
                {
                    if (extension == 0xDD)
                        cpu.Registers.IX = Address;
                    if (extension == 0xFD)
                        cpu.Registers.IY = Address;

                    cpu.Registers.Flags.C = carry;

                }, extension);

                if (reg != Register8Table.HL)
                    model.Cpu.Registers[reg].Should().Be(expectedValue);

                model.Memory.Assert(ExpectedAddress, expectedValue);
                model.Cpu.Registers.Flags.S.Should().Be(expectedSigned);
                model.Cpu.Registers.Flags.Z.Should().Be(false);
                model.Cpu.Registers.Flags.C.Should().Be(expectedCarry);
                model.Cpu.Registers.Flags.N.Should().Be(false);
                model.Cpu.Registers.Flags.H.Should().Be(false);
            });
        }

        private static void ForEachRegister(Action<Register8Table> action)
        {
            for (Register8Table reg = Register8Table.B; reg <= Register8Table.A; reg++)
            {
                action(reg);
            }
        }

        private static SimulationModel ExecuteTest(OpcodeByte ob, byte value, Action<CpuZ80> preTest, byte extension)
        {
            var cpuZ80 = new CpuZ80();
            byte[] buffer = new byte[] { extension, 0xCB, Offset, ob.Value, value };
            var model = cpuZ80.Initialize(buffer);

            cpuZ80.FillRegisters();
            preTest(cpuZ80);

            model.ClockGen.SquareWave(23);

            Console.WriteLine(model.LogicAnalyzer.ToWaveJson());

            return model;
        }
    }
}
