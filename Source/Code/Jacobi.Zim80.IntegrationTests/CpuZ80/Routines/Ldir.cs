using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Jacobi.Zim80.Test;
using FluentAssertions;
using System;
using Jacobi.Zim80.Diagnostics;

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

            bool stop = false;
            model.Cpu.InstructionExecuted += (s, e) => {
                stop = e.Opcode.Address == 0x0B;
            };

            model.ClockGen.SquareWave((c) => stop);

            Console.WriteLine(model.LogicAnalyzer.ToWaveJson());

            model.Memory.GetInt(0x20).Should().Be('H');
            model.Memory.GetInt(0x21).Should().Be('e');
            model.Memory.GetInt(0x22).Should().Be('l');
            model.Memory.GetInt(0x23).Should().Be('l');
            model.Memory.GetInt(0x24).Should().Be('o');
            model.Memory.GetInt(0x25).Should().Be(' ');
            model.Memory.GetInt(0x26).Should().Be('W');
            model.Memory.GetInt(0x27).Should().Be('o');
            model.Memory.GetInt(0x28).Should().Be('r');
            model.Memory.GetInt(0x29).Should().Be('l');
            model.Memory.GetInt(0x2A).Should().Be('d');
            model.Memory.GetInt(0x2B).Should().Be('!');
            model.Memory.GetInt(0x2C).Should().Be(0);
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
