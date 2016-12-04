using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.Test;
using FluentAssertions;

namespace Jacobi.Zim80.IntegrationTests.CpuZ80.Routines
{
    [TestClass]
    [DeploymentItem(OutPath + CopyStringBin)]
    public class CopyString : IntegrationTest
    {
        private const string CopyStringBin = "CopyString.bin";
        private const string Expected = "Hello World!";

        [TestMethod]
        public void Int_CopyString()
        {
            ushort end = 0x10;
            int trg = 0x25;
            var model = CreateModel(CopyStringBin);

            RunTest(model, end);

            var actual = model.Memory.GetString(trg);
            actual.Should().Be(Expected);
        }
    }
}
