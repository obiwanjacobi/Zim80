using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.Test;
using FluentAssertions;

namespace Jacobi.Zim80.IntegrationTests.CpuZ80.Routines
{
    [TestClass]
    [DeploymentItem(OutPath + Ldir1Bin)]
    public class Ldir : IntegrationTest
    {
        private const string Ldir1Bin = "Ldir1.bin";
        private const string Expected = "Hello World!";

        [TestMethod]
        public void Int_Ldir1()
        {
            ushort end = 0x0B;
            int trg = 0x20;
            var model = CreateModel(Ldir1Bin);

            RunTest(model, end);

            var actual = model.Memory.GetString(trg);
            actual.Should().Be(Expected);
        }
    }
}
