using System;
using System.Collections.Generic;

namespace Jacobi.Zim80.Logic
{
    /// <summary>
    /// Activates Output when one or more values are detected on the bus.
    /// </summary>
    public class BusDecoder : INamedObject
    {
        public BusDecoder()
        {
            Output = new DigitalSignalProvider("BusDecoder Output");
            _busSlave = new BusSlave("BusDecoder Input");
            _busSlave.OnChanged += Bus_OnChanged;
        }

        public BusDecoder(string name)
            : this()
        {
            Name = name;
        }

        public BusDecoder(Bus bus)
            : this()
        {
            Input.ConnectTo(bus);
        }

        public BusDecoder(Bus bus, string name)
            : this()
        {
            Name = name;
            Input.ConnectTo(bus);
        }

        private BusSlave _busSlave;

        public BusSlave Input
        {
            get { return _busSlave; }
        }

        private void Bus_OnChanged(object sender, BusChangedEventArgs<BusData> e)
        {
            Output.Write(Decode(e.Value) ? DigitalLevel.High : DigitalLevel.Low);
        }

        // pos output
        public DigitalSignalProvider Output { get; private set; }

        private List<UInt32> _values = new List<uint>();

        public IEnumerable<UInt32> Values { get { return _values; } }

        public string Name { get; set; }

        public void AddValue(UInt32 value)
        {
            _values.Add(value);
        }

        public void Clear()
        {
            _values.Clear();
        }

        private bool Decode(BusData busData)
        {
            var value = busData.ToUInt32();
            return _values.Contains(value);
        }
    }
}
