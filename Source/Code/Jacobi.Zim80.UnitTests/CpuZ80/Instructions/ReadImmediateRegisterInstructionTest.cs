using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.CpuZ80.Opcodes;
using Jacobi.Zim80.CpuZ80.UnitTests;
using System;
using Jacobi.Zim80.UnitTests;
using Jacobi.Zim80.Diagnostics;

namespace Jacobi.Zim80.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class ReadImmediateRegisterInstructionTest
    {
        [TestMethod]
        public void LdA_nn_()
        {
            var ob = OpcodeByte.New(z: 2, q: 1, p: 3);
            var cpu = ExecuteTest(ob);

            cpu.AssertRegisters(a: 0xAA);
        }

        [TestMethod]
        public void LdHL_nn_()
        {
            var ob = OpcodeByte.New(z: 2, q: 1, p: 2);
            var cpu = ExecuteTest(ob);

            cpu.AssertRegisters(hl: 0x55AA);
        }

        [TestMethod]
        public void LdIX_nn_()
        {
            var ob = OpcodeByte.New(z: 2, q: 1, p: 2);
            var cpu = ExecuteTest(ob, 0xDD);

            cpu.AssertRegisters(ix: 0x55AA);
        }

        [TestMethod]
        public void LdIY_nn_()
        {
            var ob = OpcodeByte.New(z: 2, q: 1, p: 2);
            var cpu = ExecuteTest(ob, 0xFD);

            cpu.AssertRegisters(iy: 0x55AA);
        }

        private static CpuZ80 ExecuteTest(OpcodeByte ob, byte extension = 0)
        {
            var regA = ob.Q == 3;

            var cpuZ80 = new CpuZ80();
            byte[] buffer = (extension == 0) ?
                new byte[] { ob.Value, 5, 0, 0, 0, 0xAA, 0x55 } :
                new byte[] { extension, ob.Value, 5, 0, 0, 0xAA, 0x55 };

            var model = cpuZ80.Initialize(buffer);

            cpuZ80.FillRegisters();

            model.ClockGen.SquareWave(extension == 0 ? regA ? 13 : 16 : 20);

            Console.WriteLine(model.LogicAnalyzer.ToWaveJson());

            return cpuZ80;
        }
    }
}
