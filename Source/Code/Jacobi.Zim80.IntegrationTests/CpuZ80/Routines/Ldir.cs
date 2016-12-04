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

        [TestMethod]
        public void Int_Ldir1()
        {
            ushort end = 0x0B;
            int trg = 0x20;
            var model = CreateModel(Ldir1Bin);

            RunTest(model, end);

            model.Memory.GetInt(trg++).Should().Be('H');
            model.Memory.GetInt(trg++).Should().Be('e');
            model.Memory.GetInt(trg++).Should().Be('l');
            model.Memory.GetInt(trg++).Should().Be('l');
            model.Memory.GetInt(trg++).Should().Be('o');
            model.Memory.GetInt(trg++).Should().Be(' ');
            model.Memory.GetInt(trg++).Should().Be('W');
            model.Memory.GetInt(trg++).Should().Be('o');
            model.Memory.GetInt(trg++).Should().Be('r');
            model.Memory.GetInt(trg++).Should().Be('l');
            model.Memory.GetInt(trg++).Should().Be('d');
            model.Memory.GetInt(trg++).Should().Be('!');
            model.Memory.GetInt(trg++).Should().Be(0);
        }
    }
}
