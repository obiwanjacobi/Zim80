﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.Test;
using FluentAssertions;

namespace Jacobi.Zim80.IntegrationTests.CpuZ80.Routines
{
    [TestClass]
    [DeploymentItem(OutPath + CopyStringBin)]
    public class CopyString : IntegrationTest
    {
        private const string CopyStringBin = "CopyString.bin";

        [TestMethod]
        public void Int_CopyString()
        {
            ushort end = 0x10;
            int trg = 0x25;
            var model = CreateModel(CopyStringBin);

            ExecuteTest(model, end);

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
