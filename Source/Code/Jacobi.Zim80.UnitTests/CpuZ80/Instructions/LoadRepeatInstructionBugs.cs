using FluentAssertions;
using Jacobi.Zim80.CpuZ80.Opcodes;
using Jacobi.Zim80.CpuZ80.UnitTests;
using Jacobi.Zim80.Diagnostics;
using Jacobi.Zim80.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Jacobi.Zim80.UnitTests.CpuZ80.Instructions
{
    [TestClass]
    public class LoadRepeatInstructionBugs
    {
        private const ushort RdAddress = 0x04;
        private const ushort WrAddress = 0x08;
        private const byte Value1 = 0x55;
        private const byte Value2 = 0xAA;
        private const byte Length = 0x04;

        [TestMethod]
        [TestCategory("Bug")]
        public void LDIR_WhenConditionIsMet_ExitRepeat()
        {
            // solved by adding altCycles in opcode defs.
            var ob = OpcodeByte.New(x: 2, z: 0, y: 6);
            int count = 0;

            var model = ExecuteTest(ob, (cpu) => {
                cpu.Registers.HL = RdAddress;
                cpu.Registers.DE = WrAddress;
                cpu.Registers.BC = Length;
                cpu.InstructionExecuted += (s, e) => {
                    count++;
                };
                // 3 LDIR repeats + last & nop
                return 3 * 21 + 16 + 4;
            });

            count.Should().Be(5);
            model.Cpu.AssertRegisters(bc: 0, de: WrAddress + Length, hl: RdAddress + Length);
            model.Cpu.Registers.Flags.PV.Should().Be(false);
        }

        private SimulationModel ExecuteTest(OpcodeByte ob, Func<Zim80.CpuZ80.CpuZ80, int> preTest)
        {
            var builder = new SimulationModelBuilder();
            builder.AddCpuClockGen();
            builder.AddCpuMemory();
            builder.AddLogicAnalyzer();

            builder.Model.Memory.Write(
                new byte[] { 0xED, ob.Value, 0, 0, Value1, Value2, Value2, Value1, 0, 0, 0, 0, 0, 0, 0, 0 }
                );

            builder.Model.Cpu.FillRegisters();
            int cycles = preTest(builder.Model.Cpu);

            builder.Model.ClockGen.SquareWave(cycles);

            Console.WriteLine(builder.Model.LogicAnalyzer.ToWaveJson());

            return builder.Model;
        }
    }
}
