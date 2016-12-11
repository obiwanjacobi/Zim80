using Jacobi.Zim80.CpuZ80.Opcodes;

namespace Jacobi.Zim80.CpuZ80.States
{
    internal class CpuFetch : CpuRefresh
    {
        public CpuFetch(CpuZ80 cpu)
            : base(cpu)
        { }

        protected override void OnClockPos()
        {
            switch (ExecutionEngine.Cycles.CycleName)
            {
                case CycleNames.T1:
                    ExecutionEngine.SetPcOnAddressBus();
                    Cpu.MachineCycle1.Write(DigitalLevel.Low);
                    break;
                case CycleNames.T3:
                    Read();
                    Cpu.Read.Write(DigitalLevel.High);
                    Cpu.MemoryRequest.Write(DigitalLevel.High);
                    Cpu.MachineCycle1.Write(DigitalLevel.High);
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
                    Cpu.MemoryRequest.Write(DigitalLevel.Low);
                    Cpu.Read.Write(DigitalLevel.Low);
                    break;
                case CycleNames.T4:
                    // incomplete opcode byte was read
                    IsComplete = true;
                    break;
            }
        }

        private void Read()
        {
            var ob = new OpcodeByte(Cpu.Data.Slave.Value.ToByte());

            if (ExecutionEngine.AddOpcodeByte(ob))
                IsComplete = true;
        }
    }
}
