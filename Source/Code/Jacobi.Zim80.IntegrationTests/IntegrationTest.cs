using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.Test;
using System.IO;
using System;
using Jacobi.Zim80.Diagnostics;

namespace Jacobi.Zim80.IntegrationTests
{
    public abstract class IntegrationTest
    {
        public const string OutPath = @"CpuZ80\Routines\";

        public TestContext TestContext { get; set; }

        protected void RunTest(SimulationModel model, ushort endAddress)
        {
            bool stop = false;
            model.Cpu.InstructionExecuted += (s, e) => {
                stop = e.Opcode.Address == endAddress;
            };

            model.ClockGen.SquareWave((c) => stop);

            Console.WriteLine(model.LogicAnalyzer.ToWaveJson());
        }

        protected SimulationModel CreateModel(string file)
        {
            var builder = new SimulationModelBuilder();
            builder.AddCpuClockGen();
            builder.AddCpuMemory();
            builder.AddLogicAnalyzer();

            var bytes = LoadFile(file);
            builder.Model.Memory.Write(bytes);

            return builder.Model;
        }

        protected byte[] LoadFile(string file)
        {
            var path = Path.Combine(TestContext.TestDeploymentDir, file);
            return File.ReadAllBytes(path);
        }
    }
}
