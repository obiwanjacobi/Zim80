using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.CpuZ80.Opcodes;
using Jacobi.Zim80.Test;
using Jacobi.Zim80.CpuZ80.UnitTests;
using Jacobi.Zim80.Memory.UnitTests;

namespace Jacobi.Zim80.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class LoadImmediateIndirect16InstructionTest
    {
        private const byte AddressLo = 0x05;
        private const byte AddressHi = 0;

        private const ushort Expected = 0x55AA;
        private const byte ExpectedHi = 0x55;
        private const byte ExpectedLo = 0xAA;

        [TestMethod]
        public void LdBC_nn_()
        {
            var model = ExecuteTest(Register16Table.BC, write: false);

            model.Cpu.AssertRegisters(bc: Expected);
        }

        [TestMethod]
        public void LdDE_nn_()
        {
            var model = ExecuteTest(Register16Table.DE, write: false);

            model.Cpu.AssertRegisters(de: Expected);
        }

        [TestMethod]
        public void LdHL_nn_()
        {
            var model = ExecuteTest(Register16Table.HL, write: false);

            model.Cpu.AssertRegisters(hl: Expected);
        }

        [TestMethod]
        public void LdSP_nn_()
        {
            var model = ExecuteTest(Register16Table.SP, write: false);

            model.Cpu.AssertRegisters(sp: Expected);
        }

        [TestMethod]
        public void Ld_nn_BC()
        {
            var model = ExecuteTest(Register16Table.BC, write: true);

            model.Cpu.AssertRegisters(bc: Expected);
            model.Memory.Assert(AddressLo, ExpectedLo);
            model.Memory.Assert((ushort)(AddressLo + 1), ExpectedHi);
        }

        [TestMethod]
        public void Ld_nn_DE()
        {
            var model = ExecuteTest(Register16Table.DE, write: true);

            model.Cpu.AssertRegisters(de: Expected);
            model.Memory.Assert(AddressLo, ExpectedLo);
            model.Memory.Assert((ushort)(AddressLo + 1), ExpectedHi);
        }

        [TestMethod]
        public void Ld_nn_HL()
        {
            var model = ExecuteTest(Register16Table.HL, write: true);

            model.Cpu.AssertRegisters(hl: Expected);
            model.Memory.Assert(AddressLo, ExpectedLo);
            model.Memory.Assert((ushort)(AddressLo + 1), ExpectedHi);
        }

        [TestMethod]
        public void Ld_nn_SP()
        {
            var model = ExecuteTest(Register16Table.SP, write: true);

            model.Cpu.AssertRegisters(sp: Expected);
            model.Memory.Assert(AddressLo, ExpectedLo);
            model.Memory.Assert((ushort)(AddressLo + 1), ExpectedHi);
        }

        private SimulationModel ExecuteTest(Register16Table reg, bool write)
        {
            var ob = OpcodeByte.New(x:1, z: 3, 
                q: (byte)(write ? 0: 1), p: (byte)reg);

            var cpuZ80 = new CpuZ80();
            var buffer = write ?
                new byte[] { 0xED, ob.Value, AddressLo, AddressHi, 0, 0, 0 }:
                new byte[] { 0xED, ob.Value, AddressLo, AddressHi, 0, ExpectedLo, ExpectedHi };
            var model = cpuZ80.Initialize(buffer);

            cpuZ80.FillRegisters();
            if (write)
            {
                if (reg == Register16Table.SP)
                    cpuZ80.Registers.SP = Expected;
                else
                    cpuZ80.Registers[reg] = Expected;
            }

            model.ClockGen.SquareWave(20);

            return model;
        }
    }
}
