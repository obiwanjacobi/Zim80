using Jacobi.Zim80.Components.CpuZ80.Opcodes;

namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    // similar to ReadT3InstructionPart
    internal class InputInstructionPart : AutoCompleteInstructionPart
    {
        private ushort _address;

        public InputInstructionPart(Die die, MachineCycleNames activeMachineCycle, ushort address)
            : base(die, activeMachineCycle, CycleNames.T4)
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
                    Die.IoRequest.Write(DigitalLevel.Low);
                    Die.Read.Write(DigitalLevel.Low);
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
                    Die.Read.Write(DigitalLevel.High);
                    Die.IoRequest.Write(DigitalLevel.High);
                    break;
            }
        }

        private void Read()
        {
            Data = new OpcodeByte(Die.DataBus.Slave.Value.ToByte());
        }
    }
}
