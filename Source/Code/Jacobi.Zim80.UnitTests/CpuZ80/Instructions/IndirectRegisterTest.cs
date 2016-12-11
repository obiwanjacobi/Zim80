using Jacobi.Zim80.CpuZ80.Opcodes;
using Jacobi.Zim80.CpuZ80.UnitTests;
using Jacobi.Zim80.Diagnostics;
using Jacobi.Zim80.Memory.UnitTests;
using Jacobi.Zim80.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Jacobi.Zim80.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class IndirectRegisterTest
    {
        private const ushort Address1 = 0x05;
        private const ushort Address2 = 0x06;

        private const byte Value1 = 0x55;
        private const byte Value2 = 0xAA;

        [TestMethod]
        public void IncHL()
        {
            var ob = OpcodeByte.New(z: 4, y: 6);

            var model = ExecuteTest(ob, (m) => {
                m.Cpu.FillRegisters(hl: Address2);
                m.Memory.Set(Address2, Value2);
            });

            model.Cpu.AssertRegisters(hl: Address2);
            model.Memory.Assert(Address2, Value2 + 1);
        }

        [TestMethod]
        public void DecHL()
        {
            var ob = OpcodeByte.New(z: 5, y: 6);

            var model = ExecuteTest(ob, (m) => {
                m.Cpu.FillRegisters(hl: Address2);
                m.Memory.Set(Address2, Value2);
            });

            model.Cpu.AssertRegisters(hl: Address2);
            model.Memory.Assert(Address2, Value2 - 1);
        }

        [TestMethod]
        public void IncIXd()
        {
            var ob = OpcodeByte.New(z: 4, y: 6);

            // INC (IX-1)
            var model = ExecuteTest(ob, (m) => {
                m.Cpu.FillRegisters(ix: Address2);
                m.Memory.Set(Address1, Value2);
                m.Memory.Set(Address2, Value1);
            }, 0xDD, -1);

            model.Cpu.AssertRegisters(ix: Address2);
            model.Memory.Assert(Address1, Value2 + 1);
            model.Memory.Assert(Address2, Value1);
        }

        [TestMethod]
        public void DecIXd()
        {
            var ob = OpcodeByte.New(z: 5, y: 6);

            // DEC (IX-1)
            var model = ExecuteTest(ob, (m) => {
                m.Cpu.FillRegisters(ix: Address2);
                m.Memory.Set(Address1, Value2);
                m.Memory.Set(Address2, Value1);
            }, 0xDD, -1);

            model.Cpu.AssertRegisters(ix: Address2);
            model.Memory.Assert(Address1, Value2 - 1);
            model.Memory.Assert(Address2, Value1);
        }

        [TestMethod]
        public void IncIYd()
        {
            var ob = OpcodeByte.New(z: 4, y: 6);

            // INC (IY-1)
            var model = ExecuteTest(ob, (m) => {
                m.Cpu.FillRegisters(iy: Address2);
                m.Memory.Set(Address1, Value2);
                m.Memory.Set(Address2, Value1);
            }, 0xFD, -1);

            model.Cpu.AssertRegisters(iy: Address2);
            model.Memory.Assert(Address1, Value2 + 1);
            model.Memory.Assert(Address2, Value1);
        }

        private static SimulationModel ExecuteTest(OpcodeByte ob,
                Action<SimulationModel> preTest, byte extension = 0, sbyte? d = null)
        {
            var cpuZ80 = new CpuZ80();
            byte[] buffer = (extension == 0) ?
                new byte[] { ob.Value, 0, 0, 0, 0, 0 } :
                new byte[] { extension, ob.Value, (byte)d, 0, 0, 0 };
            var model = cpuZ80.Initialize(buffer);

            preTest(model);

            model.ClockGen.SquareWave(extension == 0 ? 11 : 23);

            Console.WriteLine(model.LogicAnalyzer.ToWaveJson());

            return model;
        }
    }
}
