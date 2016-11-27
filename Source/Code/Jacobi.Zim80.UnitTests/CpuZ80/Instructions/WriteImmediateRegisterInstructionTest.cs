using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.CpuZ80.Opcodes;
using Jacobi.Zim80.CpuZ80.UnitTests;
using Jacobi.Zim80.Test;
using Jacobi.Zim80.Memory.UnitTests;

namespace Jacobi.Zim80.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class WriteImmediateRegisterInstructionTest
    {
        private const byte AddressLo = 5;
        private const byte AddressHi = 6;

        private const byte ValueLo = 0x55;
        private const byte ValueHi = 0xAA;

        [TestMethod]
        public void Ld_nn_A()
        {
            var ob = OpcodeByte.New(z: 2, q: 0, p: 3);
            var model = ExecuteTest(ob, (cpu) => cpu.Registers.A = ValueLo);

            model.Cpu.AssertRegisters(a: ValueLo);
            model.Memory.Assert(AddressLo, ValueLo);
        }

        [TestMethod]
        public void Ld_nn_HL()
        {
            var ob = OpcodeByte.New(z: 2, q: 0, p: 2);
            var model = ExecuteTest(ob, (cpu) =>
                {
                    cpu.Registers.L = ValueLo;
                    cpu.Registers.H = ValueHi;
                });

            model.Cpu.AssertRegisters(hl: (ValueHi << 8) | ValueLo);
            model.Memory.Assert(AddressLo, ValueLo);
            model.Memory.Assert(AddressHi, ValueHi);
        }

        [TestMethod]
        public void Ld_nn_IX()
        {
            var ob = OpcodeByte.New(z: 2, q: 0, p: 2);
            var model = ExecuteTest(ob, (cpu) =>
            {
                cpu.Registers.GetIX().SetLo(ValueLo);
                cpu.Registers.GetIX().SetHi(ValueHi);
            }, 0xDD);

            model.Cpu.AssertRegisters(ix: (ValueHi << 8) | ValueLo);
            model.Memory.Assert(AddressLo, ValueLo);
            model.Memory.Assert(AddressHi, ValueHi);
        }

        [TestMethod]
        public void Ld_nn_IY()
        {
            var ob = OpcodeByte.New(z: 2, q: 0, p: 2);
            var model = ExecuteTest(ob, (cpu) =>
            {
                cpu.Registers.GetIY().SetLo(ValueLo);
                cpu.Registers.GetIY().SetHi(ValueHi);
            }, 0xFD);

            model.Cpu.AssertRegisters(iy: (ValueHi << 8) | ValueLo);
            model.Memory.Assert(AddressLo, ValueLo);
            model.Memory.Assert(AddressHi, ValueHi);
        }

        private static SimulationModel ExecuteTest(OpcodeByte ob, Action<CpuZ80> preTest, byte extension = 0)
        {
            var regA = ob.Q == 3;

            var cpuZ80 = new CpuZ80();
            byte[] buffer;

            if (extension == 0)
                buffer = new byte[] { ob.Value, AddressLo, 0, 0, 0, 0, 0 };
            else
                buffer = new byte[] { extension, ob.Value, AddressLo, 0, 0, 0, 0 };

            var model = cpuZ80.Initialize(buffer);

            cpuZ80.FillRegisters();
            preTest(cpuZ80);

            model.ClockGen.SquareWave(extension == 0 ? regA ? 13 : 16 : 20);

            return model;
        }
    }
}
