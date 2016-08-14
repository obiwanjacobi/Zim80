using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using Jacobi.Zim80.Components.CpuZ80.UnitTests;
using Jacobi.Zim80.Model;
using Jacobi.Zim80.Components.Memory;
using FluentAssertions;

namespace Jacobi.Zim80.Components.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class WriteIndirectRegisterInstructionTest
    {
        private const UInt16 Address = 0x0001;
        private const byte ExpectedValue = 0xAA;

        [TestMethod]
        public void LdBC_A()
        {
            var ob = OpcodeByte.New(z: 2, q: 0, p: 0);

            var model = ExecuteTest(ob, bc: Address);

            var memory = (IDirectMemoryAccess<BusData8>)model.Memory;
            memory[Address].ToByte().Should().Be(ExpectedValue);
        }

        [TestMethod]
        public void LdDE_A()
        {
            var ob = OpcodeByte.New(z: 2, q: 0, p: 1);

            var model = ExecuteTest(ob, de: Address);

            var memory = (IDirectMemoryAccess<BusData8>)model.Memory;
            memory[Address].ToByte().Should().Be(ExpectedValue);
        }

        private static SimulationModel ExecuteTest(OpcodeByte ob,
            UInt16 bc = CpuZ80TestExtensions.MagicValue, 
            UInt16 de = CpuZ80TestExtensions.MagicValue)
        {
            var cpuZ80 = new CpuZ80();
            var model = cpuZ80.Initialize(new byte[] { ob.Value, 0 });

            cpuZ80.FillRegisters(a: 0xAA, bc: bc, de: de);

            model.ClockGen.BlockWave(7);

            return model;
        }
    }
}
