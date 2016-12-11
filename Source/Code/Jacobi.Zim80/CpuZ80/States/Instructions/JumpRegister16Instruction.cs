namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class JumpRegister16Instruction : SingleCycleInstruction
    {
        public JumpRegister16Instruction(CpuZ80 cpu) 
            : base(cpu)
        { }

        protected override void OnLastCycleFirstM()
        {
            if (ExecutionEngine.Opcode.Definition.IsIX)
                Registers.PC = Registers.IX;
            else if (ExecutionEngine.Opcode.Definition.IsIY)
                Registers.PC = Registers.IY;
            else
                Registers.PC = Registers.HL;
        }
    }
}
