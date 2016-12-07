using Jacobi.Zim80.Logic;
using Jacobi.Zim80.Memory;
using System.Collections.Generic;

namespace Jacobi.Zim80.Test
{
    public class SimulationModel
    {
        public CpuZ80.CpuZ80 Cpu { get; set; }
        public MemoryRam<BusData16, BusData8> Memory { get; set; }
        public MemoryRam<BusData16, BusData8> IoSpace { get; set; }
        public Bus<BusData16> Address { get; set; }
        public Bus<BusData8> Data { get; set; }
        public SignalGenerator ClockGen { get; set; }
        public LogicAnalyzer LogicAnalyzer { get; set; }

        private Dictionary<string, INamedObject> _components;

        // gates etc.
        public IDictionary<string, INamedObject> Components
        {
            get
            {
                if (_components == null)
                    _components = new Dictionary<string, INamedObject>();

                return _components;
            }
        }

        private Dictionary<string, OutputPort> _outputPorts;

        // IO
        public IDictionary<string, OutputPort> OutputPorts
        {
            get
            {
                if (_outputPorts == null)
                    _outputPorts = new Dictionary<string, OutputPort>();

                return _outputPorts;
            }
        }
    }
}
