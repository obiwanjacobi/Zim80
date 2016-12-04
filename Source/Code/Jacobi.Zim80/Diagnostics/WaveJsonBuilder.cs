using Jacobi.Zim80.Logic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jacobi.Zim80.Diagnostics
{
    internal class WaveJsonBuilder
    {
        private readonly StringBuilder _json = new StringBuilder();

        public WaveJsonBuilder()
        {
            _json.Append("{ ");
        }

        public void WaveFrom(WaveSignal waveSignal)
        {
            if (waveSignal.BusStream != null)
                WaveFrom(waveSignal.BusStream, waveSignal.Offset);

            if (waveSignal.SignalStream != null)
                WaveFrom(waveSignal.SignalStream, waveSignal.Offset);

            _json.Append("\n");
        }

        private void WaveFrom(BusDataStream stream, int offset)
        {
            _json.Append("{ name: \"");
            _json.Append(stream.Bus.Name);
            _json.Append("\", wave: \"");
            WaveFrom(offset);
            var data = WaveFrom(stream.Samples);
            _json.Append("\", data: [");
            int index = 0;
            foreach (var str in data)
            {
                if (index > 0)
                    _json.Append(", ");

                _json.Append('\"');
                _json.Append(str);
                _json.Append('\"');

                index++;
            }
            _json.Append("] },");
        }

        private void WaveFrom(DigitalStream stream, int offset)
        {  
            _json.Append("{ name: \"");
            _json.Append(stream.DigitalSignal.Name);
            _json.Append("\", wave: \"");
            WaveFrom(offset);
            WaveFrom(stream.Samples);
            _json.Append("\" },");
        }

        private IList<string> WaveFrom(IEnumerable<BusData> samples)
        {
            var data = new List<string>();

            BusData lastSample = null;
            foreach (var sample in samples)
            {
                if (lastSample == null ||
                    !lastSample.Equals(sample))
                {
                    lastSample = sample;
                    var value = Convert(sample);
                    if (!String.IsNullOrEmpty(value))
                    {
                        _json.Append('=');
                        data.Add(value);
                    }
                    else
                    {
                        _json.Append('x');
                    }
                }
                else
                {
                    _json.Append('.');
                }
            }

            return data;
        }

        private void WaveFrom(IEnumerable<DigitalLevel> samples)
        {
            string lastSample = null;
            foreach (var sample in samples)
            {
                var txt = Convert(sample);

                if (lastSample == null ||
                    lastSample != txt)
                {
                    lastSample = txt;
                    _json.Append(txt);
                }
                else
                {
                    _json.Append('.');
                }
            }
        }

        private void WaveFrom(int offset)
        {
            if (offset == 0) return;

            _json.Append("x");

            if (offset > 1)
            {
                _json.Append(new string('.', offset - 1));
            }
        }

        public void BeginSignal()
        {
            _json.Append("\"signal\" : [");
        }

        public void EndSignal()
        {
            _json.Append(" ], \"config\":{ \"hscale\": 1, \"skin\":'narrow'}");
        }

        public void AddHead(string title)
        {
            _json.Append("\"head\": { text: \"");
            _json.Append(title);
            _json.Append("\" }");
        }

        public override string ToString()
        {
            return _json.ToString() + " }";
        }

        private static string Convert(BusData sample)
        {
            return sample.ToString();
        }

        private static string Convert(DigitalLevel level)
        {
            switch (level)
            {
                case DigitalLevel.Floating:
                    return "x";
                case DigitalLevel.Low:
                case DigitalLevel.NegEdge:
                    return "0";
                case DigitalLevel.PosEdge:
                case DigitalLevel.High:
                    return "1";
            }

            throw new InvalidOperationException();
        }
    }
}
