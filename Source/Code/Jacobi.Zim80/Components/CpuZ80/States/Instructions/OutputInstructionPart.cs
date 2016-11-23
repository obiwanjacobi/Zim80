using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using System;

namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class OutputInstructionPart : AutoCompleteInstructionPart
    {
        private readonly UInt16 _address;

        public OutputInstructionPart(Die die,
            MachineCycleNames activeMachineCycle, UInt16 address)
            : base(die, activeMachineCycle, CycleNames.T4)
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
                case CycleNames.T2:
                    Die.IoRequest.Write(DigitalLevel.Low);
                    Die.Write.Write(DigitalLevel.Low);
                    break;
            }
        }

        protected override void OnClockNeg()
        {
            base.OnClockNeg();

            switch (ExecutionEngine.Cycles.CycleName)
            {
                case CycleNames.T1:
                    Write();
                    break;
                case CycleNames.T4:
                    Die.Write.Write(DigitalLevel.High);
                    Die.IoRequest.Write(DigitalLevel.High);
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
