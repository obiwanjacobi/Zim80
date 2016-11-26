using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.Components.Logic;
using FluentAssertions;

namespace Jacobi.Zim80.UnitTests.Components.Logic
{
    [TestClass]
    public class AndGateTest
    {
        [TestMethod]
        public void Write_LowLow_Low()
        {
            var uut = new AndGate();
            var input1 = uut.AddInput().CreateConnection();
            var input2 = uut.AddInput().CreateConnection();
            var output = uut.Output.CreateConnection();

            input1.Write(DigitalLevel.Low);
            input2.Write(DigitalLevel.Low);

            uut.Output.Level.Should().Be(DigitalLevel.Low);
        }

        [TestMethod]
        public void Write_HighLow_Low()
        {
            var uut = new AndGate();
            var input1 = uut.AddInput().CreateConnection();
            var input2 = uut.AddInput().CreateConnection();
            var output = uut.Output.CreateConnection();

            input1.Write(DigitalLevel.High);
            input2.Write(DigitalLevel.Low);

            uut.Output.Level.Should().Be(DigitalLevel.Low);
        }

        [TestMethod]
        public void Write_HighHigh_High()
        {
            var uut = new AndGate();
            var input1 = uut.AddInput().CreateConnection();
            var input2 = uut.AddInput().CreateConnection();
            var output = uut.Output.CreateConnection();

            input1.Write(DigitalLevel.High);
            input2.Write(DigitalLevel.High);

            uut.Output.Level.Should().Be(DigitalLevel.High);
        }

        [TestMethod]
        public void Write_HighPosEdge_Low()
        {
            var uut = new AndGate();
            var input1 = uut.AddInput().CreateConnection();
            var input2 = uut.AddInput().CreateConnection();
            var output = uut.Output.CreateConnection();

            input1.Write(DigitalLevel.High);
            input2.Write(DigitalLevel.PosEdge);

            uut.Output.Level.Should().Be(DigitalLevel.Low);
        }

        [TestMethod]
        public void Write_HighHighHighHigh_High()
        {
            var uut = new AndGate();
            var input1 = uut.AddInput().CreateConnection();
            var input2 = uut.AddInput().CreateConnection();
            var input3 = uut.AddInput().CreateConnection();
            var input4 = uut.AddInput().CreateConnection();
            var output = uut.Output.CreateConnection();

            input1.Write(DigitalLevel.High);
            input2.Write(DigitalLevel.High);
            input3.Write(DigitalLevel.High);
            input4.Write(DigitalLevel.High);

            uut.Output.Level.Should().Be(DigitalLevel.High);
        }
    }
}
