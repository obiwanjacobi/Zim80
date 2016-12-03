using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Jacobi.Zim80.Test;

namespace Jacobi.Zim80.IntegrationTests.CpuZ80.Routines
{
    [TestClass]
    [DeploymentItem(OutPath + Ldir1Bin)]
    public class Ldir
    {
        private const string OutPath = @"CpuZ80\Routines\";
        private const string Ldir1Bin = "Ldir1.bin";

        public TestContext TestContext { get; set; }

        [TestMethod]
        public void Ldir1()
        {
            var model = CreateModel(Ldir1Bin);

        }

        private SimulationModel CreateModel(string file)
        {
            var builder = new SimulationModelBuilder();
            builder.AddCpuClockGen();
            builder.AddCpuMemory();
            builder.AddLogicAnalyzer();

            var bytes = Load(file);
            builder.Model.Memory.Write(bytes);

            return builder.Model;
        }

        private byte[] Load(string file)
        {
            var path = Path.Combine(TestContext.TestDeploymentDir, file);
            return File.ReadAllBytes(path);
        }
    }
}
