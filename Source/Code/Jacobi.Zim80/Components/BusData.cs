using System;
using System.Collections.Generic;
using System.Linq;

namespace Jacobi.Zim80.Components
{
    /// <summary>
    /// Immutable base class for bus data objects.
    /// </summary>
    public abstract class BusData
    {
        private readonly List<DigitalLevel> _signals = new List<DigitalLevel>();

        protected BusData(byte width)
        {
            Width = width;
            for (int i = 0; i < width; i++)
                _signals.Add(DigitalLevel.Floating);
        }

        protected void Write(int index, DigitalLevel level)
        {
            _signals[index] = level;
        }

        public int Width { get; private set; }

        public DigitalLevel this[int index]
        {
            get { return Read(index); }
        }

        public IEnumerable<DigitalLevel> Signals
        {
            get { return _signals; }
        }

        public bool IsFloating
        {
            get { return _signals.All(s => s == DigitalLevel.Floating); }
        }

        public DigitalLevel Read(int index)
        {
            if (index < 0 || index >= _signals.Count)
                throw new ArgumentOutOfRangeException("index");

            return _signals[index];
        }

        internal void Write(byte data, int maxWidth = 8)
       {
            ThrowIfMaxWidthOutOfRange(maxWidth, 8);
            Write((UInt32)data, maxWidth);
        }

        internal void Write(UInt16 data, int maxWidth = 16)
        {
            ThrowIfMaxWidthOutOfRange(maxWidth, 16);
            Write((UInt32)data, maxWidth);
        }

        internal void Write(UInt32 data, int maxWidth = 32)
        {
            ThrowIfMaxWidthOutOfRange(maxWidth, 32);
            var width = Math.Min(Width, maxWidth);

            for (int i = 0; i < width; i++)
            {
                Write(i, (data & 0x01) == 0 ? DigitalLevel.Low : DigitalLevel.High);
                data >>= 1;
            }
        }

        public bool Equals(BusData that)
        {
            if (this.Width != that.Width) return false;

            for (int i = 0; i < Width; i++)
            {
                if (this._signals[i] != that._signals[i])
                    return false;
            }

            return true;
        }

        public override bool Equals(object obj)
        {
            var that = obj as BusData;

            if (that != null)
                return Equals(that);

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public byte ToByte(int maxWidth = 8)
        {
            ThrowIfMaxWidthOutOfRange(maxWidth, 8);

            byte data = 0;
            ReadBits(maxWidth, (b) => data |= (byte)b);
            return data;
        }

        public UInt16 ToUInt16(int maxWidth = 16)
        {
            ThrowIfMaxWidthOutOfRange(maxWidth, 16);

            UInt16 data = 0;
            ReadBits(maxWidth, (b) => data |= (UInt16)b);
            return data;
        }

        public UInt32 ToUInt32(int maxWidth = 32)
        {
            ThrowIfMaxWidthOutOfRange(maxWidth, 32);

            UInt32 data = 0;
            ReadBits(maxWidth, (b) => data |= (UInt32)b);
            return data;
        }

        public override string ToString()
        {
            return String.Format("{0:X}", ToUInt32(Width));
        }

        private void ReadBits(int maxWidth, Action<int> write)
        {
            for (int i = 0; i < Width && i < maxWidth; i++)
            {
                var level = Read(i);
                if (level == DigitalLevel.High ||
                    level == DigitalLevel.PosEdge)
                {
                    write((1 << i));
                }
            }
        }

        private void ThrowIfMaxWidthOutOfRange(int maxWidth, int expected)
        {
            if (maxWidth < 0 || maxWidth > expected)
                throw new ArgumentOutOfRangeException("maxWidth", 
                    String.Format("Value expected between 0 and {0}.", expected));
        }
    }
}
