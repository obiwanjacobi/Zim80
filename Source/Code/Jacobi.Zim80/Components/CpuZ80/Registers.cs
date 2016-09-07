using System;
using Jacobi.Zim80.Components.CpuZ80.Opcodes;

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

        private readonly Register16 _pc = new Register16();
        public UInt16 PC
        {
            get { return _pc.Value; }
            set { _pc.Value = value; }
        }
        public Register16 GetPC()
        {
            return _pc;
        }
    
        public UInt16 SP { get; set; }

        private readonly Register16 _ix = new Register16();
        internal Register16 GetIX()
        {
            return _ix;
        }
        public UInt16 IX
        {
            get { return _ix.Value; }
            set { _ix.Value = value; }
        }

        private readonly Register16 _iy = new Register16();
        internal Register16 GetIY()
        {
            return _iy;
        }
        public UInt16 IY
        {
            get { return _iy.Value; }
            set { _iy.Value = value; }
        }

        private readonly Register16 _ir = new Register16();
        internal UInt16 IR
        {
            get { return _ir.Value; }
        }
        public byte I
        {
            get { return _ir.GetHi(); }
            set { _ir.SetHi(value); }
        }
        public byte R
        {
            get { return _ir.GetLo(); }
            set { _ir.SetLo(value); }
        }

        // interrupt mode flip-flops
        public bool IFF1 { get; set; }
        public bool IFF2 { get; set; }

        public class RegisterSet
        {
            private Register16 _af = new Register16();
            private Register16 _bc = new Register16();
            private Register16 _de = new Register16();
            private Register16 _hl = new Register16();

            private readonly Flags _flags;

            public RegisterSet()
            {
                _flags = new Flags(_af);
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
                get { return _af.Value; }
                set { _af.Value = value; }
            }
            public UInt16 BC
            {
                get { return _bc.Value; }
                set { _bc.Value = value; }
            }
            public UInt16 DE
            {
                get { return _de.Value; }
                set { _de.Value = value; }
            }
            public UInt16 HL
            {
                get { return _hl.Value; }
                set { _hl.Value = value; }
            }

            public Flags Flags
            {
                get { return _flags; }
            }

            public byte this[Register8Table register]
            {
                get
                {
                    switch (register)
                    {
                        case Register8Table.B:
                            return B;
                        case Register8Table.C:
                            return C;
                        case Register8Table.D:
                            return D;
                        case Register8Table.E:
                            return E;
                        case Register8Table.H:
                            return H;
                        case Register8Table.L:
                            return L;
                        case Register8Table.A:
                            return A;
                        default:
                            throw new InvalidOperationException();
                    }
                }
                set
                {
                    switch (register)
                    {
                        case Register8Table.B:
                            B = value;
                            break;
                        case Register8Table.C:
                            C = value;
                            break;
                        case Register8Table.D:
                            D = value;
                            break;
                        case Register8Table.E:
                            E = value;
                            break;
                        case Register8Table.H:
                            H = value;
                            break;
                        case Register8Table.L:
                            L = value;
                            break;
                        case Register8Table.A:
                            A = value;
                            break;
                        default:
                            throw new InvalidOperationException();
                    }
                }
            }

            public ushort this[Register16Table register]
            {
                get
                {
                    switch (register)
                    {
                        case Register16Table.BC:
                            return BC;
                        case Register16Table.DE:
                            return DE;
                        case Register16Table.HL:
                            return HL;
                    }

                    throw new InvalidOperationException();
                }
                set
                {
                    switch (register)
                    {
                        case Register16Table.BC:
                            BC = value;
                            break;
                        case Register16Table.DE:
                            DE = value;
                            break;
                        case Register16Table.HL:
                            HL = value;
                            break;
                        default:
                            throw new InvalidOperationException();
                    }
                }
            }
        }
        

        public class Flags
        {
            private readonly Register16 _af;
            internal Flags(Register16 af)
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

        public class Register16
        {
            private UInt16 _val16;
            public UInt16 Value
            {
                get { return _val16; }
                set { _val16 = value; }
            }

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