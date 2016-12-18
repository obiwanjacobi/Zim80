using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.Test;
using FluentAssertions;

namespace Jacobi.Zim80.IntegrationTests.CpuZ80.Routines
{
    [TestClass]
    [DeploymentItem(OutPath + JumpingBeanBin)]
    public class JumpingBean : IntegrationTest
    {
        public const string OutPath = @"CpuZ80\Routines\";
        private const string JumpingBeanBin = "JumpingBean.bin";
        private const string Expected = "Hello World!";

        [TestMethod]
        public void Int_JumpingBean()
        {
            ushort end = 0x0E;
            int trg = 0x35;
            var model = CreateModel(JumpingBeanBin);

            RunTest(model, end);

            var actual = model.Memory.GetString(trg);
            actual.Should().Be(Expected);
        }
    }
}
