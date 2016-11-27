namespace Jacobi.Zim80
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
    }
}
