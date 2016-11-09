using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using Jacobi.Zim80.Components.CpuZ80.UnitTests;
using Jacobi.Zim80.Components.Memory.UnitTests;
using Jacobi.Zim80.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jacobi.Zim80.Components.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class ExchangeIndirectInstructionTest
    {
        private const byte Stack = 0x04;
        private const ushort Expected = 0x55AA;
        private const byte ExpectedLo = 0xAA;
        private const byte ExpectedHi = 0x55;

        [TestMethod]
        public void Ex_SP_HL()
        {
            var ob = OpcodeByte.New(x: 3, z: 3, y: 4);

            var model = ExecuteTest(ob);
            model.Cpu.AssertRegisters(hl: 0);
            model.Memory.Assert(Stack, ExpectedLo);
            model.Memory.Assert(Stack + 1, ExpectedHi);
        }

        [TestMethod]
        public void Ex_SP_IX()
        {
            var ob = OpcodeByte.New(x: 3, z: 3, y: 4);

            var model = ExecuteTest(ob, 0xDD);
            model.Memory.Assert(Stack, ExpectedLo);
            model.Memory.Assert(Stack + 1, ExpectedHi);
        }

        [TestMethod]
        public void Ex_SP_IY()
        {
            var ob = OpcodeByte.New(x: 3, z: 3, y: 4);

            var model = ExecuteTest(ob, 0xFD);
            model.Memory.Assert(Stack, ExpectedLo);
            model.Memory.Assert(Stack + 1, ExpectedHi);
        }

        private SimulationModel ExecuteTest(OpcodeByte ob, byte extension = 0)
        {
            var cpuZ80 = new CpuZ80();
            var buffer = extension == 0 ? 
                new byte[] { ob.Value, 0, 0, 0, 0, 0 } :
                new byte[] { extension, ob.Value, 0, 0, 0, 0 };
            var model = cpuZ80.Initialize(buffer);

            cpuZ80.FillRegisters(sp: Stack);

            if (extension == 0xDD)
                cpuZ80.Registers.IX = Expected;
            else if (extension == 0xFD)
                cpuZ80.Registers.IY = Expected;
            else
                cpuZ80.Registers.HL = Expected;

            model.ClockGen.BlockWave(extension == 0 ? 19 : 23);

            return model;
        }
    }
}
