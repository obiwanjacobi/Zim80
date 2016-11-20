using System;
using System.Collections;
using System.Collections.Generic;

namespace Jacobi.Zim80
{
    public class DigitalLevelCycler : IEnumerable<DigitalLevel>
    {
        private readonly DigitalLevel? _startLevel = null;
        private readonly DigitalLevel? _endLevel = null;
        private bool _abort = false;
        private ulong _cycleCount;

        public DigitalLevelCycler()
        { }

        public DigitalLevelCycler(DigitalLevel startLevel)
        {
            _startLevel = startLevel;
        }

        public DigitalLevelCycler(DigitalLevel startLevel, DigitalLevel endLevel)
        {
            if (endLevel == DigitalLevel.Floating)
                throw new ArgumentException(
                    "Invalid End Level: Floating is not supported.", nameof(endLevel));

            _startLevel = startLevel;
            _endLevel = endLevel;
        }

        public void Abort()
        {
            _abort = true;
        }

        public ulong CycleCount
        {
            get { return _cycleCount; }
        }

        public IEnumerator<DigitalLevel> GetEnumerator()
        {
            _cycleCount = 0;

            if (_startLevel == null && _endLevel == null)
            {
                while (!_abort)
                {
                    yield return DigitalLevel.Low;
                    yield return DigitalLevel.PosEdge;
                    yield return DigitalLevel.High;
                    yield return DigitalLevel.NegEdge;
                    _cycleCount++;
                }
            }
            else
            {
                var level = _startLevel.Value;

                while (!_abort)
                {
                    yield return level;

                    if (_endLevel.HasValue && level == _endLevel.Value)
                        yield break;

                    if (level < DigitalLevel.NegEdge)
                    {
                        level++;
                    }
                    else
                    {
                        level = DigitalLevel.Low;
                        _cycleCount++;
                    }
                }
            }
        }

        public static DigitalLevel NextLevel(DigitalLevel level)
        {
            if (level < DigitalLevel.NegEdge)
            {
                level++;
            }
            else
            {
                level = DigitalLevel.Low;
            }

            return level;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
