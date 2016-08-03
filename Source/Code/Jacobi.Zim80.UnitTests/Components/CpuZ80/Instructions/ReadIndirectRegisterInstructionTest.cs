using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using Jacobi.Zim80.Components.CpuZ80.UnitTests;

namespace Jacobi.Zim80.Components.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class ReadIndirectRegisterInstructionTest
    {
        private UInt16 Address = 0x0001;

        [TestMethod]
        public void LdA_BC()
        {
            var ob = OpcodeByte.New(z: 2, q: 1, p: 0);

            var cpu = ExecuteTest(ob, bc: Address);


        }

        [TestMethod]
        public void LdA_DE()
        {
            var ob = OpcodeByte.New(z: 2, q: 1, p: 1);
        }

        private static CpuZ80 ExecuteTest(OpcodeByte ob, 
            UInt16 bc = CpuZ80TestExtensions.MagicValue, 
            UInt16 de = CpuZ80TestExtensions.MagicValue)
        {
            var cpuZ80 = new CpuZ80();
            var model = cpuZ80.Initialize(new byte[] { ob.Value, 00 });

            cpuZ80.FillRegisters();

            model.ClockGen.BlockWave(7);

            return cpuZ80;
        }
    }
}
