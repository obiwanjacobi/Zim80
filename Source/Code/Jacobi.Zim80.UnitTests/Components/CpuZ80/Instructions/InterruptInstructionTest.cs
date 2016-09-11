using FluentAssertions;
using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using Jacobi.Zim80.Components.CpuZ80.UnitTests;
using Jacobi.Zim80.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jacobi.Zim80.Components.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class InterruptInstructionTest
    {
        [TestMethod]
        public void DI()
        {
            var ob = OpcodeByte.New(x: 3, z: 3, y: 6);
            var model = ExecuteTest(ob);

            model.Cpu.Registers.Interrupt.IFF1.Should().BeFalse("IFF1");
            model.Cpu.Registers.Interrupt.IFF2.Should().BeFalse("IFF2");
            model.Cpu.Registers.Interrupt.IsEnabled.Should().BeFalse("IsEnabled");
            model.Cpu.Registers.Interrupt.IsSuspended.Should().BeFalse("IsSuspended");
        }

        [TestMethod]
        public void EI()
        {
            var ob = OpcodeByte.New(x: 3, z: 3, y: 7);
            var model = ExecuteTest(ob);

            model.Cpu.Registers.Interrupt.IFF1.Should().BeTrue("IFF1");
            model.Cpu.Registers.Interrupt.IFF2.Should().BeTrue("IFF2");
            model.Cpu.Registers.Interrupt.IsEnabled.Should().BeFalse("IsEnabled");
            model.Cpu.Registers.Interrupt.IsSuspended.Should().BeTrue("IsSuspended");
        }

        private SimulationModel ExecuteTest(OpcodeByte ob)
        {
            var cpu = new CpuZ80();
            var model = cpu.Initialize(new byte[] { ob.Value });
            cpu.FillRegisters();

            model.ClockGen.BlockWave(4);

            cpu.AssertRegisters();

            return model;
        }
    }
}
