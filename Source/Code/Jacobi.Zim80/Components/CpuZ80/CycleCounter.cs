using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using System;

namespace Jacobi.Zim80.Components.CpuZ80
{
    internal class CycleCounter
    {
        public CycleNames CycleName { get; private set; }
        public MachineCycleNames MachineCycle { get; private set; }
        public bool IsMachineCycle1 { get { return MachineCycle == MachineCycleNames.M1; } }
        public bool IsLastCycle
        {
            get
            {
                if (OpcodeDefinition != null)
                    return OpcodeDefinition.Cycles[(int)MachineCycle] == (int)CycleName;

                return false;
            }
        }

        public void OnClock(DigitalLevel level)
        {
            if (level == DigitalLevel.PosEdge)
                IncrementCycle();
        }

        public OpcodeDefinition OpcodeDefinition { get; set; }
        

        private void IncrementCycle()
        {
            CycleName++;

            if (OpcodeDefinition != null &&
                OpcodeDefinition.Cycles[(int)MachineCycle] < (int)CycleName)
            {
                NextMachineCycle();

                if ((int)MachineCycle > OpcodeDefinition.Cycles.Length)
                    throw new InvalidOperationException("Too many machine cycles.");
            }
            else if(CycleName > CycleNames.T6)
                throw new InvalidOperationException("Too many T-cycles.");
        }

        private void NextMachineCycle()
        {
            MachineCycle++;
            CycleName = CycleNames.T1;
        }
    }

    public enum CycleNames
    {
        NotInitialized,
        T1,
        T2,
        T3,
        T4,
        T5,
        T6
    }

    public enum MachineCycleNames
    {
        M1,
        M2,
        M3,
        M4,
        M5,
        M6,
    }
}
