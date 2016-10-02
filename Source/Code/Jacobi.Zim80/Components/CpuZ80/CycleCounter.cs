using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using System;

namespace Jacobi.Zim80.Components.CpuZ80
{
    internal class CycleCounter
    {
        public CycleNames CycleName { get; private set; }

        public MachineCycleNames MachineCycle { get; private set; }

        public bool IsMachineCycle1
        {
            get { return MachineCycle == MachineCycleNames.M1; }
        }

        public bool IsFirstCycle
        {
            get { return CycleName == CycleNames.T1; }
        }

        public bool IsLastCycle
        {
            get
            {
                if (_cycles != null)
                    return GetCycle(MachineCycle) == CycleName;

                // opcode fetch
                return CycleName == CycleNames.T4;
            }
        }

        public bool IsLastMachineCycle
        {
            get
            {
                if (_cycles == null) return false;

                return MachineCycleToIndex(MachineCycle) == _cycles.Length - 1;
            }
        }

        public void OnClock(DigitalLevel level)
        {
            if (level == DigitalLevel.PosEdge)
                IncrementCycle();
        }

        private OpcodeDefinition _opcodeDefinition;

        public OpcodeDefinition OpcodeDefinition
        {
            get { return _opcodeDefinition; }
            set
            {
                _opcodeDefinition = value;

                if (_opcodeDefinition != null)
                    _cycles = _opcodeDefinition.Cycles;
                else
                    _cycles = null;
            }
        }

        public void SetAltCycles()
        {
            if (_opcodeDefinition == null ||
                _opcodeDefinition.AltCycles == null)
            {
                throw new InvalidOperationException("No Opcode set or Opcode does not have Alternate Cycles.");
            }

            _cycles = _opcodeDefinition.AltCycles;
        }

        public void Reset()
        {
            _opcodeDefinition = null;
            _cycles = null;
            CycleName = CycleNames.NotInitialized;
            MachineCycle = MachineCycleNames.M1;
        }

        #region private
        private int[] _cycles;

        private void IncrementCycle()
        {
            CycleName++;

            if (_cycles != null &&
                GetCycle(MachineCycle) < CycleName)
            {
                NextMachineCycle();

                if ((int)MachineCycle > _cycles.Length)
                    throw new InvalidOperationException("Too many machine cycles.");
            }
            else if (CycleName > CycleNames.T6)
                throw new InvalidOperationException("Too many T-cycles.");
        }

        private void NextMachineCycle()
        {
            MachineCycle++;
            CycleName = CycleNames.T1;
        }

        private CycleNames GetCycle(MachineCycleNames machineCycle)
        {
            if (_cycles != null)
            {
                var index = MachineCycleToIndex(machineCycle);

                if (index >= _cycles.Length)
                {
                    throw new InvalidOperationException(
                        string.Format("The instuction {0} did not signal complete within its declared cycles.", OpcodeDefinition));
                }

                return (CycleNames)_cycles[(int)index];
            }

            return CycleNames.NotInitialized;
        }

        private int MachineCycleToIndex(MachineCycleNames machineCycle)
        {
            int index = (int)machineCycle;

            if (OpcodeDefinition != null &&
                OpcodeDefinition.Ext1 != 0)
            {
                index++;

                if (OpcodeDefinition.Ext2 != 0)
                    index++;
            }

            return index;
        }
        #endregion
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
