namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class LoadRegister8Instruction : SingleCycleInstruction
    {
        public LoadRegister8Instruction(Die die) 
            : base(die)
        { }

        protected override void OnLastCycleFirstM()
        {
            var srcReg = ExecutionEngine.Opcode.Definition.Register8FromZ;
            var trgReg = ExecutionEngine.Opcode.Definition.Register8FromY;

            Registers.PrimarySet[trgReg] = Registers.PrimarySet[srcReg];
        }
    }
}
