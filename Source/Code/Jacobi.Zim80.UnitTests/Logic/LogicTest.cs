using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.Logic;
using Jacobi.Zim80.Test;
using FluentAssertions;

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
    }
}
