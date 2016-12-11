using Jacobi.Zim80.CpuZ80.Opcodes;
using System;

namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class OutputInstructionPart : AutoCompleteInstructionPart
    {
        private readonly UInt16 _address;

        public OutputInstructionPart(CpuZ80 cpu,
            MachineCycleNames activeMachineCycle, UInt16 address)
            : base(cpu, activeMachineCycle, CycleNames.T4)
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
                    Cpu.IoRequest.Write(DigitalLevel.Low);
                    Cpu.Write.Write(DigitalLevel.Low);
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
                    Cpu.Write.Write(DigitalLevel.High);
                    Cpu.IoRequest.Write(DigitalLevel.High);
                    Cpu.Data.IsEnabled = false;
                    break;
            }
        }

        private void Write()
        {
            Cpu.Data.IsEnabled = true;
            Cpu.Data.Write(new BusData8(Data.Value));
        }
    }
}
