using System;

namespace Jacobi.Zim80.Components.CpuZ80.Opcodes
{
    public class OpcodeByte
    {
        public OpcodeByte(byte opcode)
        {
            Value = opcode;
        }

        public byte Value { get; private set; }

        public byte X
        {
            get { return (byte)((Value >> 6) & 0x03); }
        }

        public byte Y
        {
            get { return (byte)((Value >> 3) & 0x07); }
        }

        public byte Z
        {
            get { return (byte)(Value & 0x07); }
        }

        public byte P
        {
            get { return (byte)((Value >> 3) & 0x01); }
        }

        public byte Q
        {
            get { return (byte)((Value >> 4) & 0x03); }
        }

        public bool IsExtension
        {
            get { return IsCB || IsED || IsDD || IsFD; }
        }

        public bool IsCB
        {
            get { return Value == 0xCB; }
        }

        public bool IsED
        {
            get { return Value == 0xED; }
        }

        public bool IsDD
        {
            get { return Value == 0xDD; }
        }

        public bool IsFD
        {
            get { return Value == 0xFD; }
        }

        private static readonly OpcodeByte _empty = new OpcodeByte(0);

        public static OpcodeByte Empty { get { return _empty; } }

        public static OpcodeByte New(byte z = 0, byte x = 0, byte p = 0, byte q  = 0)
        {
            return new OpcodeByte((byte)((x << 6) | (p << 4) | (q << 3) | z));
        }

        public static OpcodeByte New(byte z = 0, byte x = 0, byte y = 0)
        {
            return new OpcodeByte((byte)((x << 6) | (y << 3) | z));
        }

        public static UInt16 MakeUInt16(OpcodeByte lsb, OpcodeByte msb)
        {
            return (UInt16)((msb.Value << 8) | lsb.Value);
        }

        public override string ToString()
        {
            return string.Format("{0} ({0:X}h) X={1}, Z={3}, Y={2}, Q={4}, P={5}", Value, X, Y, Z, P, Q);
        }
    }
}
