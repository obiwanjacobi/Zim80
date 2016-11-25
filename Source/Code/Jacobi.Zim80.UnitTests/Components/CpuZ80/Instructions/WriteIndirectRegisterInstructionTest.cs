using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using Jacobi.Zim80.Components.CpuZ80.UnitTests;
using Jacobi.Zim80.Model;
using Jacobi.Zim80.Components.Memory.UnitTests;

namespace Jacobi.Zim80.Components.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class WriteIndirectRegisterInstructionTest
    {
        private const ushort Address = 0x0005;
        private const sbyte Offset = -1;
        private const byte Value = 0xAA;

        [TestMethod]
        public void Ld_BC_A()
        {
            var ob = OpcodeByte.New(z: 2, q: 0, p: 0);

            var model = ExecuteTest(ob, (cpu) => {
                cpu.Registers.BC = Address;
                cpu.Registers.A = Value;
                });

            model.Memory.Assert(Address, Value);
        }

        [TestMethod]
        public void Ld_DE_A()
        {
            var ob = OpcodeByte.New(z: 2, q: 0, p: 1);

            var model = ExecuteTest(ob, (cpu) => {
                cpu.Registers.DE = Address;
                cpu.Registers.A = Value;
            });

            model.Memory.Assert(Address, Value);
        }

        [TestMethod]
        public void Ld_HL_B()
        {
            var ob = OpcodeByte.New(x: 1, z: 0, y: 6);

            var model = ExecuteTest(ob, (cpu) => {
                cpu.Registers.HL = Address;
                cpu.Registers.B = Value;
            });

            model.Memory.Assert(Address, Value);
        }

        [TestMethod]
        public void Ld_HL_C()
        {
            var ob = OpcodeByte.New(x: 1, z: 1, y: 6);

            var model = ExecuteTest(ob, (cpu) => {
                cpu.Registers.HL = Address;
                cpu.Registers.C = Value;
            });

            model.Memory.Assert(Address, Value);
        }

        [TestMethod]
        public void Ld_HL_D()
        {
            var ob = OpcodeByte.New(x: 1, z: 2, y: 6);

            var model = ExecuteTest(ob, (cpu) => {
                cpu.Registers.HL = Address;
                cpu.Registers.D = Value;
            });

            model.Memory.Assert(Address, Value);
        }

        [TestMethod]
        public void Ld_HL_E()
        {
            var ob = OpcodeByte.New(x: 1, z: 3, y: 6);

            var model = ExecuteTest(ob, (cpu) => {
                cpu.Registers.HL = Address;
                cpu.Registers.E = Value;
            });

            model.Memory.Assert(Address, Value);
        }

        [TestMethod]
        public void Ld_HL_H()
        {
            var ob = OpcodeByte.New(x: 1, z: 4, y: 6);

            var model = ExecuteTest(ob, (cpu) => {
                cpu.Registers.HL = Address;
            });

            model.Memory.Assert(Address, 0x00);
        }

        [TestMethod]
        public void Ld_HL_L()
        {
            var ob = OpcodeByte.New(x: 1, z: 5, y: 6);

            var model = ExecuteTest(ob, (cpu) => {
                cpu.Registers.HL = Address;
            });

            model.Memory.Assert(Address, (byte)Address);
        }

        [TestMethod]
        public void Ld_HL_A()
        {
            var ob = OpcodeByte.New(x: 1, z: 7, y: 6);

            var model = ExecuteTest(ob, (cpu) => {
                cpu.Registers.HL = Address;
                cpu.Registers.A = Value;
            });

            model.Memory.Assert(Address, Value);
        }

        [TestMethod]
        public void Ld_IXd_B()
        {
            var ob = OpcodeByte.New(x: 1, z: 0, y: 6);

            var model = ExecuteTest(ob, (cpu) => {
                cpu.Registers.IX = Address;
                cpu.Registers.B = Value;
            }, 0xDD);

            model.Memory.Assert(Address + Offset, Value);
        }

        [TestMethod]
        public void Ld_IXd_C()
        {
            var ob = OpcodeByte.New(x: 1, z: 1, y: 6);

            var model = ExecuteTest(ob, (cpu) => {
                cpu.Registers.IX = Address;
                cpu.Registers.C = Value;
            }, 0xDD);

            model.Memory.Assert(Address + Offset, Value);
        }

        [TestMethod]
        public void Ld_IXd_D()
        {
            var ob = OpcodeByte.New(x: 1, z: 2, y: 6);

            var model = ExecuteTest(ob, (cpu) => {
                cpu.Registers.IX = Address;
                cpu.Registers.D = Value;
            }, 0xDD);

            model.Memory.Assert(Address + Offset, Value);
        }

        [TestMethod]
        public void Ld_IXd_E()
        {
            var ob = OpcodeByte.New(x: 1, z: 3, y: 6);

            var model = ExecuteTest(ob, (cpu) => {
                cpu.Registers.IX = Address;
                cpu.Registers.E = Value;
            }, 0xDD);

            model.Memory.Assert(Address + Offset, Value);
        }

        [TestMethod]
        public void Ld_IXd_H()
        {
            var ob = OpcodeByte.New(x: 1, z: 4, y: 6);

            var model = ExecuteTest(ob, (cpu) => {
                cpu.Registers.IX = Address;
                cpu.Registers.H = Value;
            }, 0xDD);

            model.Memory.Assert(Address + Offset, Value);
        }

        [TestMethod]
        public void Ld_IXd_L()
        {
            var ob = OpcodeByte.New(x: 1, z: 5, y: 6);

            var model = ExecuteTest(ob, (cpu) => {
                cpu.Registers.IX = Address;
                cpu.Registers.L = Value;
            }, 0xDD);

            model.Memory.Assert(Address + Offset, Value);
        }

        [TestMethod]
        public void Ld_IXd_A()
        {
            var ob = OpcodeByte.New(x: 1, z: 7, y: 6);

            var model = ExecuteTest(ob, (cpu) => {
                cpu.Registers.IX = Address;
                cpu.Registers.A = Value;
            }, 0xDD);

            model.Memory.Assert(Address + Offset, Value);
        }

        [TestMethod]
        public void Ld_IYd_B()
        {
            var ob = OpcodeByte.New(x: 1, z: 0, y: 6);

            var model = ExecuteTest(ob, (cpu) => {
                cpu.Registers.IY = Address;
                cpu.Registers.B = Value;
            }, 0xFD);

            model.Memory.Assert(Address + Offset, Value);
        }

        [TestMethod]
        public void Ld_IYd_C()
        {
            var ob = OpcodeByte.New(x: 1, z: 1, y: 6);

            var model = ExecuteTest(ob, (cpu) => {
                cpu.Registers.IY = Address;
                cpu.Registers.C = Value;
            }, 0xFD);

            model.Memory.Assert(Address + Offset, Value);
        }

        [TestMethod]
        public void Ld_IYd_D()
        {
            var ob = OpcodeByte.New(x: 1, z: 2, y: 6);

            var model = ExecuteTest(ob, (cpu) => {
                cpu.Registers.IY = Address;
                cpu.Registers.D = Value;
            }, 0xFD);

            model.Memory.Assert(Address + Offset, Value);
        }

        [TestMethod]
        public void Ld_IYd_E()
        {
            var ob = OpcodeByte.New(x: 1, z: 3, y: 6);

            var model = ExecuteTest(ob, (cpu) => {
                cpu.Registers.IY = Address;
                cpu.Registers.E = Value;
            }, 0xFD);

            model.Memory.Assert(Address + Offset, Value);
        }

        [TestMethod]
        public void Ld_IYd_H()
        {
            var ob = OpcodeByte.New(x: 1, z: 4, y: 6);

            var model = ExecuteTest(ob, (cpu) => {
                cpu.Registers.IY = Address;
                cpu.Registers.H = Value;
            }, 0xFD);

            model.Memory.Assert(Address + Offset, Value);
        }

        [TestMethod]
        public void Ld_IYd_L()
        {
            var ob = OpcodeByte.New(x: 1, z: 5, y: 6);

            var model = ExecuteTest(ob, (cpu) => {
                cpu.Registers.IY = Address;
                cpu.Registers.L = Value;
            }, 0xFD);

            model.Memory.Assert(Address + Offset, Value);
        }

        [TestMethod]
        public void Ld_IYd_A()
        {
            var ob = OpcodeByte.New(x: 1, z: 7, y: 6);

            var model = ExecuteTest(ob, (cpu) => {
                cpu.Registers.IY = Address;
                cpu.Registers.A = Value;
            }, 0xFD);

            model.Memory.Assert(Address + Offset, Value);
        }

        private static SimulationModel ExecuteTest(OpcodeByte ob, 
            Action<CpuZ80> preTest, byte extension = 0)
        {
            var cpuZ80 = new CpuZ80();
            var buffer = extension == 0 ? new byte[] { ob.Value, 0, 0, 0, 0, 0 } :
                            new byte[] { extension, ob.Value, unchecked((byte)Offset), 0, 0 };
            var model = cpuZ80.Initialize(buffer);

            cpuZ80.FillRegisters();
            preTest(cpuZ80);

            model.ClockGen.SquareWave(extension == 0 ? 7 : 19);

            return model;
        }
    }
}
