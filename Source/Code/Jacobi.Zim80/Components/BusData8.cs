using System;

namespace Jacobi.Zim80.Components
{
    public class BusData8 : BusData
    {
        public const int BusWidth = 8;

        public BusData8()
            : base(BusWidth)
        { }

        public BusData8(DigitalLevel level7, DigitalLevel level6,
            DigitalLevel level5, DigitalLevel level4, DigitalLevel level3,
            DigitalLevel level2, DigitalLevel level1, DigitalLevel level0)
            : base(BusWidth)
        {
            Write(7, level7);
            Write(6, level6);
            Write(5, level5);
            Write(4, level4);
            Write(3, level3);
            Write(2, level2);
            Write(1, level1);
            Write(0, level0);
        }

        public BusData8(byte data)
            : base(BusWidth)
        {
            Write(data);
        }

        public byte ToByte()
        {
            byte data = 0;

            for (int i = 0; i < BusWidth; i++)
            {
                var level = Read(i);
                if (level == DigitalLevel.High ||
                    level == DigitalLevel.PosEdge)
                {
                    data |= (byte)(1 << i);
                }
            }

            return data;
        }

        internal void Write(byte data)
        {
            for (int i = 0; i < BusWidth; i++)
            {
                Write(i, (data & 0x01) == 0 ? DigitalLevel.Low : DigitalLevel.High);
                data >>= 1;
            }
        }
    }
}
