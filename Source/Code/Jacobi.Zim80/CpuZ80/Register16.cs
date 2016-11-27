using System;

namespace Jacobi.Zim80.CpuZ80
{
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
        { _val16 = (UInt16)((_val16 & 0x00FF) | ((int)value << 8)); }
        public byte GetHi()
        { return (byte)((_val16 & 0xFF00) >> 8); }
    }
}
