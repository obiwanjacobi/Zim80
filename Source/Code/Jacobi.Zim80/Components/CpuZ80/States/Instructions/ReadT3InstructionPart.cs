using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using System;

namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class ReadT3InstructionPart : AutoCompleteInstructionPart
    {
        private readonly UInt16? _address;

        public ReadT3InstructionPart(Die die, 
            MachineCycleNames activeMachineCycle) 
            : base(die, activeMachineCycle, CycleNames.T3)
        { }

        public ReadT3InstructionPart(Die die, 
            MachineCycleNames activeMachineCycle, UInt16 address)
            : base(die, activeMachineCycle, CycleNames.T3)
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
                    Die.MemoryRequest.Write(DigitalLevel.Low);
                    Die.Read.Write(DigitalLevel.Low);
                    break;
                case CycleNames.T3:
                    Read();
                    Die.Read.Write(DigitalLevel.High);
                    Die.MemoryRequest.Write(DigitalLevel.High);
                    break;
            }
        }

        private void Read()
        {
            Data = new OpcodeByte(Die.DataBus.Slave.Value.ToByte());

            if (!_address.HasValue)
                ExecutionEngine.AddOpcodeByte(Data);
        }
    }
}
