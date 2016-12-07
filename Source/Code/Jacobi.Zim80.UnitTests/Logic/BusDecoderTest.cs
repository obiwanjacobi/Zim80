using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.Logic;
using Jacobi.Zim80.Test;
using FluentAssertions;

namespace Jacobi.Zim80.UnitTests.Logic
{
    [TestClass]
    public class BusDecoderTest
    {
        private const UInt32 Value = 0x0100;

        private BusDecoder _uut;
        private DigitalSignalConsumer _output;
        private BusMaster _input;

        [TestInitialize]
        public void TestInitialize()
        {
            _uut = new BusDecoder();
            _output = _uut.Output.CreateConnection();

            var inputBus = new Bus(16);
            _uut.Input.ConnectTo(inputBus);
            _input = new BusMaster(inputBus);
            _input.IsEnabled = true;
        }

        [TestMethod]
        public void Decode_MatchingValue_OutputHigh()
        {
            _uut.AddValue(Value);

            var value = NewValue(Value);
            _input.Write(value);

            _uut.Output.Level.Should().Be(DigitalLevel.High);
        }

        [TestMethod]
        public void Decode_NonMatchingValue_OutputHigh()
        {
            _uut.AddValue(Value);

            var value = NewValue(Value + Value);
            _input.Write(value);

            _uut.Output.Level.Should().Be(DigitalLevel.Low);
        }

        [TestMethod]
        public void Decode_MultipleMatchingValues_OutputHigh()
        {
            _uut.AddValue(Value);
            _uut.AddValue(Value + Value);

            var value = NewValue(Value);
            _input.Write(value);

            _uut.Output.Level.Should().Be(DigitalLevel.High);

            value = NewValue(Value + Value);
            _input.Write(value);

            _uut.Output.Level.Should().Be(DigitalLevel.High);
        }

        [TestMethod]
        public void Decode_MultipleMixedValues_OutputHighAndLow()
        {
            _uut.AddValue(Value);
            _uut.AddValue(Value + Value);

            var value = NewValue(Value);
            _input.Write(value);

            _uut.Output.Level.Should().Be(DigitalLevel.High);

            value = NewValue(Value + 42);
            _input.Write(value);

            _uut.Output.Level.Should().Be(DigitalLevel.Low);
        }

        private BusData NewValue(UInt32 value)
        {
            var data = new BusData(16);
            data.Write(value);
            return data;
        }
    }
}
