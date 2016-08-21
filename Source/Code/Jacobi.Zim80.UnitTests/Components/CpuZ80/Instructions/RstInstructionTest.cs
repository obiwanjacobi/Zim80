using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using Jacobi.Zim80.Components.CpuZ80.UnitTests;
using Jacobi.Zim80.Components.Memory;

namespace Jacobi.Zim80.Components.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class RstInstructionTest
    {
        [TestMethod]
        public void Rst00()
        {
            var rst = OpcodeByte.New(x: 3, z: 7, y: 0);
            var cpu = ExecuteTest(rst);

            cpu.AssertRegisters(pc: 0x00, sp:CpuZ80TestExtensions.MagicValue - 2);
        }

        [TestMethod]
        public void Rst08()
        {
            var rst = OpcodeByte.New(x: 3, z: 7, y: 1);
            var cpu = ExecuteTest(rst);

            cpu.AssertRegisters(pc: 0x08, sp: CpuZ80TestExtensions.MagicValue - 2);
        }

        [TestMethod]
        public void Rst10()
        {
            var rst = OpcodeByte.New(x: 3, z: 7, y: 2);
            var cpu = ExecuteTest(rst);

            cpu.AssertRegisters(pc: 0x10, sp: CpuZ80TestExtensions.MagicValue - 2);
        }

        [TestMethod]
        public void Rst18()
        {
            var rst = OpcodeByte.New(x: 3, z: 7, y: 3);
            var cpu = ExecuteTest(rst);

            cpu.AssertRegisters(pc: 0x18, sp: CpuZ80TestExtensions.MagicValue - 2);
        }

        [TestMethod]
        public void Rst20()
        {
            var rst = OpcodeByte.New(x: 3, z: 7, y: 4);
            var cpu = ExecuteTest(rst);

            cpu.AssertRegisters(pc: 0x20, sp: CpuZ80TestExtensions.MagicValue - 2);
        }

        [TestMethod]
        public void Rst28()
        {
            var rst = OpcodeByte.New(x: 3, z: 7, y: 5);
            var cpu = ExecuteTest(rst);

            cpu.AssertRegisters(pc: 0x28, sp: CpuZ80TestExtensions.MagicValue - 2);
        }

        [TestMethod]
        public void Rst30()
        {
            var rst = OpcodeByte.New(x: 3, z: 7, y: 6);
            var cpu = ExecuteTest(rst);

            cpu.AssertRegisters(pc: 0x30, sp: CpuZ80TestExtensions.MagicValue - 2);
        }

        [TestMethod]
        public void Rst38()
        {
            var rst = OpcodeByte.New(x: 3, z: 7, y: 7);
            var cpu = ExecuteTest(rst);

            cpu.AssertRegisters(pc: 0x38, sp: CpuZ80TestExtensions.MagicValue - 2);
        }

        private static CpuZ80 ExecuteTest(OpcodeByte rst)
        {
            var cpuZ80 = new CpuZ80();
            var model = cpuZ80.Initialize(null);

            var writer = new MemoryWriter<BusData16, BusData8>(model.Memory);
            writer.Fill(0x48, new BusData8(0));
            writer[new BusData16(0)] = new BusData8(rst.Value);

            cpuZ80.FillRegisters();

            model.ClockGen.BlockWave(11);

            return cpuZ80;
        }
    }
}
