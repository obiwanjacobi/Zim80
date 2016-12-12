using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jacobi.Zim80.Logic
{
    public class BusDataStream
    {
        private readonly List<BusData> _samples = new List<BusData>();
        private readonly Bus _bus;

        public BusDataStream(Bus bus)
        {
            _bus = bus;
        }

        public Bus Bus { get { return _bus; } }

        public IEnumerable<BusData> Samples { get { return _samples; } }

        public void Sample()
        {
            _samples.Add(_bus.Value);
        }

        public void Clear()
        {
            _samples.Clear();
        }

        internal void AddFloating()
        {
            _samples.Add(new BusData(_bus.BusWidth));
        }

        public override string ToString()
        {
            var bytes = _samples.Select((s) => s.ToByte()).Where(c => c != 0);
            return Encoding.UTF8.GetString(bytes.ToArray());
        }
    }
}
