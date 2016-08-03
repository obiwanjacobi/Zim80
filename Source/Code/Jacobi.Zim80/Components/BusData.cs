using System;
using System.Collections.Generic;

namespace Jacobi.Zim80.Components
{
    // immutable
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

        public DigitalLevel Read(int index)
        {
            if (index < 0 || index >= _signals.Count)
                throw new ArgumentOutOfRangeException("index");

            return _signals[index];
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
    }
}
