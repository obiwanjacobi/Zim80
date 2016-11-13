using Jacobi.Zim80.Components;
using Jacobi.Zim80.Components.CpuZ80;
using Jacobi.Zim80.Components.Generators;
using Jacobi.Zim80.Components.Memory;

namespace Jacobi.Zim80.Model
{
    public class SimulationModel
    {
        public CpuZ80 Cpu { get; set; }
        public MemoryRam<BusData16, BusData8> Memory { get; set; }
        public MemoryRam<BusData16, BusData8> IoSpace { get; set; }
        public Bus<BusData16> Address { get; set; }
        public Bus<BusData8> Data { get; set; }
        public SignalGenerator ClockGen { get; set; }
    }
}
