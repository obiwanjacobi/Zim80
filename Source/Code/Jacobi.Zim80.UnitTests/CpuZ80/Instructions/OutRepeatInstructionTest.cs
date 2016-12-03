using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.CpuZ80.Opcodes;
using Jacobi.Zim80.Test;
using Jacobi.Zim80.CpuZ80.UnitTests;
using Jacobi.Zim80.Memory.UnitTests;
using Jacobi.Zim80.UnitTests;
using Jacobi.Zim80.Diagnostics;

namespace Jacobi.Zim80.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class OutRepeatInstructionTest
    {
        private const ushort Address = 0x04;
        private const ushort IoAddress = 0x02;
        private const byte Value = 0xAA;

        [TestMethod]
        public void OUTI()
        {
            var ob = OpcodeByte.New(x: 2, z: 3, y: 4);
            var model = ExecuteTest(ob, (cpu) =>
            {
                cpu.Registers.HL = Address;
                cpu.Registers.BC = 0x01 << 8 | IoAddress;

            }, true);

            model.Cpu.AssertRegisters(hl: Address + 1, bc: IoAddress);
            model.IoSpace.Assert(IoAddress, Value);
        }

        [TestMethod]
        public void OUTD()
        {
            var ob = OpcodeByte.New(x: 2, z: 3, y: 5);
            var model = ExecuteTest(ob, (cpu) =>
            {
                cpu.Registers.HL = Address;
                cpu.Registers.BC = 0x01 << 8 | IoAddress;

            }, true);

            model.Cpu.AssertRegisters(hl: Address - 1, bc: IoAddress);
            model.IoSpace.Assert(IoAddress, Value);
        }

        [TestMethod]
        public void OUTIR()
        {
            var ob = OpcodeByte.New(x: 2, z: 3, y: 6);
            var model = ExecuteTest(ob, (cpu) =>
            {
                cpu.Registers.HL = Address;
                cpu.Registers.BC = 0x01 << 8 | IoAddress;

            }, true);

            model.Cpu.AssertRegisters(hl: Address + 1, bc: IoAddress);
            model.IoSpace.Assert(IoAddress, Value);
        }

        [TestMethod]
        public void OUTDR()
        {
            var ob = OpcodeByte.New(x: 2, z: 3, y: 7);
            var model = ExecuteTest(ob, (cpu) =>
            {
                cpu.Registers.HL = Address;
                cpu.Registers.BC = 0x01 << 8 | IoAddress;

            }, true);

            model.Cpu.AssertRegisters(hl: Address - 1, bc: IoAddress);
            model.IoSpace.Assert(IoAddress, Value);
        }

        // TODO: make OUTIR/OUTDR tests that actually repeat.

        private SimulationModel ExecuteTest(OpcodeByte ob, Action<CpuZ80> preTest, bool isConditionMet)
        {
            var cpu = new CpuZ80();
            var model = cpu.Initialize(
                new byte[] { 0xED, ob.Value, 0, 0, Value, 0, 0 },
                new byte[] { 0, 0, 0, 0, 0, 0, 0 });

            cpu.FillRegisters();
            preTest(cpu);

            model.ClockGen.SquareWave(isConditionMet ? 16 : 21);

            Console.WriteLine(model.LogicAnalyzer.ToWaveJson());

            return model;
        }
    }
}
