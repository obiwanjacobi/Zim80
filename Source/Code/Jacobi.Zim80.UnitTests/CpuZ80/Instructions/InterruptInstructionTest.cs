using FluentAssertions;
using Jacobi.Zim80.CpuZ80.Opcodes;
using Jacobi.Zim80.CpuZ80.UnitTests;
using Jacobi.Zim80.Diagnostics;
using Jacobi.Zim80.Test;
using Jacobi.Zim80.UnitTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Jacobi.Zim80.CpuZ80.Instructions.UnitTests
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

            model.ClockGen.SquareWave(4);

            Console.WriteLine(model.LogicAnalyzer.ToWaveJson());

            cpu.AssertRegisters();

            return model;
        }
    }
}
