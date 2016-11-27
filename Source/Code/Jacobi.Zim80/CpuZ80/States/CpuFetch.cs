using Jacobi.Zim80.CpuZ80.Opcodes;

namespace Jacobi.Zim80.CpuZ80.States
{
    internal class CpuFetch : CpuRefresh
    {
        public CpuFetch(Die die)
            : base(die)
        { }

        protected override void OnClockPos()
        {
            switch (ExecutionEngine.Cycles.CycleName)
            {
                case CycleNames.T1:
                    ExecutionEngine.SetPcOnAddressBus();
                    Die.MachineCycle1.Write(DigitalLevel.Low);
                    break;
                case CycleNames.T3:
                    Read();
                    Die.Read.Write(DigitalLevel.High);
                    Die.MemoryRequest.Write(DigitalLevel.High);
                    Die.MachineCycle1.Write(DigitalLevel.High);
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
                    Die.MemoryRequest.Write(DigitalLevel.Low);
                    Die.Read.Write(DigitalLevel.Low);
                    break;
                case CycleNames.T4:
                    // incomplete opcode byte was read
                    IsComplete = true;
                    break;
            }
        }

        private void Read()
        {
            var ob = new OpcodeByte(Die.DataBus.Slave.Value.ToByte());

            if (ExecutionEngine.AddOpcodeByte(ob))
                IsComplete = true;
        }
    }
}
