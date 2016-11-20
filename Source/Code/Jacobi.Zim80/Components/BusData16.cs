namespace Jacobi.Zim80.Components
{
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
}
