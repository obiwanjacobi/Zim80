using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.Logic;
using Jacobi.Zim80.Test;
using System.Linq;
using FluentAssertions;

namespace Jacobi.Zim80.UnitTests.Logic
{
    [TestClass]
    public class OutputPortTest
    {
        private const byte Value = 0x55;
        private const byte Value2 = 0xAA;

        private OutputPort _uut;
        private DigitalSignalProvider _portEnable;
        private BusMaster _input;

        [TestInitialize]
        public void TestInitialize()
        {
            _uut = new OutputPort();
            _portEnable = _uut.PortEnable.CreateConnection();

            var inputBus = new Bus(8);
            _uut.Input.ConnectTo(inputBus);
            _input = new BusMaster(inputBus);
            _input.IsEnabled = true;
        }

        [TestMethod]
        public void Enable_PosEdge_CapturedValue()
        {
            var value = NewValue(Value);
            _input.Write(value);

            _portEnable.Write(DigitalLevel.PosEdge);

            _uut.DataStream.Samples.First().ToByte().Should().Be(Value);
        }

        [TestMethod]
        public void Enable_Cycles_CapturedMultipleValues()
        {
            var value = NewValue(Value);
            _input.Write(value);

            _portEnable.Write(DigitalLevel.PosEdge);
            _portEnable.Write(DigitalLevel.High);

            value = NewValue(Value2);
            _input.Write(value);

            _portEnable.Write(DigitalLevel.PosEdge);

            _uut.DataStream.Samples.Should().HaveCount(2);
            _uut.DataStream.Samples.First().ToByte().Should().Be(Value);
            _uut.DataStream.Samples.Skip(1).First().ToByte().Should().Be(Value2);
        }

        [TestMethod]
        public void Enable_Cycles_CapturedString()
        {
            var value = NewValue((byte)'H');
            _input.Write(value);

            _portEnable.Write(DigitalLevel.PosEdge);
            _portEnable.Write(DigitalLevel.High);

            value = NewValue((byte)'i');
            _input.Write(value);

            _portEnable.Write(DigitalLevel.PosEdge);

            _uut.DataStream.ToString().Should().Be("Hi");
        }

        private BusData NewValue(byte value)
        {
            var data = new BusData(8);
            data.Write(value);
            return data;
        }
    }
}
