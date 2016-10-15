using System;

namespace Jacobi.Zim80.Components.CpuZ80
{
    internal class Alu
    {
        private readonly Registers.RegisterSet _primarySet;

        public Alu(Registers.RegisterSet primarySet)
        {
            _primarySet = primarySet;
        }

        private Registers.Flags Flags
        {
            get { return _primarySet.Flags; }
        }

        public ushort Inc16(ushort value)
        {
            var newValue = (ushort)(value + 1);

            // no flags affected

            return newValue;
        }

        

        // Y/X flags not supported for (HL) and IX/IY +d
        // see undocumented (p15)
        public void TestBit(byte bit, byte value)
        {
            var isSet = (value & (1 << bit)) != 0;

            Flags.S = bit == 7 && isSet;
            Flags.Z = !isSet;
            Flags.Y = bit == 5 && isSet;
            Flags.H = true;
            Flags.X = bit == 3 && isSet;
            Flags.PV = !isSet;
            Flags.N = true;
        }

        public byte ResetBit(byte bit, byte value)
        {
            return (byte)(value & ~(1 << bit));
        }

        public byte SetBit(byte bit, byte value)
        {
            return (byte)(value | (1 << bit));
        }

        public ushort Dec16(ushort value)
        {
            var newValue = (ushort)(value - 1);

            // no flags affected

            return newValue;
        }

        public byte Inc8(byte value)
        {
            var newValue = (byte)(value + 1);

            Flags.S = IsNegative(newValue);
            Flags.PV = (value == 0x7E);
            Flags.Z = (newValue == 0);
            Flags.H = HalfCarryFromLo(value, newValue);
            Flags.N = false;

            return newValue;
        }

        public byte Dec8(byte value)
        {
            var newValue = (byte)(value - 1);

            Flags.S = IsNegative(newValue);
            Flags.PV = (value == 0x80);
            Flags.Z = (newValue == 0);
            Flags.H = HalfCarryFromHi(value, newValue);
            Flags.N = true;

            return newValue;
        }

        public ushort Add16(ushort value1, ushort value2, bool addCarry = false)
        {
            int newValue = (int)value1 + (int)value2;

            if (addCarry && Flags.C) newValue++;

            Flags.C = newValue > 0xFFFF;
            //Flags.H = HalfCarryFromLo(); HOW to detect carry from bit11->bit12?
            Flags.N = false;

            return (ushort)newValue;
        }

        private static bool IsNegative(byte value)
        {
            return (value & 0x80) > 0;
        }

        private static bool HalfCarryFromHi(byte beforeValue, byte afterValue)
        {
            return (beforeValue & 0xF0) > (afterValue & 0xF0);
        }

        private static bool HalfCarryFromLo(byte beforeValue, byte afterValue)
        {
            return (beforeValue & 0xF0) < (afterValue & 0xF0);
        }

        public static UInt16 Add(UInt16 nn, sbyte d)
        {
            var value = (int)nn;
            value += d;
            return (UInt16)value;
        }
    }
}
