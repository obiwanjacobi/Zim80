namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class Inc16Instruction : SingleCycleInstruction
    {
        public Inc16Instruction(Die die)
            : base(die)
        { }

        protected override void OnLastCycleFirstM()
        {
            var register = ExecutionEngine.Opcode.Definition.Register16FromP;

            if (ExecutionEngine.Opcode.Definition.IsIX)
                Registers.IX = Die.Alu.Inc16(Registers.IX);
            else if (ExecutionEngine.Opcode.Definition.IsIY)
                Registers.IY = Die.Alu.Inc16(Registers.IY);
            else
                Registers[register] = Die.Alu.Inc16(Registers[register]);
        }
    }
}
