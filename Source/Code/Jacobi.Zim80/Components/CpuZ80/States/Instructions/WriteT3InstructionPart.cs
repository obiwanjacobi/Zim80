using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using System;

namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class WriteT3InstructionPart : AutoCompleteInstructionPart
    {
        private readonly UInt16 _address;

        public WriteT3InstructionPart(Die die,
            MachineCycleNames activeMachineCycle, UInt16 address)
            : base(die, activeMachineCycle, CycleNames.T3)
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
                    Die.MemoryRequest.Write(DigitalLevel.Low);
                    break;
                case CycleNames.T2:
                    Die.Write.Write(DigitalLevel.Low);
                    Write();
                    break;
                case CycleNames.T3:
                    Die.Write.Write(DigitalLevel.High);
                    Die.MemoryRequest.Write(DigitalLevel.High);
                    Die.DataBus.IsEnabled = false;
                    break;
            }
        }

        private void Write()
        {
            Die.DataBus.IsEnabled = true;
            Die.DataBus.Write(new BusData8(Data.Value));
        }
    }
}
