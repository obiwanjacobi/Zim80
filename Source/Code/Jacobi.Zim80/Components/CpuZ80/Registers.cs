using System;

namespace Jacobi.Zim80.Components.CpuZ80
{
    public class Registers
    {
        internal Registers()
        {
            PrimarySet = new RegisterSet();
            AlternateSet = new RegisterSet();
        }

        public RegisterSet PrimarySet { get; private set; }
        public RegisterSet AlternateSet { get; private set; }

        public UInt16 PC { get; set; }
        public UInt16 SP { get; set; }
        public UInt16 IX { get; set; }
        public UInt16 IY { get; set; }
        public byte I { get; set; }
        public byte R { get; set; }

        // interrupt mode flip-flops
        public bool IFF1 { get; set; }
        public bool IFF2 { get; set; }

        public class RegisterSet
        {
            private Register16 _af;
            private Register16 _bc;
            private Register16 _de;
            private Register16 _hl;

            private readonly Flags _flags;

            public RegisterSet()
            {
                _flags = new Flags(ref _af);
            }

            public byte A
            {
                get { return _af.GetHi(); }
                set { _af.SetHi(value); }
            }
            public byte B
            {
                get { return _bc.GetHi(); }
                set { _bc.SetHi(value); }
            }
            public byte C
            {
                get { return _bc.GetLo(); }
                set { _bc.SetLo(value); }
            }
            public byte D
            {
                get { return _de.GetHi(); }
                set { _de.SetHi(value); }
            }
            public byte E
            {
                get { return _de.GetLo(); }
                set { _de.SetLo(value); }
            }
            public byte F
            {
                get { return _af.GetLo(); }
                set { _af.SetLo(value); }
            }
            public byte H
            {
                get { return _hl.GetHi(); }
                set { _hl.SetHi(value); }
            }
            public byte L
            {
                get { return _hl.GetLo(); }
                set { _hl.SetLo(value); }
            }

            public UInt16 AF
            {
                get { return _af.Get(); }
                set { _af.Set(value); }
            }
            public UInt16 BC
            {
                get { return _bc.Get(); }
                set { _bc.Set(value); }
            }
            public UInt16 DE
            {
                get { return _de.Get(); }
                set { _de.Set(value); }
            }
            public UInt16 HL
            {
                get { return _hl.Get(); }
                set { _hl.Set(value); }
            }

            public Flags Flags
            {
                get { return _flags; }
            }
        }

        public class Flags
        {
            private readonly Register16 _af;
            internal Flags(ref Register16 af)
            {
                _af = af;
            }

            private bool IsBitSet(int bitNo)
            {
                return (_af.GetLo() & (1 << bitNo)) > 0;
            }
            private void SetBit(int bitNo, bool value)
            {
                var flags = _af.GetLo();
                var mask = (1 << bitNo);

                if (value)
                    flags |= (byte)mask;
                else
                    flags &= (byte)~mask;

                _af.SetLo(flags);
            }

            // sign
            public bool S
            {
                get { return IsBitSet(7); }
                set { SetBit(7, value); }
            }

            // zero
            public bool Z
            {
                get { return IsBitSet(6); }
                set { SetBit(6, value); }
            }

            // half-carry
            public bool H
            {
                get { return IsBitSet(4); }
                set { SetBit(4, value); }
            }

            // parity/
            public bool PV
            {
                get { return IsBitSet(2); }
                set { SetBit(2, value); }
            }

            // negative
            public bool N
            {
                get { return IsBitSet(1); }
                set { SetBit(1, value); }
            }

            // carry
            public bool C
            {
                get { return IsBitSet(0); }
                set { SetBit(0, value); }
            }
        }

        internal struct Register16
        {
            private UInt16 _val16;

            public void Set(UInt16 value)
            { _val16 = value; }
            public UInt16 Get()
            { return _val16; }

            public void SetLo(byte value)
            { _val16 = (UInt16)((_val16 & 0xFF00) | value); }
            public byte GetLo()
            { return (byte)(_val16 & 0x00FF); }

            public void SetHi(byte value)
            { _val16 = (UInt16)((_val16 & 0x00FF) | ((int)value << 8) ); }
            public byte GetHi()
            { return (byte)((_val16 & 0xFF00) >> 8); }
        }
    }
}