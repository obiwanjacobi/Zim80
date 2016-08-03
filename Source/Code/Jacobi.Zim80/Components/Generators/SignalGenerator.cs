using Jacobi.Zim80.Components.CpuZ80;

namespace Jacobi.Zim80.Components.Generators
{
    public class SignalGenerator
    {
        public SignalGenerator()
        {
            Output = new DigitalSignalProvider();
        }

        public DigitalSignalProvider Output { get; private set; }

        public void BlockWave(long numberOfCycles)
        {
            var level = Output.Level;

            for (int i = 0; i < numberOfCycles; i++)
            {
                Output.Write(DigitalLevel.Low);
                Output.Write(DigitalLevel.PosEdge);
                Output.Write(DigitalLevel.High);
                Output.Write(DigitalLevel.NegEdge);
            }

            Output.Write(level);
        }

        public void BlockWave(long numberOfMachineCycles, CycleNames toCycle,
            DigitalLevel toLevel, CycleNames maxCycle = CycleNames.T4)
        {
            if (maxCycle < toCycle)
                maxCycle = toCycle;

            for (int machineCycle = 1; machineCycle <= numberOfMachineCycles; machineCycle++)
            {
                for (var cycle = CycleNames.T1; cycle <= toCycle; cycle++)
                {
                    for (var level = DigitalLevel.Low; level <= DigitalLevel.NegEdge; level++)
                    {
                        Output.Write(level);

                        if (machineCycle == numberOfMachineCycles &&
                            cycle == toCycle && level == toLevel)
                            return;
                    }
                }
            }
        }
    }
}
