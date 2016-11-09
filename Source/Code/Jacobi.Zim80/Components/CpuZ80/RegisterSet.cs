using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using System;

namespace Jacobi.Zim80.Components.CpuZ80
{
    public class RegisterSet
    {
        private Register16 _af = new Register16();
        private Register16 _bc = new Register16();
        private Register16 _de = new Register16();
        private Register16 _hl = new Register16();

        private readonly RegisterFlags _flags;

        public RegisterSet()
        {
            _flags = new RegisterFlags(_af);
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

        public RegisterFlags Flags
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
                        throw new InvalidOperationException("Invalid Register index.");
                }
            }
        }


    }
}
