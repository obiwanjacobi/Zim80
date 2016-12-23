using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jacobi.Zim80
{
    public class BusDataBuffer : IEnumerable<BusData>
    {
        private readonly List<BusData> _buffer = new List<BusData>();
        private int _readIndex;

        public bool IsMixedWidth
        {
            get { return MinWidth != MaxWidth; }
        }

        public int Count
        {
            get { return _buffer.Count; }
        }

        public int MinWidth
        {
            get { return _buffer.Min(d => d.Width); }
        }

        public int MaxWidth
        {
            get { return _buffer.Max(d => d.Width); }
        }

        public void Write(BusData data)
        {
            _buffer.Add(data);
        }

        public void Write(IEnumerable<BusData> data)
        {
            _buffer.AddRange(data);
        }

        public void Write(IEnumerable<byte> data)
        {
            foreach (var b in data)
            {
                _buffer.Add(new BusData8(b));
            }
        }

        public BusData Read()
        {
            if (_readIndex < _buffer.Count)
                return _buffer[_readIndex++];

            return null;
        }

        public void Clear()
        {
            _buffer.Clear();
        }

        public override string ToString()
        {
            var bytes = _buffer.Select((s) => s.ToByte()).Where(c => c != 0);
            return Encoding.UTF8.GetString(bytes.ToArray());
        }

        public IEnumerator<BusData> GetEnumerator()
        {
            return _buffer.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
