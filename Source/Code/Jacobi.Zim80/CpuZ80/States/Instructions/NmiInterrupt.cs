using Jacobi.Zim80.CpuZ80.Opcodes;

namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class NmiInterrupt : Interrupt
    {
        public NmiInterrupt(CpuZ80 cpu, OpcodeDefinition opcodeDefinition) 
            : base(cpu, opcodeDefinition)
        { }

        protected override void StartInterrupt()
        {
            ExecutionEngine.InterruptManager.PushNmi();
        }
    }
}
