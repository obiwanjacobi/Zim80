using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using Jacobi.Zim80.Model;
using Jacobi.Zim80.Components.CpuZ80.UnitTests;
using FluentAssertions;

namespace Jacobi.Zim80.Components.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class IntModeInstructionTest
    {
        [TestMethod]
        public void IntMode0_IsDefault()
        {
            var cpu = new CpuZ80();
            cpu.Registers.Interrupt.InterruptMode.Should().Be(InterruptModes.InterruptMode0);
        }

        [TestMethod]
        public void IntMode0()
        {
            var ob = OpcodeByte.New(x: 1, z: 6, y: 0);
            var model = ExecuteTest(ob);

            model.Cpu.Registers.Interrupt.InterruptMode.Should().Be(InterruptModes.InterruptMode0);
        }

        [TestMethod]
        public void IntMode1()
        {
            var ob = OpcodeByte.New(x: 1, z: 6, y: 2);
            var model = ExecuteTest(ob);

            model.Cpu.Registers.Interrupt.InterruptMode.Should().Be(InterruptModes.InterruptMode1);
        }

        [TestMethod]
        public void IntMode2()
        {
            var ob = OpcodeByte.New(x: 1, z: 6, y: 3);
            var model = ExecuteTest(ob);

            model.Cpu.Registers.Interrupt.InterruptMode.Should().Be(InterruptModes.InterruptMode2);
        }

        [TestMethod]
        public void IntMode0alt()
        {
            var ob = OpcodeByte.New(x: 1, z: 6, y: 4);
            var model = ExecuteTest(ob);

            model.Cpu.Registers.Interrupt.InterruptMode.Should().Be(InterruptModes.InterruptMode0);
        }

        [TestMethod]
        public void IntMode1alt()
        {
            var ob = OpcodeByte.New(x: 1, z: 6, y: 6);
            var model = ExecuteTest(ob);

            model.Cpu.Registers.Interrupt.InterruptMode.Should().Be(InterruptModes.InterruptMode1);
        }

        [TestMethod]
        public void IntMode2alt()
        {
            var ob = OpcodeByte.New(x: 1, z: 6, y: 7);
            var model = ExecuteTest(ob);

            model.Cpu.Registers.Interrupt.InterruptMode.Should().Be(InterruptModes.InterruptMode2);
        }

        private SimulationModel ExecuteTest(OpcodeByte ob)
        {
            var cpu = new CpuZ80();
            var model = cpu.Initialize(new byte[] { 0xED, ob.Value });
            cpu.FillRegisters();

            model.ClockGen.BlockWave(8);

            cpu.AssertRegisters();

            return model;
        }
    }
}
