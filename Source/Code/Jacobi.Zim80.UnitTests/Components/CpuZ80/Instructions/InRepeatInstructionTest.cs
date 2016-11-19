using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using Jacobi.Zim80.Model;
using System;
using Jacobi.Zim80.Components.CpuZ80.UnitTests;
using Jacobi.Zim80.Components.Memory.UnitTests;

namespace Jacobi.Zim80.Components.CpuZ80.Instructions.UnitTests
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

            model.Cpu.AssertRegisters(hl: Address + 1, bc: 0xFF);
            model.Memory.Assert(Address, Value);
        }

        private SimulationModel ExecuteTest(OpcodeByte ob, Action<CpuZ80> preTest, bool isConditionMet)
        {
            var cpu = new CpuZ80();
            var model = cpu.Initialize(
                new byte[] { 0xED, ob.Value, 0, 0, 0, 0, 0 });//, 
                //new byte[] { 0, 0, Value, 0, 0, 0, 0 });

            cpu.FillRegisters();
            preTest(cpu);

            model.ClockGen.BlockWave(isConditionMet ? 16 : 21);

            return model;
        }
    }
}
