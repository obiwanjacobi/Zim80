using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using System;

namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class WriteT3InstructionPart : AutoCompleteInstructionPart
    {
        private readonly UInt16 _address;

        public WriteT3InstructionPart(ExecutionEngine executionEngine,
            MachineCycleNames activeMachineCycle, UInt16 address)
            : base(executionEngine, activeMachineCycle)
        {
            _address = address;
        }

        public OpcodeByte Data { get; set; }

        protected override void OnClockPos()
        {
            base.OnClockPos();

            switch (ExecutionEngine.Cycles.CycleName)
            {
                case CycleNames.T1:
                    ExecutionEngine.SetAddressBus(_address);
                    break;
            }
        }

        protected override void OnClockNeg()
        {
            base.OnClockNeg();

            switch (ExecutionEngine.Cycles.CycleName)
            {
                case CycleNames.T1:
                    ExecutionEngine.Die.MemoryRequest.Write(DigitalLevel.Low);
                    break;
                case CycleNames.T2:
                    ExecutionEngine.Die.Write.Write(DigitalLevel.Low);
                    Write();
                    break;
                case CycleNames.T3:
                    ExecutionEngine.Die.Write.Write(DigitalLevel.High);
                    ExecutionEngine.Die.MemoryRequest.Write(DigitalLevel.High);
                    ExecutionEngine.Die.DataBus.IsEnabled = false;
                    break;
            }
        }

        private void Write()
        {
            ExecutionEngine.Die.DataBus.IsEnabled = true;
            ExecutionEngine.Die.DataBus.Write(new BusData8(Data.Value));
        }
    }
}
