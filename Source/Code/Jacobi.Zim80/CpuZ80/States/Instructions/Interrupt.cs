using Jacobi.Zim80.CpuZ80.Opcodes;

namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class Interrupt : PushInstruction
    {
        private readonly OpcodeDefinition _opcodeDefinition;

        public Interrupt(Die die, OpcodeDefinition opcodeDefinition) 
            : base(die)
        {
            _opcodeDefinition = opcodeDefinition;
        }

        protected OpcodeDefinition OpcodeDefinition
        {
            get { return _opcodeDefinition; }
        }

        protected override void OnClockPos()
        {
            if (ExecutionEngine.Cycles.IsMachineCycle1 &&
                ExecutionEngine.Cycles.IsFirstCycle)
                StartInterrupt();

            base.OnClockPos();
        }

        protected virtual void StartInterrupt()
        {
            ExecutionEngine.InterruptManager.PushInt();
        }

        protected override byte GetRegisterLowValue()
        {
            return Registers.GetPC().GetLo();
        }

        protected override byte GetRegisterHighValue()
        {
            return Registers.GetPC().GetHi();
        }

    }
}
