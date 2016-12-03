using Jacobi.Zim80.CpuZ80;
using System;

namespace Jacobi.Zim80.Logic
{
    public class SignalGenerator
    {
        public SignalGenerator()
        {
            Output = new DigitalSignalProvider();
        }

        public DigitalSignalProvider Output { get; private set; }

        public void SquareWave(long numberOfCycles)
        {
            var level = Output.Level;

            for (int i = 0; i < numberOfCycles; i++)
            {
                OutputOneCycle();
            }

            Output.Write(level);
        }

        private void OutputOneCycle()
        {
            Output.Write(DigitalLevel.Low);
            Output.Write(DigitalLevel.PosEdge);
            Output.Write(DigitalLevel.High);
            Output.Write(DigitalLevel.NegEdge);
        }

        public void SquareWave(long numberOfMachineCycles, CycleNames toCycle,
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

        public void SquareWave(Func<long, bool> abortFunc)
        {
            if (abortFunc == null)
                throw new ArgumentNullException(nameof(abortFunc));

            long cycles = 0;
            do
            {
                OutputOneCycle();
                cycles++;
            }
            while (!abortFunc(cycles));
        }
    }
}
