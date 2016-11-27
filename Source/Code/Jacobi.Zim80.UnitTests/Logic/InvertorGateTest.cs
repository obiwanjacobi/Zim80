using FluentAssertions;
using Jacobi.Zim80.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jacobi.Zim80.UnitTests.Logic
{
    [TestClass]
    public class InvertorGateTest
    {
        [TestMethod]
        public void Invert_High_Low()
        {
            InvertorGate.Invert(DigitalLevel.High).Should().Be(DigitalLevel.Low);
        }

        [TestMethod]
        public void Invert_Low_High()
        {
            InvertorGate.Invert(DigitalLevel.Low).Should().Be(DigitalLevel.High);
        }

        [TestMethod]
        public void Invert_PosEdge_NegEdge()
        {
            InvertorGate.Invert(DigitalLevel.PosEdge).Should().Be(DigitalLevel.NegEdge);
        }

        [TestMethod]
        public void Invert_NegEdge_PosEdge()
        {
            InvertorGate.Invert(DigitalLevel.NegEdge).Should().Be(DigitalLevel.PosEdge);
        }

        [TestMethod]
        public void Invert_Floating_Floating()
        {
            InvertorGate.Invert(DigitalLevel.Floating).Should().Be(DigitalLevel.Floating);
        }

        [TestMethod]
        public void Write_High_Low()
        {
            var uut = new InvertorGate();
            var input = uut.Input.CreateConnection();
            var output = uut.Output.CreateConnection();

            input.Write(DigitalLevel.High);

            output.Level.Should().Be(DigitalLevel.Low);
        }

        [TestMethod]
        public void Write_Low_High()
        {
            var uut = new InvertorGate();
            var input = uut.Input.CreateConnection();
            var output = uut.Output.CreateConnection();

            input.Write(DigitalLevel.Low);

            output.Level.Should().Be(DigitalLevel.High);
        }

        [TestMethod]
        public void Write_PosEdge_NegEdge()
        {
            var uut = new InvertorGate();
            var input = uut.Input.CreateConnection();
            var output = uut.Output.CreateConnection();

            input.Write(DigitalLevel.PosEdge);

            output.Level.Should().Be(DigitalLevel.NegEdge);
        }

        [TestMethod]
        public void Write_NegEdge_PosEdge()
        {
            var uut = new InvertorGate();
            var input = uut.Input.CreateConnection();
            var output = uut.Output.CreateConnection();

            input.Write(DigitalLevel.NegEdge);

            output.Level.Should().Be(DigitalLevel.PosEdge);
        }

        [TestMethod]
        public void Write_Floating_Floating()
        {
            var uut = new InvertorGate();
            var input = uut.Input.CreateConnection();
            var output = uut.Output.CreateConnection();

            input.Write(DigitalLevel.Floating);

            output.Level.Should().Be(DigitalLevel.Floating);
        }
    }
}
