using System;
using Jacobi.Zim80.CpuZ80.Opcodes;

namespace Jacobi.Zim80.CpuZ80
{
    public class Registers : RegisterSet
    {
        internal Registers()
        {
            AlternateSet = new RegisterSet();
        }

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
                    case Register16Table.SP:
                        return SP;
                }

                throw new ArgumentException();
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
                    case Register16Table.SP:
                        SP = value;
                        break;
                }
            }
        }

        private readonly InterruptRegisters _interrupt = new InterruptRegisters();

        public InterruptRegisters Interrupt
        { get { return _interrupt; } }
    }
}