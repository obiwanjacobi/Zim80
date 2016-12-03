﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Jacobi.Zim80.Logic;
using Jacobi.Zim80.Test;

namespace Jacobi.Zim80.CpuZ80.UnitTests
{
    [TestClass]
    public class CpuZ80FetchTest
    {
        [TestMethod]
        public void Clock_PosEdgeCycleT1_SignalsCorrect()
        {
            var cpu = new CpuZ80();
            cpu.Clock(CycleNames.T1, DigitalLevel.PosEdge);

            // active
            cpu.MachineCycle1.Level.Should().Be(DigitalLevel.Low);
            
            // inactive
            cpu.Refresh.Level.Should().Be(DigitalLevel.High);
            cpu.IoRequest.Level.Should().Be(DigitalLevel.High);
            cpu.Write.Level.Should().Be(DigitalLevel.High);

            // bus
            cpu.Address.Value.Should().Be(new BusData16(0));
        }

        [TestMethod]
        public void Clock_NegEdgeCycleT1_SignalsCorrect()
        {
            var cpu = new CpuZ80();
            cpu.Clock(CycleNames.T1, DigitalLevel.NegEdge);

            // active
            cpu.MachineCycle1.Level.Should().Be(DigitalLevel.Low);
            cpu.Read.Level.Should().Be(DigitalLevel.Low);
            cpu.MemoryRequest.Level.Should().Be(DigitalLevel.Low);

            // inactive
            cpu.Refresh.Level.Should().Be(DigitalLevel.High);
            cpu.IoRequest.Level.Should().Be(DigitalLevel.High);
            cpu.Write.Level.Should().Be(DigitalLevel.High);

            // bus
            cpu.Address.Value.Should().Be(new BusData16(0));
        }

        [TestMethod]
        public void Clock_PosEdgeCycleT3_SignalsCorrect()
        {
            var cpu = new CpuZ80();
            cpu.Clock(CycleNames.T3, DigitalLevel.PosEdge);

            // active
            cpu.Refresh.Level.Should().Be(DigitalLevel.Low);

            // inactive
            cpu.MachineCycle1.Level.Should().Be(DigitalLevel.High);
            cpu.Read.Level.Should().Be(DigitalLevel.High);
            cpu.MemoryRequest.Level.Should().Be(DigitalLevel.High);
            cpu.IoRequest.Level.Should().Be(DigitalLevel.High);
            cpu.Write.Level.Should().Be(DigitalLevel.High);

            // bus
            cpu.Address.Value.Should().Be(new BusData16(0));
        }

        [TestMethod]
        public void Clock_NegEdgeCycleT3_SignalsCorrect()
        {
            var cpu = new CpuZ80();
            cpu.Clock(CycleNames.T3, DigitalLevel.NegEdge);

            // active
            cpu.Refresh.Level.Should().Be(DigitalLevel.Low);
            cpu.MemoryRequest.Level.Should().Be(DigitalLevel.Low);

            // inactive
            cpu.MachineCycle1.Level.Should().Be(DigitalLevel.High);
            cpu.Read.Level.Should().Be(DigitalLevel.High);
            cpu.IoRequest.Level.Should().Be(DigitalLevel.High);
            cpu.Write.Level.Should().Be(DigitalLevel.High);

            // bus
            cpu.Address.Value.Should().Be(new BusData16(0));
        }

        [TestMethod]
        public void Clock_NegEdgeCycleT4_SignalsCorrect()
        {
            var cpu = new CpuZ80();
            cpu.Clock(CycleNames.T4, DigitalLevel.NegEdge);

            // active
            cpu.Refresh.Level.Should().Be(DigitalLevel.Low);

            // inactive
            cpu.MachineCycle1.Level.Should().Be(DigitalLevel.High);
            cpu.MemoryRequest.Level.Should().Be(DigitalLevel.High);
            cpu.Read.Level.Should().Be(DigitalLevel.High);
            cpu.IoRequest.Level.Should().Be(DigitalLevel.High);
            cpu.Write.Level.Should().Be(DigitalLevel.High);

            // bus
            cpu.Address.Value.Should().Be(new BusData16(0));
        }

        
    }

    internal static class CpuExtensions
    {
        public static void Clock(this CpuZ80 cpu,
        CycleNames toCycle, DigitalLevel toLevel)
        {
            var gen = new SignalGenerator();
            var clock = cpu.Clock.ConnectTo(gen.Output);

            gen.SquareWave(1, toCycle, toLevel);
        }
    }
}
