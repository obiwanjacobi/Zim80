using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace Jacobi.Zim80.IntegrationTests.CpuZ80.Zexlax
{
    [TestClass]
    [DeploymentItem(OutPath + ZexAllBin)]
    public class ZexAll : IntegrationTest
    {
        public const string OutPath = @"CpuZ80\Zexlax\";
        private const string ZexAllBin = "zexall.zmac.bin";

        [TestMethod]
        public void Int_ZexAll()
        {
            ushort start = 0x100;
            ushort fail = 0x0;
            ushort crash = 0x010F;
            ushort ok = 0x0440;
            var model = CreateModel(ZexAllBin);

            model.Cpu.Registers.PC = start;

            RunTest(model, fail, crash, ok);

            model.Cpu.Registers.PC.Should().Be((ushort)(ok + 1));
        }
    }
}
