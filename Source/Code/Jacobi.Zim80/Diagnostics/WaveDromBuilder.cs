﻿using Jacobi.Zim80.Logic;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Jacobi.Zim80.Diagnostics
{
    // See also http://wavedrom.com/
    // Try it: http://wavedrom.com/editor.html
    public class WaveDromBuilder
    {
        private readonly List<WaveSignal> _waves = new List<WaveSignal>();

        public int MaxCount
        {
            get { return _waves.Select((w) => w.Offset + w.SignalStream.Samples.Count()).Max(); }
        }

        public int MinCount
        {
            get { return _waves.Select((w) => w.Offset + w.SignalStream.Samples.Count()).Min(); }
        }

        public void Add(DigitalInputStream digitalStream, int offset = 0)
        {
            var wave = new WaveSignal
            {
                Offset = offset,
                SignalStream = digitalStream
            };

            _waves.Add(wave);
        }

        public void AddRange(IEnumerable<DigitalInputStream> digitalStreams)
        {
            foreach (var stream in digitalStreams)
                Add(stream);
        }

        public void Add(BusDataInputStream busStream, int offset = 0)
        {
            var wave = new WaveSignal
            {
                Offset = offset,
                BusStream = busStream
            };

            _waves.Add(wave);
        }

        public void AddRange(IEnumerable<BusDataInputStream> busStreams)
        {
            foreach (var stream in busStreams)
                Add(stream);
        }

        public void Save(Stream stream, string title)
        {
            WaveJsonBuilder builder = new WaveJsonBuilder();

            builder.BeginSignal();

            foreach (var wave in _waves)
            {
                builder.WaveFrom(wave);
            }

            builder.EndSignal();
            builder.AddHead(title);

            var writer = new StreamWriter(stream);
            writer.Write(builder.ToString());
        }

        public override string ToString()
        {
            WaveJsonBuilder builder = new WaveJsonBuilder();

            builder.BeginSignal();

            foreach (var wave in _waves)
            {
                builder.WaveFrom(wave);
            }

            builder.EndSignal();

            return builder.ToString();
        }
    }

    internal class WaveSignal
    {
        public int Offset;
        public DigitalInputStream SignalStream;
        public BusDataInputStream BusStream;
    }
}
