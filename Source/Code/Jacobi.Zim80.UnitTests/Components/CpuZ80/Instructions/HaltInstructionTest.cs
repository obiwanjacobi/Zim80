using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using Jacobi.Zim80.Components.CpuZ80.UnitTests;
using Jacobi.Zim80.Model;
using Jacobi.Zim80.UnitTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jacobi.Zim80.Components.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class HaltInstructionTest
    {
        // Note: Requires interrupts are working
        [TestMethod]
        public void Halt_IntInOpcode()
        {
            var model = CreateModel();
            var intProvider = model.Cpu.Interrupt.CreateConnection();

            // act
            model.ClockGen.SquareWave(2);
            intProvider.Write(DigitalLevel.Low);
            model.ClockGen.SquareWave(2);

            model.Cpu.AssertRegisters(pc: 1);
        }

        [TestMethod]
        public void Halt_IntAfterOpcode()
        {
            var model = CreateModel();
            var intProvider = model.Cpu.Interrupt.CreateConnection();

            // act
            model.ClockGen.SquareWave(6);
            intProvider.Write(DigitalLevel.Low);
            model.ClockGen.SquareWave(2);

            model.Cpu.AssertRegisters(pc: 1);
        }

        [TestMethod]
        public void Halt_IntWayAfterOpcode()
        {
            var model = CreateModel();
            var intProvider = model.Cpu.Interrupt.CreateConnection();

            var x = 9999;
            // act
            model.ClockGen.SquareWave(x*4 + 2);
            intProvider.Write(DigitalLevel.Low);
            model.ClockGen.SquareWave(2);

            model.Cpu.AssertRegisters(pc: 1);
        }

        private static SimulationModel CreateModel()
        {
            var ob = OpcodeByte.New(x: 1, z: 6, y: 6);
            var cpuZ80 = new CpuZ80();
            var model = cpuZ80.Initialize(new byte[] { ob.Value });

            cpuZ80.FillRegisters();

            return model;
        }
    }
}
