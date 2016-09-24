using Jacobi.Zim80.Components.Memory.UnitTests;
using Jacobi.Zim80.Components.UnitTests;
using Jacobi.Zim80.UnitTests;
using Jacobi.Zim80.Model;
using Jacobi.Zim80.Components.Generators;
using FluentAssertions;
using System;

namespace Jacobi.Zim80.Components.CpuZ80.UnitTests
{
    internal static class CpuZ80TestExtensions
    {
        public static DigitalSignalProvider Clock(this CpuZ80 cpu, 
            CycleNames toCycle, DigitalLevel toLevel)
        {
            var gen = new SignalGenerator();
            var clock = cpu.Clock.CreateConnection(gen.Output);

            gen.BlockWave(1, toCycle, toLevel);

            return clock;
        }

        public static SimulationModel Initialize(this CpuZ80 cpu, byte[] program)
        {
            new InstructionLogger(cpu);

            var model = new SimulationModel();
            model.Cpu = cpu;
            model.Memory = MemoryTestExtensions.NewRam(program);
            model.ClockGen = new SignalGenerator();
            model.ClockGen.Output.CreateConnection(model.Cpu.Clock);
            model.Cpu.MemoryRequest.CreateConnection(model.Memory.ChipEnable);
            model.Cpu.Read.CreateConnection(model.Memory.OutputEnable);
            model.Cpu.Write.CreateConnection(model.Memory.WriteEnable);
            model.Address = cpu.Address.CreateConnection(model.Memory.Address);
            model.Data = cpu.Data.CreateConnection(model.Memory.Data);

            return model;
        }

        public const byte MagicValue = 42;

        public static void FillRegisters(this CpuZ80 cpu, 
            byte i = (byte)MagicValue, byte r = (byte)MagicValue,
            UInt16 pc = 0, UInt16 sp = MagicValue,
            UInt16 ix = MagicValue, UInt16 iy = MagicValue,
            byte a = MagicValue, UInt16 bc = MagicValue, UInt16 de = MagicValue, UInt16 hl = MagicValue,
            byte a_a = MagicValue, UInt16 a_bc = MagicValue, UInt16 a_de = MagicValue, UInt16 a_hl = MagicValue)
        {
            cpu.Registers.PC = pc;
            cpu.Registers.SP = sp;
            cpu.Registers.IX = ix;
            cpu.Registers.IY = iy;
            cpu.Registers.I = i;
            cpu.Registers.R = r;
            cpu.Registers.PrimarySet.A = a;
            cpu.Registers.PrimarySet.BC = bc;
            cpu.Registers.PrimarySet.DE = de;
            cpu.Registers.PrimarySet.HL = hl;
            cpu.Registers.AlternateSet.A = a_a;
            cpu.Registers.AlternateSet.BC = a_bc;
            cpu.Registers.AlternateSet.DE = a_de;
            cpu.Registers.AlternateSet.HL = a_hl;
        }

        public static void AssertRegisters(this CpuZ80 cpu,
            byte i = 0, byte r = 0,
            UInt16 pc = 0, UInt16 sp = 0,
            UInt16 ix = MagicValue, UInt16 iy = MagicValue,
            byte a = MagicValue, UInt16 bc = MagicValue, UInt16 de = MagicValue, UInt16 hl = MagicValue,
            byte a_a = MagicValue, UInt16 a_bc = MagicValue, UInt16 a_de = MagicValue, UInt16 a_hl = MagicValue)
        {
            if (pc != 0)
                cpu.Registers.PC.Should().Be(pc, "pc");
            if (sp != 0)
                cpu.Registers.SP.Should().Be(sp, "sp");

            cpu.Registers.IX.Should().Be(ix, "ix");
            cpu.Registers.IY.Should().Be(iy, "iy");
            if (i != 0)
                cpu.Registers.I.Should().Be(i, "i");
            if (r != 0)
                cpu.Registers.R.Should().Be(r, "r");
            cpu.Registers.PrimarySet.A.Should().Be(a, "a");
            cpu.Registers.PrimarySet.BC.Should().Be(bc, "bc");
            cpu.Registers.PrimarySet.DE.Should().Be(de, "de");
            cpu.Registers.PrimarySet.HL.Should().Be(hl, "hl");
            cpu.Registers.AlternateSet.A.Should().Be(a_a, "a'");
            cpu.Registers.AlternateSet.BC.Should().Be(a_bc, "bc'");
            cpu.Registers.AlternateSet.DE.Should().Be(a_de, "de'");
            cpu.Registers.AlternateSet.HL.Should().Be(a_hl, "hl'");
        }

    }
}
