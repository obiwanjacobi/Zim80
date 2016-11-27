namespace Jacobi.Zim80.CpuZ80
{
    public class RegisterFlags
    {
        private readonly Register16 _af;

        internal RegisterFlags(Register16 af)
        {
            _af = af;
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

        // undocumented: copy of bit5 of result
        public bool Y
        {
            get { return IsBitSet(5); }
            set { SetBit(5, value); }
        }

        // half-carry
        public bool H
        {
            get { return IsBitSet(4); }
            set { SetBit(4, value); }
        }

        // undocumented: copy of bit3 of result
        public bool X
        {
            get { return IsBitSet(3); }
            set { SetBit(3, value); }
        }

        // parity/overflow
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
    }
}
