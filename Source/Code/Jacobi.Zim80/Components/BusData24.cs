namespace Jacobi.Zim80.Components
{
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
