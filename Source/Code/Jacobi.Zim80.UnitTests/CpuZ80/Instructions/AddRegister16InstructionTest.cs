using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.CpuZ80.Opcodes;
using Jacobi.Zim80.CpuZ80.UnitTests;
using FluentAssertions;
using Jacobi.Zim80.UnitTests;
using Jacobi.Zim80.Diagnostics;

namespace Jacobi.Zim80.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class AddRegister16InstructionTest
    {
        private const ushort Value = 0xAA55;
        private const ushort ExpectedValue = CpuZ80TestExtensions.MagicValue + Value;
        private const ushort DoubleValue = (Value + Value) & 0xFFFF;

        [TestMethod]
        public void AddHL_BC()
        {
            var ob = OpcodeByte.New(z: 1, q: 1, p: 0);

            var cpuZ80 = ExecuteTest(ob, (cpu) => cpu.FillRegisters(bc: Value));

            cpuZ80.AssertRegisters(hl: ExpectedValue, bc: Value);
            cpuZ80.Registers.Flags.C.Should().BeFalse();
        }

        [TestMethod]
        public void AddHL_DE()
        {
            var ob = OpcodeByte.New(z: 1, q: 1, p: 1);

            var cpuZ80 = ExecuteTest(ob, (cpu) => cpu.FillRegisters(de: Value));

            cpuZ80.AssertRegisters(hl: ExpectedValue, de: Value);
            cpuZ80.Registers.Flags.C.Should().BeFalse();
        }

        [TestMethod]
        public void AddHL_HL()
        {
            var ob = OpcodeByte.New(z: 1, q: 1, p: 2);

            var cpuZ80 = ExecuteTest(ob, (cpu) => cpu.FillRegisters(hl: Value));

            cpuZ80.AssertRegisters(hl: DoubleValue);
            cpuZ80.Registers.Flags.C.Should().BeTrue();
        }

        [TestMethod]
        public void AddHL_SP()
        {
            var ob = OpcodeByte.New(z: 1, q: 1, p: 3);

            var cpuZ80 = ExecuteTest(ob, (cpu) => cpu.FillRegisters(sp: Value));

            cpuZ80.AssertRegisters(hl: ExpectedValue, sp: Value);
            cpuZ80.Registers.Flags.C.Should().BeFalse();
        }

        [TestMethod]
        public void AddIX_BC()
        {
            var ob = OpcodeByte.New(z: 1, q: 1, p: 0);

            var cpuZ80 = ExecuteTest(ob, (cpu) => cpu.FillRegisters(bc: Value), extension: 0xDD);

            cpuZ80.AssertRegisters(ix: ExpectedValue, bc: Value);
            cpuZ80.Registers.Flags.C.Should().BeFalse();
        }

        [TestMethod]
        public void AddIX_DE()
        {
            var ob = OpcodeByte.New(z: 1, q: 1, p: 1);

            var cpuZ80 = ExecuteTest(ob, (cpu) => cpu.FillRegisters(de: Value), extension: 0xDD);

            cpuZ80.AssertRegisters(ix: ExpectedValue, de: Value);
            cpuZ80.Registers.Flags.C.Should().BeFalse();
        }

        [TestMethod]
        public void AddIX_IX()
        {
            var ob = OpcodeByte.New(z: 1, q: 1, p: 2);

            var cpuZ80 = ExecuteTest(ob, (cpu) => cpu.FillRegisters(ix: Value), extension: 0xDD);

            cpuZ80.AssertRegisters(ix: DoubleValue);
        }

        [TestMethod]
        public void AddIX_SP()
        {
            var ob = OpcodeByte.New(z: 1, q: 1, p: 3);

            var cpuZ80 = ExecuteTest(ob, (cpu) => cpu.FillRegisters(sp: Value), extension: 0xDD);

            cpuZ80.AssertRegisters(ix: ExpectedValue, sp: Value);
            cpuZ80.Registers.Flags.C.Should().BeFalse();
        }

        [TestMethod]
        public void AddIY_BC()
        {
            var ob = OpcodeByte.New(z: 1, q: 1, p: 0);

            var cpuZ80 = ExecuteTest(ob, (cpu) => cpu.FillRegisters(bc: Value), extension: 0xFD);

            cpuZ80.AssertRegisters(iy: ExpectedValue, bc: Value);
            cpuZ80.Registers.Flags.C.Should().BeFalse();
        }

        [TestMethod]
        public void AddIY_DE()
        {
            var ob = OpcodeByte.New(z: 1, q: 1, p: 1);

            var cpuZ80 = ExecuteTest(ob, (cpu) => cpu.FillRegisters(de: Value), extension: 0xFD);

            cpuZ80.AssertRegisters(iy: ExpectedValue, de: Value);
            cpuZ80.Registers.Flags.C.Should().BeFalse();
        }

        [TestMethod]
        public void AddIY_IY()
        {
            var ob = OpcodeByte.New(z: 1, q: 1, p: 2);

            var cpuZ80 = ExecuteTest(ob, (cpu) => cpu.FillRegisters(iy: Value), extension: 0xFD);

            cpuZ80.AssertRegisters(iy: DoubleValue);
        }

        [TestMethod]
        public void AddIY_SP()
        {
            var ob = OpcodeByte.New(z: 1, q: 1, p: 3);

            var cpuZ80 = ExecuteTest(ob, (cpu) => cpu.FillRegisters(sp: Value), extension: 0xFD);

            cpuZ80.AssertRegisters(iy: ExpectedValue, sp: Value);
            cpuZ80.Registers.Flags.C.Should().BeFalse();
        }

        [TestMethod]
        public void AddCarryHL_BC_nc()
        {
            var ob = OpcodeByte.New(x: 1, z: 2, q: 1, p: 0);

            var cpuZ80 = ExecuteTest(ob, (cpu) => cpu.FillRegisters(bc: Value), extension: 0xED);

            cpuZ80.AssertRegisters(hl: ExpectedValue, bc: Value);
            cpuZ80.Registers.Flags.C.Should().BeFalse();
        }

        [TestMethod]
        public void AddCarryHL_DE_nc()
        {
            var ob = OpcodeByte.New(x: 1, z: 2, q: 1, p: 1);

            var cpuZ80 = ExecuteTest(ob, (cpu) => cpu.FillRegisters(de: Value), extension: 0xED);

            cpuZ80.AssertRegisters(hl: ExpectedValue, de: Value);
            cpuZ80.Registers.Flags.C.Should().BeFalse();
        }

        [TestMethod]
        public void AddCarryHL_HL_nc()
        {
            var ob = OpcodeByte.New(x: 1, z: 2, q: 1, p: 2);

            var cpuZ80 = ExecuteTest(ob, (cpu) => cpu.FillRegisters(hl: Value), extension: 0xED);

            cpuZ80.AssertRegisters(hl: DoubleValue);
            cpuZ80.Registers.Flags.C.Should().BeTrue();
        }

        [TestMethod]
        public void AddCarryHL_SP_nc()
        {
            var ob = OpcodeByte.New(x: 1, z: 2, q: 1, p: 3);

            var cpuZ80 = ExecuteTest(ob, (cpu) => cpu.FillRegisters(sp: Value), extension: 0xED);

            cpuZ80.AssertRegisters(hl: ExpectedValue, sp: Value);
            cpuZ80.Registers.Flags.C.Should().BeFalse();

        }

        [TestMethod]
        public void AddCarryHL_BC_c()
        {
            var ob = OpcodeByte.New(x: 1, z: 2, q: 1, p: 0);

            var cpuZ80 = ExecuteTest(ob, (cpu) => 
                {
                    cpu.FillRegisters(bc: Value);
                    cpu.Registers.Flags.C = true;
                }, extension: 0xED);

            cpuZ80.AssertRegisters(hl: ExpectedValue + 1, bc: Value);
            cpuZ80.Registers.Flags.C.Should().BeFalse();
        }

        [TestMethod]
        public void AddCarryHL_DE_c()
        {
            var ob = OpcodeByte.New(x: 1, z: 2, q: 1, p: 1);

            var cpuZ80 = ExecuteTest(ob, (cpu) => 
            {
                cpu.FillRegisters(de: Value);
                cpu.Registers.Flags.C = true;
            }, extension: 0xED);

            cpuZ80.AssertRegisters(hl: ExpectedValue + 1, de: Value);
            cpuZ80.Registers.Flags.C.Should().BeFalse();
        }

        [TestMethod]
        public void AddCarryHL_HL_c()
        {
            var ob = OpcodeByte.New(x: 1, z: 2, q: 1, p: 2);

            var cpuZ80 = ExecuteTest(ob, (cpu) => 
            {
                cpu.FillRegisters(hl: Value);
                cpu.Registers.Flags.C = true;
            }, extension: 0xED);

            cpuZ80.AssertRegisters(hl: DoubleValue + 1);
            cpuZ80.Registers.Flags.C.Should().BeTrue();
        }

        [TestMethod]
        public void AddCarryHL_SP_c()
        {
            var ob = OpcodeByte.New(x: 1, z: 2, q: 1, p: 3);

            var cpuZ80 = ExecuteTest(ob, (cpu) => 
            {
                cpu.FillRegisters(sp: Value);
                cpu.Registers.Flags.C = true;
            }, extension: 0xED);

            cpuZ80.AssertRegisters(hl: ExpectedValue + 1, sp: Value);
            cpuZ80.Registers.Flags.C.Should().BeFalse();

        }

        private static CpuZ80 ExecuteTest(OpcodeByte ob, 
            Action<CpuZ80> preTest, byte extension = 0)
        {
            var cpuZ80 = new CpuZ80();
            byte[] buffer = (extension == 0) ?
                    new byte[] { ob.Value } :
                    new byte[] { extension, ob.Value };

            var model = cpuZ80.Initialize(buffer);

            preTest(cpuZ80);

            model.ClockGen.SquareWave(extension == 0 ? 11 : 15);

            Console.WriteLine(model.LogicAnalyzer.ToWaveJson());

            return cpuZ80;
        }
    }
}
