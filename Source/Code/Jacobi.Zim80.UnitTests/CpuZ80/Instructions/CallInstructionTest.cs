using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.CpuZ80.Opcodes;
using Jacobi.Zim80.CpuZ80.UnitTests;
using System;
using Jacobi.Zim80.UnitTests;
using Jacobi.Zim80.Diagnostics;

namespace Jacobi.Zim80.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class CallInstructionTest
    {
        private const byte AddresssLSB = 0x55;
        private const byte AddresssMSB = 0xAA;

        [TestMethod]
        public void CallNZ_nz()
        {
            var call = OpcodeByte.New(x: 3, z: 4, y: 0);

            var cpu = ExecuteTest(call, (r) => r.Flags.Z = false, alt: false);

            cpu.AssertRegisters(pc: 0xAA55, sp: CpuZ80TestExtensions.MagicValue - 2);
        }

        [TestMethod]
        public void CallNZ_z()
        {
            var call = OpcodeByte.New(x: 3, z: 4, y: 0);
            
            var cpu = ExecuteTest(call, (r) => r.Flags.Z = true, alt: true);

            cpu.AssertRegisters(pc: 3, sp: CpuZ80TestExtensions.MagicValue);
        }

        [TestMethod]
        public void CallZ_z()
        {
            var call = OpcodeByte.New(x: 3, z: 4, y: 1);

            var cpu = ExecuteTest(call, (r) => r.Flags.Z = true, alt: false);

            cpu.AssertRegisters(pc: 0xAA55, sp: CpuZ80TestExtensions.MagicValue - 2);
        }

        [TestMethod]
        public void CallZ_nz()
        {
            var call = OpcodeByte.New(x: 3, z: 4, y: 1);

            var cpu = ExecuteTest(call, (r) => r.Flags.Z = false, alt: true);

            cpu.AssertRegisters(pc: 3, sp: CpuZ80TestExtensions.MagicValue);
        }

        [TestMethod]
        public void CallNC_nc()
        {
            var call = OpcodeByte.New(x: 3, z: 4, y: 2);

            var cpu = ExecuteTest(call, (r) => r.Flags.C = false, alt: false);

            cpu.AssertRegisters(pc: 0xAA55, sp: CpuZ80TestExtensions.MagicValue - 2);
        }

        [TestMethod]
        public void CallNC_c()
        {
            var call = OpcodeByte.New(x: 3, z: 4, y: 2);

            var cpu = ExecuteTest(call, (r) => r.Flags.C = true, alt: true);

            cpu.AssertRegisters(pc: 3, sp: CpuZ80TestExtensions.MagicValue);
        }

        [TestMethod]
        public void CallC_c()
        {
            var call = OpcodeByte.New(x: 3, z: 4, y: 3);

            var cpu = ExecuteTest(call, (r) => r.Flags.C = true, alt: false);

            cpu.AssertRegisters(pc: 0xAA55, sp: CpuZ80TestExtensions.MagicValue - 2);
        }

        [TestMethod]
        public void CallC_nc()
        {
            var call = OpcodeByte.New(x: 3, z: 4, y: 3);

            var cpu = ExecuteTest(call, (r) => r.Flags.C = false, alt: true);

            cpu.AssertRegisters(pc: 3, sp: CpuZ80TestExtensions.MagicValue);
        }

        [TestMethod]
        public void CallPO_np()
        {
            var call = OpcodeByte.New(x: 3, z: 4, y: 4);

            var cpu = ExecuteTest(call, (r) => r.Flags.PV = false, alt: false);

            cpu.AssertRegisters(pc: 0xAA55, sp: CpuZ80TestExtensions.MagicValue - 2);
        }

        [TestMethod]
        public void CallPO_p()
        {
            var call = OpcodeByte.New(x: 3, z: 4, y: 4);

            var cpu = ExecuteTest(call, (r) => r.Flags.PV = true, alt: true);

            cpu.AssertRegisters(pc: 3, sp: CpuZ80TestExtensions.MagicValue);
        }

        [TestMethod]
        public void CallPE_p()
        {
            var call = OpcodeByte.New(x: 3, z: 4, y: 5);

            var cpu = ExecuteTest(call, (r) => r.Flags.PV = true, alt: false);

            cpu.AssertRegisters(pc: 0xAA55, sp: CpuZ80TestExtensions.MagicValue - 2);
        }

        [TestMethod]
        public void CallPE_np()
        {
            var call = OpcodeByte.New(x: 3, z: 4, y: 5);

            var cpu = ExecuteTest(call, (r) => r.Flags.PV = false, alt: true);

            cpu.AssertRegisters(pc: 3, sp: CpuZ80TestExtensions.MagicValue);
        }

        [TestMethod]
        public void CallP_s()
        {
            var call = OpcodeByte.New(x: 3, z: 4, y: 6);

            var cpu = ExecuteTest(call, (r) => r.Flags.S = false, alt: false);

            cpu.AssertRegisters(pc: 0xAA55, sp: CpuZ80TestExtensions.MagicValue - 2);
        }

        [TestMethod]
        public void CallP_ns()
        {
            var call = OpcodeByte.New(x: 3, z: 4, y: 6);

            var cpu = ExecuteTest(call, (r) => r.Flags.S = true, alt: true);

            cpu.AssertRegisters(pc: 3, sp: CpuZ80TestExtensions.MagicValue);
        }

        [TestMethod]
        public void CallM_s()
        {
            var call = OpcodeByte.New(x: 3, z: 4, y: 7);

            var cpu = ExecuteTest(call, (r) => r.Flags.S = true, alt: false);

            cpu.AssertRegisters(pc: 0xAA55, sp: CpuZ80TestExtensions.MagicValue - 2);
        }

        [TestMethod]
        public void CallM_ns()
        {
            var call = OpcodeByte.New(x: 3, z: 4, y: 7);

            var cpu = ExecuteTest(call, (r) => r.Flags.S = false, alt: true);

            cpu.AssertRegisters(pc: 3, sp: CpuZ80TestExtensions.MagicValue);
        }

        [TestMethod]
        public void Call()
        {
            var call = OpcodeByte.New(x: 3, z: 5, q:1, p: 0);

            var cpu = ExecuteTest(call, null);

            cpu.AssertRegisters(pc: 0xAA55, sp:CpuZ80TestExtensions.MagicValue - 2);
        }

        private static CpuZ80 ExecuteTest(OpcodeByte call, 
            Action<Registers> preTest,
            bool alt = false)
        {
            var cpuZ80 = new CpuZ80();
            var model = cpuZ80.Initialize(new[] { call.Value, AddresssLSB, AddresssMSB });
            cpuZ80.FillRegisters();

            if (preTest != null)
            {
                preTest(cpuZ80.Registers);
            }

            model.ClockGen.SquareWave(alt ? 11 : 17);

            Console.WriteLine(model.LogicAnalyzer.ToWaveJson());

            return cpuZ80;
        }
    }
}
