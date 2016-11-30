using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.CpuZ80.Opcodes;
using Jacobi.Zim80.Test;
using System;
using Jacobi.Zim80.CpuZ80.UnitTests;
using Jacobi.Zim80.Memory.UnitTests;
using Jacobi.Zim80.UnitTests;

namespace Jacobi.Zim80.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class InRepeatInstructionTest
    {
        private const ushort Address = 0x04;
        private const ushort IoAddress = 0x02;
        private const byte Value = 0xAA;

        [TestMethod]
        public void INI()
        {
            var ob = OpcodeByte.New(x: 2, z: 2, y: 4);
            var model = ExecuteTest(ob, (cpu) => 
                {
                    cpu.Registers.HL = Address;
                    cpu.Registers.BC = IoAddress;

                }, true);

            model.Cpu.AssertRegisters(hl: Address + 1, bc: 0xFF02);
            model.Memory.Assert(Address, Value);
        }

        [TestMethod]
        public void IND()
        {
            var ob = OpcodeByte.New(x: 2, z: 2, y: 5);
            var model = ExecuteTest(ob, (cpu) =>
            {
                cpu.Registers.HL = Address;
                cpu.Registers.BC = IoAddress;

            }, true);

            model.Cpu.AssertRegisters(hl: Address - 1, bc: 0xFF02);
            model.Memory.Assert(Address, Value);
        }

        [TestMethod]
        public void INIR()
        {
            var ob = OpcodeByte.New(x: 2, z: 2, y: 6);
            var model = ExecuteTest(ob, (cpu) =>
            {
                cpu.Registers.HL = Address;
                cpu.Registers.BC = IoAddress;

            }, true);

            model.Cpu.AssertRegisters(hl: Address + 1, bc: 0xFF02);
            model.Memory.Assert(Address, Value);
        }

        [TestMethod]
        public void INDR()
        {
            var ob = OpcodeByte.New(x: 2, z: 2, y: 7);
            var model = ExecuteTest(ob, (cpu) =>
            {
                cpu.Registers.HL = Address;
                cpu.Registers.BC = IoAddress;

            }, true);

            model.Cpu.AssertRegisters(hl: Address - 1, bc: 0xFF02);
            model.Memory.Assert(Address, Value);
        }

        // TODO: make INIR/INDR tests that actually repeat.

        private SimulationModel ExecuteTest(OpcodeByte ob, Action<CpuZ80> preTest, bool isConditionMet)
        {
            var cpu = new CpuZ80();
            var model = cpu.Initialize(
                new byte[] { 0xED, ob.Value, 0, 0, 0, 0, 0 }, 
                new byte[] { 0, 0, Value, 0, 0, 0, 0 });

            cpu.FillRegisters();
            preTest(cpu);

            model.ClockGen.SquareWave(isConditionMet ? 16 : 21);

            Console.WriteLine(model.LogicAnalyzer.ToWaveJson());

            return model;
        }
    }
}
