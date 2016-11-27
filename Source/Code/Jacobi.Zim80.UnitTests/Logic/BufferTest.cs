using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gates = Jacobi.Zim80.Logic;
using FluentAssertions;

namespace Jacobi.Zim80.UnitTests.Logic
{
    [TestClass]
    public class BufferTest
    {
        [TestMethod]
        public void Write_OutputDisabled_Floating()
        {
            var uut = new Gates.Buffer();
            var oe = uut.OutputEnable.CreateConnection();

            DigitalSignalConsumer input1;
            DigitalSignalProvider output1;
            uut.Add(out input1, out output1);

            var stimulus = input1.CreateConnection();
            var result = output1.CreateConnection();

            stimulus.Write(DigitalLevel.High);
            result.Level.Should().Be(DigitalLevel.Floating);
        }

        [TestMethod]
        public void Write_OutputSetDisabled_Floating()
        {
            var uut = new Gates.Buffer();
            var oe = uut.OutputEnable.CreateConnection();

            DigitalSignalConsumer input1;
            DigitalSignalProvider output1;
            uut.Add(out input1, out output1);

            var stimulus = input1.CreateConnection();
            var result = output1.CreateConnection();

            oe.Write(DigitalLevel.Low); // enabled
            stimulus.Write(DigitalLevel.High);

            oe.Write(DigitalLevel.High); // disabled
            result.Level.Should().Be(DigitalLevel.Floating);
        }

        [TestMethod]
        public void Write_OutputEnabled_OutputIsInput()
        {
            var uut = new Gates.Buffer();
            var oe = uut.OutputEnable.CreateConnection();

            DigitalSignalConsumer input1;
            DigitalSignalProvider output1;
            uut.Add(out input1, out output1);

            var stimulus = input1.CreateConnection();
            var result = output1.CreateConnection();

            oe.Write(DigitalLevel.Low); // enabled
            stimulus.Write(DigitalLevel.High);

            result.Level.Should().Be(DigitalLevel.High);
        }
    }
}
