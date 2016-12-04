using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.Test;
using FluentAssertions;

namespace Jacobi.Zim80.IntegrationTests.CpuZ80.Routines
{
    [TestClass]
    [DeploymentItem(OutPath + CallSubRoutineBin)]
    public class CallSubRoutine : IntegrationTest
    {
        private const string CallSubRoutineBin = "CallSubRoutine.bin";
        private const string Expected = "Hello World!";

        [TestMethod]
        public void Int_CallSubRoutineBin()
        {
            ushort end = 0x15;
            int trg = 0x30;
            var model = CreateModel(CallSubRoutineBin);

            RunTest(model, end);

            var actual = model.Memory.GetString(trg);
            actual.Should().Be(Expected);
        }
    }
}
