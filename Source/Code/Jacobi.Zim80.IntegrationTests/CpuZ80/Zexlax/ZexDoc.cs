using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System.Linq;

namespace Jacobi.Zim80.IntegrationTests.CpuZ80.Zexlax
{
    [TestClass]
    [DeploymentItem(OutPath + ZexDocBin)]
    public class ZexDoc : IntegrationTest
    {
        public const string OutPath = @"CpuZ80\Zexlax\";
        private const string ZexDocBin = "zexdoc.zmac.bin";

        //[TestMethod]
        public void Int_ZexDoc()
        {
            ushort start = 0x0100;
            ushort stop = 0x0000;
            var model = CreateModel(ZexDocBin);
            LogExecutionPath(model);

            model.Cpu.Registers.PC = start;

            RunTest(model, stop);

            TestContext.WriteLine(
                model.OutputPorts.Values.First().DataStream.ToString());

            //model.Cpu.Registers.PC.Should().Be((ushort)(ok + 1));
        }
    }
}
