using Jacobi.Zim80.CpuZ80.Opcodes;

namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    // similar to ReadT3InstructionPart
    internal class InputInstructionPart : AutoCompleteInstructionPart
    {
        private ushort _address;

        public InputInstructionPart(CpuZ80 cpu, MachineCycleNames activeMachineCycle, ushort address)
            : base(cpu, activeMachineCycle, CycleNames.T4)
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
                    ExecutionEngine.SetAddressBus(_address);
                    break;
                case CycleNames.T2:
                    Cpu.IoRequest.Write(DigitalLevel.Low);
                    Cpu.Read.Write(DigitalLevel.Low);
                    break;
            }
        }

        protected override void OnClockNeg()
        {
            base.OnClockNeg();

            switch (ExecutionEngine.Cycles.CycleName)
            {
                case CycleNames.T4:
                    Read();
                    Cpu.Read.Write(DigitalLevel.High);
                    Cpu.IoRequest.Write(DigitalLevel.High);
                    break;
            }
        }

        private void Read()
        {
            Data = new OpcodeByte(Cpu.Data.Slave.Value.ToByte());
        }
    }
}
