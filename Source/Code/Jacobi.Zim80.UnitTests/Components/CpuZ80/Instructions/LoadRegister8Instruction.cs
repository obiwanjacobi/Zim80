using System;
using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.Components.CpuZ80.UnitTests;
using FluentAssertions;

namespace Jacobi.Zim80.Components.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class LoadRegister8Instruction
    {
        private const byte Value = 0x55;

        [TestMethod]
        public void LD_reg8_reg8()
        {
            TestAll();
        }

        private void TestAll()
        {
            for (Register8Table trgReg = Register8Table.B; trgReg <= Register8Table.A; trgReg++)
            {
                if (trgReg == Register8Table.HL) continue;

                for (Register8Table srcReg = Register8Table.B; srcReg <= Register8Table.A; srcReg++)
                {
                    if (srcReg == Register8Table.HL) continue;

                    LD_Test(trgReg, srcReg);
                }
            }
        }

        private void LD_Test(Register8Table trgReg, Register8Table srcReg)
        {
            var ob = OpcodeByte.New(x: 1, z: (byte)srcReg, y: (byte)trgReg);
            var cpuZ80 = ExecuteTest(ob, (cpu) => cpu.Registers[srcReg] = Value);

            cpuZ80.Registers[trgReg].Should().Be(Value);
        }

        private CpuZ80 ExecuteTest(OpcodeByte ob, Action<CpuZ80> preTest)
        {
            var cpuZ80 = new CpuZ80();
            var model = cpuZ80.Initialize(new[] { ob.Value });

            cpuZ80.FillRegisters();
            preTest(cpuZ80);

            model.ClockGen.SquareWave(4);

            return cpuZ80;
        }
    }
}
