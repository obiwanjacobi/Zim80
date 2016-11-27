using Jacobi.Zim80.CpuZ80.Opcodes;

namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class NmiInterrupt : Interrupt
    {
        public NmiInterrupt(Die die, OpcodeDefinition opcodeDefinition) 
            : base(die, opcodeDefinition)
        { }

        protected override void StartInterrupt()
        {
            ExecutionEngine.InterruptManager.PushNmi();
        }
    }
}
