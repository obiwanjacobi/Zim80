using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.CpuZ80.Opcodes;
using Jacobi.Zim80.CpuZ80.UnitTests;
using Jacobi.Zim80.Test;
using System;
using Jacobi.Zim80.Memory.UnitTests;
using Jacobi.Zim80.UnitTests;
using Jacobi.Zim80.Diagnostics;

namespace Jacobi.Zim80.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class PushInstructionTest
    {
        private const ushort Stack = 0x05;
        private const ushort Value = 0xAA55;
        private const byte ValueMsb = 0xAA;
        private const byte ValueLsb = 0x55;

        [TestMethod]
        public void PushBC()
        {
            var push = OpcodeByte.New(x:3, z:5, p:0);
            var model = ExecuteTest(push, 
                            (m) => m.Cpu.FillRegisters(sp:Stack, bc:Value));

            model.Cpu.AssertRegisters(sp:Stack - 2, bc: Value);
            model.Memory.Assert(Stack - 1, ValueMsb);
            model.Memory.Assert(Stack - 2, ValueLsb);
        }

        [TestMethod]
        public void PushDE()
        {
            var push = OpcodeByte.New(x: 3, z: 5, p: 1);
            var model = ExecuteTest(push,
                            (m) => m.Cpu.FillRegisters(sp: Stack, de: Value));

            model.Cpu.AssertRegisters(sp: Stack - 2, de: Value);
            model.Memory.Assert(Stack - 1, ValueMsb);
            model.Memory.Assert(Stack - 2, ValueLsb);
        }

        [TestMethod]
        public void PushHL()
        {
            var push = OpcodeByte.New(x: 3, z: 5, p: 2);
            var model = ExecuteTest(push,
                            (m) => m.Cpu.FillRegisters(sp: Stack, hl: Value));

            model.Cpu.AssertRegisters(sp: Stack - 2, hl: Value);
            model.Memory.Assert(Stack - 1, ValueMsb);
            model.Memory.Assert(Stack - 2, ValueLsb);
        }

        [TestMethod]
        public void PushAF_NotTestingF()
        {
            var push = OpcodeByte.New(x: 3, z: 5, p: 3);
            var model = ExecuteTest(push,
                            (m) => m.Cpu.FillRegisters(sp: Stack, a: ValueMsb));

            model.Cpu.AssertRegisters(sp: Stack - 2, a: ValueMsb);
            model.Memory.Assert(Stack - 1, ValueMsb);
        }

        [TestMethod]
        public void PushIX()
        {
            var push = OpcodeByte.New(x: 3, z: 5, p: 2);
            var model = ExecuteTest(push,
                            (m) => m.Cpu.FillRegisters(sp: Stack, ix: Value), 0xDD);

            model.Cpu.AssertRegisters(sp: Stack - 2, ix: Value);
            model.Memory.Assert(Stack - 1, ValueMsb);
            model.Memory.Assert(Stack - 2, ValueLsb);
        }

        [TestMethod]
        public void PushIY()
        {
            var push = OpcodeByte.New(x: 3, z: 5, p: 2);
            var model = ExecuteTest(push,
                            (m) => m.Cpu.FillRegisters(sp: Stack, iy: Value), 0xFD);

            model.Cpu.AssertRegisters(sp: Stack - 2, iy: Value);
            model.Memory.Assert(Stack - 1, ValueMsb);
            model.Memory.Assert(Stack - 2, ValueLsb);
        }

        private static SimulationModel ExecuteTest(OpcodeByte push, 
                Action<SimulationModel> preTest, byte extension = 0)
        {
            var cpuZ80 = new CpuZ80();
            var buffer = (extension == 0) ?
                new byte[] { push.Value, 0, 0, 0, 0, 0 } :
                new byte[] { extension, push.Value, 0, 0, 0, 0 };
            var model = cpuZ80.Initialize(buffer);

            preTest(model);

            model.ClockGen.SquareWave(extension == 0 ? 11 : 15);

            Console.WriteLine(model.LogicAnalyzer.ToWaveJson());

            return model;
        }
    }
}
