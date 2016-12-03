﻿using Jacobi.Zim80.Memory.UnitTests;
using Jacobi.Zim80.Test;
using Jacobi.Zim80.Logic;
using FluentAssertions;
using System;

namespace Jacobi.Zim80.CpuZ80.UnitTests
{
    internal static class CpuZ80TestExtensions
    {
        public static SimulationModel Initialize(this CpuZ80 cpu, byte[] program, byte[] ioSpace = null)
        {
            new InstructionLogger(cpu);

            var model = new SimulationModel();
            model.Cpu = cpu;
            model.Cpu.Name = "U1";
            model.ClockGen = new SignalGenerator();
            model.ClockGen.Output.ConnectTo(model.Cpu.Clock, "Clock");

            model.Memory = MemoryTestExtensions.NewRam(program);
            model.Memory.Name = "U2";
            model.Cpu.MemoryRequest.ConnectTo(model.Memory.ChipEnable, "MREQ");
            model.Cpu.Read.ConnectTo(model.Memory.OutputEnable, "RD");
            model.Cpu.Write.ConnectTo(model.Memory.WriteEnable, "WE");
            model.Address = cpu.Address.ConnectTo(model.Memory.Address, "Address");
            model.Data = cpu.Data.ConnectTo(model.Memory.Data.Slave, "Data");
            new MemoryLogger<BusData16, BusData8>().Attach(model.Memory);

            if (ioSpace != null)
            {
                model.IoSpace = MemoryTestExtensions.NewRam(ioSpace);
                model.IoSpace.Name = "U3";
                model.Cpu.IoRequest.ConnectTo(model.IoSpace.ChipEnable, "IORQ");
                model.IoSpace.OutputEnable.ConnectTo(model.Cpu.Read.DigitalSignal);
                model.IoSpace.WriteEnable.ConnectTo(model.Cpu.Write.DigitalSignal);
                model.IoSpace.Address.ConnectTo(cpu.Address.Bus);
                model.IoSpace.Data.ConnectTo(cpu.Data.Bus);
                new MemoryLogger<BusData16, BusData8>("IO").Attach(model.IoSpace);
            }

            model.LogicAnalyzer = new LogicAnalyzer();
            model.LogicAnalyzer.Clock.ConnectTo(model.ClockGen.Output.DigitalSignal);
            model.Cpu.MachineCycle1.ConnectTo(model.LogicAnalyzer.AddInput("M1"));
            model.Cpu.Refresh.ConnectTo(model.LogicAnalyzer.AddInput("RFSH"));
            model.LogicAnalyzer.ConnectInput(model.Cpu.MemoryRequest.DigitalSignal);
            if (ioSpace != null)
                model.LogicAnalyzer.ConnectInput(model.Cpu.IoRequest.DigitalSignal);
            model.LogicAnalyzer.ConnectInput(model.Cpu.Read.DigitalSignal);
            model.LogicAnalyzer.ConnectInput(model.Cpu.Write.DigitalSignal);
            model.LogicAnalyzer.ConnectInput(model.Address);
            model.LogicAnalyzer.ConnectInput(model.Data);
            model.LogicAnalyzer.Start();

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
            FillRegisters(cpu.Registers, i, r, pc, sp, ix, iy, 
                    a, bc, de, hl, 
                    a_a, a_bc, a_de, a_hl);
        }

        public static void FillRegisters(this Registers registers,
            byte i = (byte)MagicValue, byte r = (byte)MagicValue,
            UInt16 pc = 0, UInt16 sp = MagicValue,
            UInt16 ix = MagicValue, UInt16 iy = MagicValue,
            byte a = MagicValue, UInt16 bc = MagicValue, UInt16 de = MagicValue, UInt16 hl = MagicValue,
            byte a_a = MagicValue, UInt16 a_bc = MagicValue, UInt16 a_de = MagicValue, UInt16 a_hl = MagicValue)
        {
            registers.PC = pc;
            registers.SP = sp;
            registers.IX = ix;
            registers.IY = iy;
            registers.I = i;
            registers.R = r;
            registers.A = a;
            registers.BC = bc;
            registers.DE = de;
            registers.HL = hl;
            registers.AlternateSet.A = a_a;
            registers.AlternateSet.BC = a_bc;
            registers.AlternateSet.DE = a_de;
            registers.AlternateSet.HL = a_hl;
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
            cpu.Registers.A.Should().Be(a, "a");
            cpu.Registers.BC.Should().Be(bc, "bc");
            cpu.Registers.DE.Should().Be(de, "de");
            cpu.Registers.HL.Should().Be(hl, "hl");
            cpu.Registers.AlternateSet.A.Should().Be(a_a, "a'");
            cpu.Registers.AlternateSet.BC.Should().Be(a_bc, "bc'");
            cpu.Registers.AlternateSet.DE.Should().Be(a_de, "de'");
            cpu.Registers.AlternateSet.HL.Should().Be(a_hl, "hl'");
        }

    }
}
