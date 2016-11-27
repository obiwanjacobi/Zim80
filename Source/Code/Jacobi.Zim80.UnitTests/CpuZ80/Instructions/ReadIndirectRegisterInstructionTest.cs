using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.CpuZ80.Opcodes;
using Jacobi.Zim80.CpuZ80.UnitTests;

namespace Jacobi.Zim80.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class ReadIndirectRegisterInstructionTest
    {
        private const ushort Address = 0x0005;
        private const byte Offset = 0xFF;   //-1

        private const byte ExpectedValue = 0xAA;
        private const ushort ExpectedValueHi = 0xAA2A;

        [TestMethod]
        public void LdA_BC()
        {
            var ob = OpcodeByte.New(z: 2, q: 1, p: 0);

            var cpu = ExecuteTest(ob, (cpuZ80) => cpuZ80.Registers.BC = Address);

            cpu.AssertRegisters(a: ExpectedValue, bc: Address);
        }

        [TestMethod]
        public void LdA_DE()
        {
            var ob = OpcodeByte.New(z: 2, q: 1, p: 1);

            var cpu = ExecuteTest(ob, (cpuZ80) => cpuZ80.Registers.DE = Address);

            cpu.AssertRegisters(a: ExpectedValue, de: Address);
        }

        [TestMethod]
        public void LdB_HL()
        {
            var ob = OpcodeByte.New(x: 1, z: 6, y: 0);

            var cpu = ExecuteTest(ob, (cpuZ80) => cpuZ80.Registers.HL = Address);

            cpu.AssertRegisters(bc: ExpectedValueHi, hl: Address);
        }

        [TestMethod]
        public void LdC_HL()
        {
            var ob = OpcodeByte.New(x: 1, z: 6, y: 1);

            var cpu = ExecuteTest(ob, (cpuZ80) => cpuZ80.Registers.HL = Address);

            cpu.AssertRegisters(bc: ExpectedValue, hl: Address);
        }

        [TestMethod]
        public void LdD_HL()
        {
            var ob = OpcodeByte.New(x: 1, z: 6, y: 2);

            var cpu = ExecuteTest(ob, (cpuZ80) => cpuZ80.Registers.HL = Address);

            cpu.AssertRegisters(de: ExpectedValueHi, hl: Address);
        }

        [TestMethod]
        public void LdE_HL()
        {
            var ob = OpcodeByte.New(x: 1, z: 6, y: 3);

            var cpu = ExecuteTest(ob, (cpuZ80) => cpuZ80.Registers.HL = Address);

            cpu.AssertRegisters(de: ExpectedValue, hl: Address);
        }

        [TestMethod]
        public void LdH_HL()
        {
            var ob = OpcodeByte.New(x: 1, z: 6, y: 4);

            var cpu = ExecuteTest(ob, (cpuZ80) => cpuZ80.Registers.HL = Address);

            cpu.AssertRegisters(hl: (ExpectedValue << 8) | Address);
        }

        [TestMethod]
        public void LdL_HL()
        {
            var ob = OpcodeByte.New(x: 1, z: 6, y: 5);

            var cpu = ExecuteTest(ob, (cpuZ80) => cpuZ80.Registers.HL = Address);

            cpu.AssertRegisters(hl: ExpectedValue);
        }

        [TestMethod]
        public void LdA_HL()
        {
            var ob = OpcodeByte.New(x: 1, z: 6, y: 0);

            var cpu = ExecuteTest(ob, (cpuZ80) => cpuZ80.Registers.HL = Address);

            cpu.AssertRegisters(bc: ExpectedValueHi, hl: Address);
        }

        [TestMethod]
        public void LdB_IXd()
        {
            var ob = OpcodeByte.New(x: 1, z: 6, y: 0);

            var cpu = ExecuteTest(ob, (cpuZ80) => cpuZ80.Registers.IX = Address, 0xDD);

            cpu.AssertRegisters(bc: ExpectedValueHi, ix: Address);
        }

        [TestMethod]
        public void LdC_IXd()
        {
            var ob = OpcodeByte.New(x: 1, z: 6, y: 1);

            var cpu = ExecuteTest(ob, (cpuZ80) => cpuZ80.Registers.IX = Address, 0xDD);

            cpu.AssertRegisters(bc: ExpectedValue, ix: Address);
        }

        [TestMethod]
        public void LdD_IXd()
        {
            var ob = OpcodeByte.New(x: 1, z: 6, y: 2);

            var cpu = ExecuteTest(ob, (cpuZ80) => cpuZ80.Registers.IX = Address, 0xDD);

            cpu.AssertRegisters(de: ExpectedValueHi, ix: Address);
        }

        [TestMethod]
        public void LdE_IXd()
        {
            var ob = OpcodeByte.New(x: 1, z: 6, y: 3);

            var cpu = ExecuteTest(ob, (cpuZ80) => cpuZ80.Registers.IX = Address, 0xDD);

            cpu.AssertRegisters(de: ExpectedValue, ix: Address);
        }

        [TestMethod]
        public void LdH_IXd()
        {
            var ob = OpcodeByte.New(x: 1, z: 6, y: 4);

            var cpu = ExecuteTest(ob, (cpuZ80) => cpuZ80.Registers.IX = Address, 0xDD);

            cpu.AssertRegisters(hl: ExpectedValueHi, ix: Address);
        }

        [TestMethod]
        public void LdL_IXd()
        {
            var ob = OpcodeByte.New(x: 1, z: 6, y: 5);

            var cpu = ExecuteTest(ob, (cpuZ80) => cpuZ80.Registers.IX = Address, 0xDD);

            cpu.AssertRegisters(hl: ExpectedValue, ix: Address);
        }

        [TestMethod]
        public void LdA_IXd()
        {
            var ob = OpcodeByte.New(x: 1, z: 6, y: 0);

            var cpu = ExecuteTest(ob, (cpuZ80) => cpuZ80.Registers.IX = Address, 0xDD);

            cpu.AssertRegisters(bc: ExpectedValueHi, ix: Address);
        }

        [TestMethod]
        public void LdB_IYd()
        {
            var ob = OpcodeByte.New(x: 1, z: 6, y: 0);

            var cpu = ExecuteTest(ob, (cpuZ80) => cpuZ80.Registers.IY = Address, 0xFD);

            cpu.AssertRegisters(bc: ExpectedValueHi, iy: Address);
        }

        [TestMethod]
        public void LdC_IYd()
        {
            var ob = OpcodeByte.New(x: 1, z: 6, y: 1);

            var cpu = ExecuteTest(ob, (cpuZ80) => cpuZ80.Registers.IY = Address, 0xFD);

            cpu.AssertRegisters(bc: ExpectedValue, iy: Address);
        }

        [TestMethod]
        public void LdD_IYd()
        {
            var ob = OpcodeByte.New(x: 1, z: 6, y: 2);

            var cpu = ExecuteTest(ob, (cpuZ80) => cpuZ80.Registers.IY = Address, 0xFD);

            cpu.AssertRegisters(de: ExpectedValueHi, iy: Address);
        }

        [TestMethod]
        public void LdE_IYd()
        {
            var ob = OpcodeByte.New(x: 1, z: 6, y: 3);

            var cpu = ExecuteTest(ob, (cpuZ80) => cpuZ80.Registers.IY = Address, 0xFD);

            cpu.AssertRegisters(de: ExpectedValue, iy: Address);
        }

        [TestMethod]
        public void LdH_IYd()
        {
            var ob = OpcodeByte.New(x: 1, z: 6, y: 4);

            var cpu = ExecuteTest(ob, (cpuZ80) => cpuZ80.Registers.IY = Address, 0xFD);

            cpu.AssertRegisters(hl: ExpectedValueHi, iy: Address);
        }

        [TestMethod]
        public void LdL_IYd()
        {
            var ob = OpcodeByte.New(x: 1, z: 6, y: 5);

            var cpu = ExecuteTest(ob, (cpuZ80) => cpuZ80.Registers.IY = Address, 0xFD);

            cpu.AssertRegisters(hl: ExpectedValue, iy: Address);
        }

        [TestMethod]
        public void LdA_IYd()
        {
            var ob = OpcodeByte.New(x: 1, z: 6, y: 0);

            var cpu = ExecuteTest(ob, (cpuZ80) => cpuZ80.Registers.IY = Address, 0xFD);

            cpu.AssertRegisters(bc: ExpectedValueHi, iy: Address);
        }

        private static CpuZ80 ExecuteTest(OpcodeByte ob, Action<CpuZ80> preTest, byte extension = 0)
        {
            var cpuZ80 = new CpuZ80();
            var buffer = extension == 0 ? new byte[] { ob.Value, 0, 0, 0, 0, ExpectedValue } :
                             new byte[] { extension, ob.Value, Offset, 0, ExpectedValue, 0 };
            var model = cpuZ80.Initialize(buffer);

            cpuZ80.FillRegisters();
            preTest(cpuZ80);

            model.ClockGen.SquareWave(extension == 0 ? 7 : 19);

            return cpuZ80;
        }
    }
}
