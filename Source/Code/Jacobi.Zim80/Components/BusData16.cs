using System;

namespace Jacobi.Zim80.Components
{
    public class BusData16 : BusData
    {
        public const int BusWidth = 16;

        public BusData16()
            : base(BusWidth)
        { }

        public BusData16(UInt16 data)
            : base(BusWidth)
        {
            for (int i = 0; i < BusWidth; i++)
            {
                Write(i, (data & 0x01) == 0 ? DigitalLevel.Low : DigitalLevel.High);
                data >>= 1;
            }
        }

        public UInt16 ToUInt16()
        {
            UInt16 data = 0;

            for (int i = 0; i < BusWidth; i++)
            {
                var level = Read(i);
                if (level == DigitalLevel.High ||
                    level == DigitalLevel.PosEdge)
                {
                    data |= (UInt16)(1 << i);
                }
            }

            return data;
        }
    }
}
