using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.Logic;
using Jacobi.Zim80.Test;
using FluentAssertions;

namespace Jacobi.Zim80.UnitTests.Logic
{
    [TestClass]
    public class InputPortTest
    {
        private const byte Value = 0x55;
        private const byte Value2 = 0xAA;

        private InputPort _uut;
        private DigitalSignalProvider _portEnable;
        private BusSlave _output;

        [TestInitialize]
        public void TestInitialize()
        {
            _uut = new InputPort();
            _uut.DataBuffer.Write(new byte[] { Value, Value2 });
            _portEnable = _uut.PortEnable.CreateConnection();

            var outputBus = new Bus(8);
            _uut.Output.ConnectTo(outputBus);
            _output = new BusSlave(outputBus);
        }

        [TestMethod]
        public void Enable_PosEdge_ReleaseValue()
        {
            _portEnable.Write(DigitalLevel.PosEdge);

            _output.Value.ToByte().Should().Be(Value);
        }

        [TestMethod]
        public void Enable_Cycles_ReleaseMultipleValues()
        {
            _portEnable.Write(DigitalLevel.PosEdge);

            _output.Value.ToByte().Should().Be(Value);

            _portEnable.Write(DigitalLevel.High);
            _portEnable.Write(DigitalLevel.PosEdge);

            _output.Value.ToByte().Should().Be(Value2);
        }
    }
}
