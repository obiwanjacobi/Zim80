using System.Collections.Generic;

namespace Jacobi.Zim80.Logic
{
    public class BusDataStream<DataT>
        where DataT : BusData, new()
    {
        private readonly List<DataT> _samples = new List<DataT>();
        private readonly Bus<DataT> _bus;

        public BusDataStream(Bus<DataT> bus)
        {
            _bus = bus;
        }

        public Bus<DataT> Bus { get { return _bus; } }

        public IEnumerable<DataT> Samples { get { return _samples; } }

        public void Sample()
        {
            _samples.Add(_bus.Value);
        }

        public void Clear()
        {
            _samples.Clear();
        }
    }
}
