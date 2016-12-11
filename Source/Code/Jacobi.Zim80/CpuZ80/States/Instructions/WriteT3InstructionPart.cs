using Jacobi.Zim80.CpuZ80.Opcodes;
using System;

namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class WriteT3InstructionPart : AutoCompleteInstructionPart
    {
        private readonly UInt16 _address;

        public WriteT3InstructionPart(CpuZ80 cpu,
            MachineCycleNames activeMachineCycle, UInt16 address)
            : base(cpu, activeMachineCycle, CycleNames.T3)
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
                    Cpu.MemoryRequest.Write(DigitalLevel.Low);
                    break;
                case CycleNames.T2:
                    Cpu.Write.Write(DigitalLevel.Low);
                    Write();
                    break;
                case CycleNames.T3:
                    Cpu.Write.Write(DigitalLevel.High);
                    Cpu.MemoryRequest.Write(DigitalLevel.High);
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
