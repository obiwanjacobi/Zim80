using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using System;

namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class ReadT3InstructionPart : AutoCompleteInstructionPart
    {
        private readonly UInt16? _address;

        public ReadT3InstructionPart(ExecutionEngine executionEngine, 
            MachineCycleNames activeMachineCycle) 
            : base(executionEngine, activeMachineCycle)
        { }

        public ReadT3InstructionPart(ExecutionEngine executionEngine, 
            MachineCycleNames activeMachineCycle, UInt16 address)
            : base(executionEngine, activeMachineCycle)
        {
            _address = address;
        }

        public OpcodeByte Data { get; private set; }

        protected override void OnClockPos()
        {
            base.OnClockPos();

            switch (ExecutionEngine.Cycles.CycleName)
            {
                case CycleNames.T1:
                    if (_address.HasValue)
                        ExecutionEngine.SetAddressBus(_address.Value);
                    else
                        ExecutionEngine.SetPcOnAddressBus();
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
                    ExecutionEngine.Die.Read.Write(DigitalLevel.Low);
                    break;
                case CycleNames.T3:
                    Read();
                    ExecutionEngine.Die.Read.Write(DigitalLevel.High);
                    ExecutionEngine.Die.MemoryRequest.Write(DigitalLevel.High);
                    break;
            }
        }

        private void Read()
        {
            Data = new OpcodeByte(ExecutionEngine.Die.DataBus.ToSlave().Value.ToByte());

            if (!_address.HasValue)
                ExecutionEngine.AddOpcodeByte(Data);
        }
    }
}
