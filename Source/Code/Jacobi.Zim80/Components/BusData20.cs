namespace Jacobi.Zim80.Components
{
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
}
