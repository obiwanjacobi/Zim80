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
                if (_opcodeCycles != null)
                    return GetCycle(MachineCycle) == CycleName;

                if (_continueCount > 0)
                    return _continueCycles == 0;

                // opcode fetch
                return CycleName == CycleNames.T4;
            }
        }

        public bool IsLastMachineCycle
        {
            get
            {
                if (_opcodeCycles == null) return false;

                return MachineCycleToIndex(MachineCycle) == _opcodeCycles.Length - 1;
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
                    _opcodeCycles = _opcodeDefinition.Cycles;
                else
                    _opcodeCycles = null;
            }
        }

        public void SetAltCycles()
        {
            if (_opcodeDefinition == null ||
                _opcodeDefinition.AltCycles == null)
            {
                throw new InvalidOperationException("No Opcode set or Opcode does not have Alternate Cycles.");
            }

            _opcodeCycles = _opcodeDefinition.AltCycles;
        }

        // use after instruction has completed
        public void Reset()
        {
            _continueCycles = 0;
            _continueCount = 0;
            _opcodeDefinition = null;
            _opcodeCycles = null;
            CycleName = CycleNames.NotInitialized;
            MachineCycle = MachineCycleNames.M1;
        }

        // use to keep reading without opcode
        public void Continue(int cycles = 4)
        {
            _continueCycles = cycles;
            _continueCount++;

            CycleName = CycleNames.NotInitialized;
            MachineCycle = MachineCycleNames.M1;
        }

        #region private
        private int _continueCount;
        private int _continueCycles;
        private int[] _opcodeCycles;

        private void IncrementCycle()
        {
            CycleName++;

            if (_opcodeCycles != null &&
                GetCycle(MachineCycle) < CycleName)
            {
                NextMachineCycle();

                if ((int)MachineCycle > _opcodeCycles.Length)
                    throw new InvalidOperationException("Too many machine cycles.");
            }
            else if(_opcodeCycles == null &&
                    _continueCount > 0)
            {
                if (_continueCycles == 0)
                    throw new InvalidOperationException("Too many T-cycles (Continue).");

                _continueCycles--;
            }
            else if (CycleName > CycleNames.T6)
                throw new InvalidOperationException("Too many T-cycles.");
        }

        private void NextMachineCycle()
        {
            MachineCycle++;
            CycleName = CycleNames.T1;

            if (_opcodeCycles != null)
            {
                // special case for cycle-count of -1
                // that means the instruction 'hangs' until it signals complete
                // See also HaltInstruction.
                var index = MachineCycleToIndex(MachineCycle);
                if (_opcodeCycles[index] < 0)
                    MachineCycle = MachineCycleNames.M1;
            }
        }

        private CycleNames GetCycle(MachineCycleNames machineCycle)
        {
            if (_opcodeCycles != null)
            {
                var index = MachineCycleToIndex(machineCycle);

                if (index >= _opcodeCycles.Length)
                {
                    throw new InvalidOperationException(
                        string.Format("The instuction {0} did not signal complete within its declared machine cycles.", OpcodeDefinition));
                }

                return (CycleNames)_opcodeCycles[(int)index];
            }

            return CycleNames.NotInitialized;
        }

        private int MachineCycleToIndex(MachineCycleNames machineCycle)
        {
            int index = (int)machineCycle;
            index += _continueCount;

            //if (OpcodeDefinition != null &&
            //    OpcodeDefinition.Ext1 != 0)
            //{
            //    index++;

            //    if (OpcodeDefinition.Ext2 != 0)
            //        index++;
            //}

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
