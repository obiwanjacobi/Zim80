using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.Test;
using Jacobi.Zim80.CpuZ80.Opcodes;
using Jacobi.Zim80.CpuZ80.UnitTests;
using Jacobi.Zim80.Memory.UnitTests;

namespace Jacobi.Zim80.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class LoadRepeatInstructionTest
    {
        private const ushort RdAddress = 0x04;
        private const ushort WrAddress = 0x08;
        private const byte Value1 = 0xAA;
        private const byte Value2 = 0xAA;
        private const byte Length = 0x02;

        [TestMethod]
        public void LDI()
        {
            var ob = OpcodeByte.New(x: 2, z: 0, y: 4);
            var model = ExecuteTest(ob, (cpu) =>
            {
                cpu.Registers.HL = RdAddress;
                cpu.Registers.DE = WrAddress;
                cpu.Registers.BC = Length;

            }, true);

            model.Cpu.AssertRegisters(hl: RdAddress + 1, de: WrAddress + 1, bc: Length - 1);
            model.Memory.Assert(WrAddress, Value1);
        }

        [TestMethod]
        public void LDD()
        {
            var ob = OpcodeByte.New(x: 2, z: 0, y: 5);
            var model = ExecuteTest(ob, (cpu) =>
            {
                cpu.Registers.HL = RdAddress + 1;
                cpu.Registers.DE = WrAddress + 1;
                cpu.Registers.BC = Length;

            }, true);

            model.Cpu.AssertRegisters(hl: RdAddress, de: WrAddress, bc: Length - 1);
            model.Memory.Assert(WrAddress + 1, Value2);
        }

        [TestMethod]
        public void LDIR()
        {
            var ob = OpcodeByte.New(x: 2, z: 0, y: 6);
            var model = ExecuteTest(ob, (cpu) =>
            {
                cpu.Registers.HL = RdAddress;
                cpu.Registers.DE = WrAddress;
                cpu.Registers.BC = Length;

            }, false);

            model.Cpu.AssertRegisters(hl: RdAddress + 2, de: WrAddress + 2, bc: 0);
            model.Memory.Assert(WrAddress, Value1);
            model.Memory.Assert(WrAddress + 1, Value2);
        }

        [TestMethod]
        public void LDDR()
        {
            var ob = OpcodeByte.New(x: 2, z: 0, y: 7);
            var model = ExecuteTest(ob, (cpu) =>
            {
                cpu.Registers.HL = RdAddress + 1;
                cpu.Registers.DE = WrAddress + 1;
                cpu.Registers.BC = Length;

            }, false);

            model.Cpu.AssertRegisters(hl: RdAddress - 1, de: WrAddress - 1, bc: 0);
            model.Memory.Assert(WrAddress, Value1);
            model.Memory.Assert(WrAddress + 1, Value2);
        }

        private SimulationModel ExecuteTest(OpcodeByte ob, Action<CpuZ80> preTest, bool isConditionMet)
        {
            var cpu = new CpuZ80();
            var model = cpu.Initialize(
                new byte[] { 0xED, ob.Value, 0, 0, Value1, Value2, 0, 0, 0, 0, 0 });

            cpu.FillRegisters();
            preTest(cpu);

            long cycles = 16;
            if (!isConditionMet) cycles += 21;
            model.ClockGen.SquareWave(cycles);

            return model;
        }
    }
}
