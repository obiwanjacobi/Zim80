using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.CpuZ80.Opcodes;
using Jacobi.Zim80.Test;
using Jacobi.Zim80.CpuZ80.UnitTests;
using Jacobi.Zim80.Memory.UnitTests;
using System;
using Jacobi.Zim80.UnitTests;

namespace Jacobi.Zim80.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class OutInstructionTest
    {
        private const byte IoAddress = 0x02;

        [TestMethod]
        public void Out_n_A()
        {
            var ob = OpcodeByte.New(x: 3, z: 3, y: 2);
            var model = ExecuteTest(ob, (cpu) => cpu.Registers.A = 0);

            model.Cpu.AssertRegisters(a: 0);
            model.IoSpace.Assert(IoAddress, 0);
        }

        [TestMethod]
        public void Out_C_B()
        {
            var ob = OpcodeByte.New(x: 1, z: 1, y: 0);
            var model = ExecuteTest(ob, (cpu) => cpu.Registers.BC = IoAddress, 0xED);

            model.Cpu.AssertRegisters(bc: IoAddress);
            model.IoSpace.Assert(IoAddress, 0);
        }

        [TestMethod]
        public void Out_C_C()
        {
            var ob = OpcodeByte.New(x: 1, z: 1, y: 1);
            var model = ExecuteTest(ob, (cpu) => cpu.Registers.BC = IoAddress, 0xED);

            model.Cpu.AssertRegisters(bc: IoAddress);
            model.IoSpace.Assert(IoAddress, 0x02);
        }

        [TestMethod]
        public void Out_C_D()
        {
            var ob = OpcodeByte.New(x: 1, z: 1, y: 2);
            var model = ExecuteTest(ob, (cpu) =>
                {
                    cpu.Registers.BC = IoAddress;
                    cpu.Registers.DE = 0xAA55;
                }, 0xED);

            model.Cpu.AssertRegisters(bc: IoAddress, de: 0xAA55);
            model.IoSpace.Assert(IoAddress, 0xAA);
        }

        [TestMethod]
        public void Out_C_E()
        {
            var ob = OpcodeByte.New(x: 1, z: 1, y: 3);
            var model = ExecuteTest(ob, (cpu) =>
            {
                cpu.Registers.BC = IoAddress;
                cpu.Registers.DE = 0xAA55;
            }, 0xED);

            model.Cpu.AssertRegisters(bc: IoAddress, de: 0xAA55);
            model.IoSpace.Assert(IoAddress, 0x55);
        }

        [TestMethod]
        public void Out_C_H()
        {
            var ob = OpcodeByte.New(x: 1, z: 1, y: 4);
            var model = ExecuteTest(ob, (cpu) =>
            {
                cpu.Registers.BC = IoAddress;
                cpu.Registers.HL = 0xAA55;
            }, 0xED);

            model.Cpu.AssertRegisters(bc: IoAddress, hl: 0xAA55);
            model.IoSpace.Assert(IoAddress, 0xAA);
        }

        [TestMethod]
        public void Out_C_L()
        {
            var ob = OpcodeByte.New(x: 1, z: 1, y: 5);
            var model = ExecuteTest(ob, (cpu) =>
            {
                cpu.Registers.BC = IoAddress;
                cpu.Registers.HL = 0xAA55;
            }, 0xED);

            model.Cpu.AssertRegisters(bc: IoAddress, hl: 0xAA55);
            model.IoSpace.Assert(IoAddress, 0x55);
        }

        [TestMethod]
        public void Out_C_0()
        {
            var ob = OpcodeByte.New(x: 1, z: 1, y: 6);
            var model = ExecuteTest(ob, (cpu) =>
            {
                cpu.Registers.BC = IoAddress;
            }, 0xED);

            model.Cpu.AssertRegisters(bc: IoAddress);
            model.IoSpace.Assert(IoAddress, 0);
        }

        [TestMethod]
        public void Out_C_A()
        {
            var ob = OpcodeByte.New(x: 1, z: 1, y: 7);
            var model = ExecuteTest(ob, (cpu) =>
            {
                cpu.Registers.BC = IoAddress;
                cpu.Registers.A = 0xAA;
            }, 0xED);

            model.Cpu.AssertRegisters(bc: IoAddress, a: 0xAA);
            model.IoSpace.Assert(IoAddress, 0xAA);
        }

        private SimulationModel ExecuteTest(OpcodeByte ob, Action<CpuZ80> preTest, byte extension = 0)
        {
            var cpu = new CpuZ80();

            var buffer = extension == 0 ?
                new byte[] { ob.Value, IoAddress } :
                new byte[] { extension, ob.Value };

            var model = cpu.Initialize(buffer, new byte[] { 0, 0, 0, 0, 0 });

            cpu.FillRegisters();
            preTest(cpu);

            model.ClockGen.SquareWave(extension == 0 ? 11 : 12);

            Console.WriteLine(model.LogicAnalyzer.ToWaveJson());

            return model;
        }
    }
}
