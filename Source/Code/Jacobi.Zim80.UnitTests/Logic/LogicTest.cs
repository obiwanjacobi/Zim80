using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.Logic;
using Jacobi.Zim80.Test;
using FluentAssertions;
using Jacobi.Zim80.Memory;
using System;
using Jacobi.Zim80.Diagnostics;

namespace Jacobi.Zim80.UnitTests.Logic
{
    [TestClass]
    public class LogicTest
    {
        [TestMethod]
        public void BusDecoderAnd_DecodeAnd_OutputActiveWhenEnabled()
        {
            const byte Value = 0x10;

            var decoder = new BusDecoder("decoder");
            decoder.AddValue(Value);
            decoder.Input.ConnectTo(new Bus<BusData8>("bus"));
            var busInput = decoder.Input.CreateConnection();
            busInput.IsEnabled = true;

            var and = new AndGate() { Name = "and" };
            and.AddInput().ConnectTo(decoder.Output);
            var enable = and.AddInput().CreateConnection();

            var output = and.Output.CreateConnection();

            busInput.Write(new BusData8(Value));
            decoder.Output.Level.Should().Be(DigitalLevel.High);
            output.DigitalSignal.Level.Should().Be(DigitalLevel.Low);

            enable.Write(DigitalLevel.High);
            output.DigitalSignal.Level.Should().Be(DigitalLevel.High);
        }

        [TestMethod]
        public void BusDecoderInvertorOr_DecodeOr_OutputActiveWhenEnabled()
        {
            const byte Value = 0x10;

            var decoder = new BusDecoder("decoder");
            decoder.AddValue(Value);
            decoder.Input.ConnectTo(new Bus<BusData8>("bus"));
            var busInput = decoder.Input.CreateConnection();
            busInput.IsEnabled = true;

            var invertor = new InvertorGate() { Name = "inv" };
            invertor.Input.ConnectTo(decoder.Output);

            var or = new OrGate() { Name = "or" };
            or.AddInput().ConnectTo(invertor.Output);
            var enable = or.AddInput().CreateConnection();
            enable.Write(DigitalLevel.High);

            var output = or.Output.CreateConnection();

            busInput.Write(new BusData8(Value));
            decoder.Output.Level.Should().Be(DigitalLevel.High);
            invertor.Output.Level.Should().Be(DigitalLevel.Low);
            output.DigitalSignal.Level.Should().Be(DigitalLevel.High);

            enable.Write(DigitalLevel.Low);
            output.DigitalSignal.Level.Should().Be(DigitalLevel.Low);
        }

        [TestMethod]
        public void AddInputPort_InitialState_IsPropegated()
        {
            var uut = new SimulationModelBuilder();
            uut.AddCpuMemory();
            ((IDirectMemoryAccess<BusData8>)uut.Model.Memory)[0] = new BusData8(0); // nop
            uut.AddCpuClockGen();
            var inputPort = uut.AddInputPort(0x10, "Test");
            uut.AddLogicAnalyzer();

            inputPort.DataBuffer.Write(new BusData8(0xFF));

            uut.Model.ClockGen.SquareWave(4);

            Console.WriteLine(uut.Model.LogicAnalyzer.ToWaveJson());

            inputPort.DataBuffer.Read().ToByte().Should().Be(0xFF);
        }
    }
}
