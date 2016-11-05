using System.Collections;
using System.Collections.Generic;

namespace Jacobi.Zim80.Components.Generators
{
    public class DigitalLevelCycle : IEnumerable<DigitalLevel>
    {
        private readonly DigitalLevel? _startLevel = null;
        private readonly DigitalLevel? _endLevel = null;
        private bool _abort = false;
        private ulong _cycleCount;

        public DigitalLevelCycle()
        { }

        public DigitalLevelCycle(DigitalLevel startLevel)
        {
            _startLevel = startLevel;
        }

        public DigitalLevelCycle(DigitalLevel startLevel, DigitalLevel endLevel)
        {
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

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
