using Jacobi.Zim80.CpuZ80.Opcodes;
using Jacobi.Zim80.CpuZ80.UnitTests;
using Jacobi.Zim80.Memory.UnitTests;
using Jacobi.Zim80.Test;
using Jacobi.Zim80.UnitTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Jacobi.Zim80.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class LoadImmediateIndirectInstructionTest
    {
        private const ushort Address = 5;
        private const sbyte Offset = -1;

        private const byte Value = 0x55;

        [TestMethod]
        public void LD_HL_n()
        {
            var model = ExecuteTest((m) => {
                m.Cpu.FillRegisters(hl: Address);
            });

            model.Memory.Assert(Address, Value);
        }

        [TestMethod]
        public void LD_IXd_n()
        {
            var model = ExecuteTest((m) => {
                m.Cpu.FillRegisters(ix: Address);
            }, 0xDD);

            model.Memory.Assert(Address + Offset, Value);
        }

        [TestMethod]
        public void LD_IYd_n()
        {
            var model = ExecuteTest((m) => {
                m.Cpu.FillRegisters(iy: Address);
            }, 0xFD);

            model.Memory.Assert(Address + Offset, Value);
        }

        private static SimulationModel ExecuteTest(Action<SimulationModel> preTest, byte extension = 0)
        {
            var ob = OpcodeByte.New(z: 6, y: 6);
            var cpuZ80 = new CpuZ80();
            var buffer = extension == 0 ?
                new byte[] { ob.Value, Value, 0, 0, 0, 0 } :
                new byte[] { extension, ob.Value, unchecked((byte)Offset), Value, 0, 0, 0 };
            var model = cpuZ80.Initialize(buffer);

            cpuZ80.FillRegisters();
            preTest(model);

            model.ClockGen.SquareWave(extension == 0 ? 10 : 19);

            Console.WriteLine(model.LogicAnalyzer.ToWaveJson());

            return model;
        }
    }
}
