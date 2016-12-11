namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class LoadRegister16Instruction : SingleCycleInstruction
    {
        public LoadRegister16Instruction(CpuZ80 cpu) 
            : base(cpu)
        { }

        protected override void OnLastCycleFirstM()
        {
            if (ExecutionEngine.Opcode.Definition.IsIX)
            {
                Registers.SP = Registers.IX;
            }
            else if (ExecutionEngine.Opcode.Definition.IsIY)
            {
                Registers.SP = Registers.IY;
            }
            else
            {
                Registers.SP = Registers.HL;
            }
        }
    }
}
