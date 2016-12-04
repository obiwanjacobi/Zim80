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

        [TestMethod]
        public void Int_CallSubRoutineBin()
        {
            ushort end = 0x15;
            int trg = 0x30;
            var model = CreateModel(CallSubRoutineBin);

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
