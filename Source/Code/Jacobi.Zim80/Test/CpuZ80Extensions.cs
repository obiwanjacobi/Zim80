using System.Collections.Generic;

namespace Jacobi.Zim80.Test
{
    public static class CpuZ80Extensions
    {
        public static IEnumerable<DigitalSignalConsumer> Inputs(this CpuZ80.CpuZ80 cpu)
        {
            yield return cpu.Clock;
            yield return cpu.BusRequest;
            yield return cpu.Wait;
            yield return cpu.Reset;
            yield return cpu.Interrupt;
            yield return cpu.NonMaskableInterrupt;
        }

        public static IEnumerable<DigitalSignalProvider> Outputs(this CpuZ80.CpuZ80 cpu)
        {
            yield return cpu.MachineCycle1;
            yield return cpu.MemoryRequest;
            yield return cpu.IoRequest;
            yield return cpu.Read;
            yield return cpu.Write;
            yield return cpu.Refresh;
            yield return cpu.Halt;
            yield return cpu.BusAcknowledge;
        }
    }
}
