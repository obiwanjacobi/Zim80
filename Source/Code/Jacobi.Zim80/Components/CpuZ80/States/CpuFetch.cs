using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using System;

namespace Jacobi.Zim80.Components.CpuZ80.States
{
    internal class CpuFetch : CpuRefresh
    {
        public CpuFetch(ExecutionEngine executionEngine)
            : base(executionEngine)
        { }

        protected override void OnClockPos()
        {
            switch (ExecutionEngine.Cycles.CycleName)
            {
                case CycleNames.T1:
                    ExecutionEngine.SetPcOnAddressBus();
                    ExecutionEngine.Die.MachineCycle1.Write(DigitalLevel.Low);
                    break;
                case CycleNames.T3:
                    Read();
                    ExecutionEngine.Die.Read.Write(DigitalLevel.High);
                    ExecutionEngine.Die.MemoryRequest.Write(DigitalLevel.High);
                    ExecutionEngine.Die.MachineCycle1.Write(DigitalLevel.High);
                    break;
            }

            base.OnClockPos();
        }

        protected override void OnClockNeg()
        {
            base.OnClockNeg();

            switch (ExecutionEngine.Cycles.CycleName)
            {
                case CycleNames.T1:
                    ExecutionEngine.Die.MemoryRequest.Write(DigitalLevel.Low);
                    ExecutionEngine.Die.Read.Write(DigitalLevel.Low);
                    break;
            }
        }

        private void Read()
        {
            var ob = new OpcodeByte(ExecutionEngine.Die.DataBus.ToSlave().Value.ToByte());

            if (ExecutionEngine.AddOpcodeByte(ob))
                IsComplete = true;
            else
                throw new InvalidOperationException("No valid opcode during Fetch.");
        }
    }
}
