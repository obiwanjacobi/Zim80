using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.Test;
using FluentAssertions;

namespace Jacobi.Zim80.IntegrationTests.CpuZ80.Routines
{
    [TestClass]
    [DeploymentItem(OutPath + JumpingBeanBin)]
    public class JumpingBean : IntegrationTest
    {
        private const string JumpingBeanBin = "JumpingBean.bin";

        [TestMethod]
        public void Int_JumpingBean()
        {
            ushort end = 0x0E;
            int trg = 0x35;
            var model = CreateModel(JumpingBeanBin);

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
