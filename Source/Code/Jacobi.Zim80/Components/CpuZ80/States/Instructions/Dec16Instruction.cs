namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class Dec16Instruction : SingleCycleInstruction
    {
        public Dec16Instruction(Die die)
            : base(die)
        { }

        protected override void OnLastCycleFirstM()
        {
            var register = ExecutionEngine.Opcode.Definition.Register16FromP;

            if (ExecutionEngine.Opcode.Definition.IsIX)
                Registers.IX = Die.Alu.Dec16(Registers.IX);
            else if (ExecutionEngine.Opcode.Definition.IsIY)
                Registers.IY = Die.Alu.Dec16(Registers.IY);
            else
                Registers[register] = Die.Alu.Dec16(Registers[register]);
        }
    }
}
