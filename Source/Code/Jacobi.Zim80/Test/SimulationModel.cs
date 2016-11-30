using Jacobi.Zim80.Logic;
using Jacobi.Zim80.Memory;

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
    }
}
