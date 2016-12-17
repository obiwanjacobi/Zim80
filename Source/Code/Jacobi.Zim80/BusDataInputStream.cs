using System.Collections.Generic;

namespace Jacobi.Zim80
{
    public class BusDataInputStream
    {
        private readonly BusDataBuffer _buffer = new BusDataBuffer();
        private readonly Bus _bus;

        public BusDataInputStream(Bus bus)
        {
            _bus = bus;
        }

        public Bus Bus { get { return _bus; } }

        public IEnumerable<BusData> Data { get { return _buffer; } }

        public void Sample()
        {
            _buffer.Write(_bus.Value);
        }

        public void Clear()
        {
            _buffer.Clear();
        }

        internal void AddFloating()
        {
            _buffer.Write(new BusData(_bus.BusWidth));
        }

        public override string ToString()
        {
            return _buffer.ToString();
        }
    }
}
