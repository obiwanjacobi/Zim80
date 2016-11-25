﻿using System;
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
            _json.Append("{ name: \"");
            _json.Append(waveSignal.Stream.DigitalSignal.Name);
            _json.Append("\", wave: \"");
            WaveFrom(waveSignal.Offset);
            WaveFrom(waveSignal.Stream.Samples);
            _json.Append("\" },");
        }

        private void WaveFrom(IEnumerable<DigitalLevel> samples)
        {
            DigitalLevel? lastSample = null;
            foreach (var sample in samples)
            {
                if (lastSample == null ||
                    lastSample.Value != sample)
                {
                    lastSample = sample;
                    _json.Append(Convert(sample));
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
            _json.Append("\"signal\" [");
        }

        public void EndSignal()
        {
            _json.Append(" ], ");
        }

        public void AddHead(string title)
        {
            _json.Append("\"head\": { text: \"");
            _json.Append(title);
            _json.Append("\" }");
        }

        public override string ToString()
        {
            _json.Append(" }");
            return _json.ToString();
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
