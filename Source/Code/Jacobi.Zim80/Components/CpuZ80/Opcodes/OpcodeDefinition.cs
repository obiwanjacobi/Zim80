using Jacobi.Zim80.Components.CpuZ80.States.Instructions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Jacobi.Zim80.Components.CpuZ80.Opcodes
{
    [DebuggerDisplay("{Mnemonic} ({Value})")]
    public partial class OpcodeDefinition
    {
        public const byte NotInUse = 0xFF;

        private OpcodeDefinition()
        { }

        public OpcodeDefinition(byte x, byte y, byte z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public OpcodeDefinition(byte x, byte p, byte q, byte z)
        {
            X = x;
            P = p;
            Q = q;
            Z = z;
        }

        private byte _x = NotInUse;

        public byte X
        {
            get { return _x; }
            private set
            {
                if (value > 3) throw new ArgumentOutOfRangeException();
                _x = value;
            }
        }

        private byte _y = NotInUse;

        public byte Y
        {
            get { return _y; }
            private set
            {
                if (value > 7) throw new ArgumentOutOfRangeException();

                _y = value;
                _p = (byte)((value >> 1) & 0x03);
                _q = (byte)(value & 0x01);
            }
        }

        private byte _z = NotInUse;

        public byte Z
        {
            get { return _z; }
            private set
            {
                if (value > 7) throw new ArgumentOutOfRangeException();
                _z = value;
            }
        }

        private byte _p = NotInUse;

        public byte P
        {
            get { return _p; }
            private set
            {
                if (value > 3) throw new ArgumentOutOfRangeException();

                _p = value;
                _y &= 0x01;
                _y |= (byte)(value << 1);
            }
        }

        private byte _q = NotInUse;

        public byte Q
        {
            get { return _q; }
            private set
            {
                if (value > 1) throw new ArgumentOutOfRangeException();

                _q = value;
                _y &= 0x06;
                _y |= value;
            }
        }

        public string Mnemonic { get; private set; }

        // parameters
        public bool HasParameters { get { return d || n || nn; } }
        public int ParameterCount
        {
            get
            {
                int count = 0;
                if (d) count++;
                if (n) count++;
                if (nn) count = 2;

                return count;
            }
        }
        public bool d { get; private set; }
        public bool n { get; private set; }
        public bool nn { get; private set; }

        // extensions
        public bool HasExtension { get { return Ext1 != 0; } }
        public byte Ext1 { get; private set; }
        public byte Ext2 { get; private set; }
        public bool IsIX { get { return Ext1 == 0xDD; } }
        public bool IsIY { get { return Ext1 == 0xFD; } }

        // T-cycle counts per machine cycle
        public int[] Cycles { get; private set; }
        // alternates for conditional branching
        public int[] AltCycles { get; private set; }

        // the state object for the instruction
        public Type Instruction { get; private set; }

        // typed aspects of opcode
        public Register16Table Register16FromP { get { return (Register16Table)P; } }
        public Register8Table Register8FromY { get { return (Register8Table)Y; } }
        public Register8Table Register8FromZ { get { return (Register8Table)Z; } }
        public MathOperations MathOperationFromY { get { return (MathOperations)Y; } }

        public byte Value
        {
            get { return (byte)((X << 6) | (Y << 3) | Z); }
        }

        public bool IsEqualTo(OpcodeByte opcodeByte)
        {
            if (opcodeByte == null) return false;
            return Value == opcodeByte.Value;
        }

        public override string ToString()
        {
            return Mnemonic;
        }

        public static OpcodeDefinition Find(OpcodeByte opcode,
            OpcodeByte ext1 = null, OpcodeByte ext2 = null)
        {
            var result = FindInternal(opcode, ext1, ext2);

            if (!result.Any() && 
                ext1 != null && ext1.Value != 0)
            {
                // shift extensions 
                result = FindInternal(opcode, ext2, OpcodeByte.Empty);
            }

            return result.SingleOrDefault();
        }

        private static IEnumerable<OpcodeDefinition> FindInternal(OpcodeByte opcode, OpcodeByte ext1, OpcodeByte ext2)
        {
            return (from od in Defintions
                    where (ext1 == null && od.Ext1 == 0) || ext1 != null && od.Ext1 == ext1.Value
                    where (ext2 == null && od.Ext2 == 0) || ext2 != null && od.Ext2 == ext2.Value
                    where od.IsEqualTo(opcode)
                    select od);
        }

        // interrupt definitions
        private static readonly OpcodeDefinition[] Interrupts = new OpcodeDefinition[]
        {
            // X = unused
            new OpcodeDefinition { Z = 0, Y = 0, Mnemonic = "NMI", Cycles = new[] { 5, 3, 3 }, Instruction = typeof(NmiInterrupt) },
            new OpcodeDefinition { Z = 1, Y = 0, Mnemonic = "INT0", Cycles = new[] { 7, 3, 3 }, Instruction = typeof(Interrupt) },
            new OpcodeDefinition { Z = 1, Y = 1, Mnemonic = "INT1", Cycles = new[] { 7, 3, 3 }, Instruction = typeof(Interrupt) },
            new OpcodeDefinition { Z = 1, Y = 2, Mnemonic = "INT2", Cycles = new[] { 7, 3, 3, 3, 3 }, Instruction = typeof(Interrupt) },
        };

        internal static OpcodeDefinition GetNmiDefinition()
        {
            return Interrupts[0];
        }

        internal static OpcodeDefinition GetInterruptDefinition(InterruptModes intMode)
        {
            return Interrupts[(int)intMode + 1];
        }
    }
}
