namespace Jacobi.Zim80
{
    public class BusData4 : BusData
    {
        public const int BusWidth = 4;

        public BusData4()
            : base(BusWidth)
        { }

        public BusData4(byte data)
            : base(BusWidth)
        {
            Write(data);
        }
    }

    public class BusData8 : BusData
    {
        public const int BusWidth = 8;

        public BusData8()
            : base(BusWidth)
        { }

        public BusData8(byte data)
            : base(BusWidth)
        {
            Write(data);
        }
    }

    public class BusData10 : BusData
    {
        public const int BusWidth = 10;

        public BusData10()
            : base(BusWidth)
        { }

        public BusData10(ushort data)
            : base(BusWidth)
        {
            Write(data);
        }
    }

    public class BusData12 : BusData
    {
        public const int BusWidth = 12;

        public BusData12()
            : base(BusWidth)
        { }

        public BusData12(ushort data)
            : base(BusWidth)
        {
            Write(data);
        }
    }

    public class BusData14 : BusData
    {
        public const int BusWidth = 14;

        public BusData14()
            : base(BusWidth)
        { }

        public BusData14(ushort data)
            : base(BusWidth)
        {
            Write(data);
        }
    }

    public class BusData16 : BusData
    {
        public const int BusWidth = 16;

        public BusData16()
            : base(BusWidth)
        { }

        public BusData16(ushort data)
            : base(BusWidth)
        {
            Write(data);
        }
    }

    public class BusData20 : BusData
    {
        public const int BusWidth = 20;

        public BusData20()
            : base(BusWidth)
        { }

        public BusData20(uint data)
            : base(BusWidth)
        {
            Write(data);
        }
    }

    public class BusData24 : BusData
    {
        public const int BusWidth = 24;

        public BusData24()
            : base(BusWidth)
        { }

        public BusData24(uint data)
            : base(BusWidth)
        {
            Write(data);
        }
    }
}
