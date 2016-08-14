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

        public byte Dec8(byte value)
        {
            value--;
            _primarySet.Flags.Z = (value == 0);
            return value;
        }

        public static UInt16 Add(UInt16 nn, sbyte d)
        {
            var value = (int)nn;
            value += d;
            return (UInt16)value;
        }
    }
}
