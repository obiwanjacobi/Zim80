namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class Inc16Instruction : SingleCycleInstruction
    {
        public Inc16Instruction(CpuZ80 cpu)
            : base(cpu)
        { }

        protected override void OnLastCycleFirstM()
        {
            var register = ExecutionEngine.Opcode.Definition.Register16FromP;

            if (ExecutionEngine.Opcode.Definition.IsIX)
                Registers.IX = Cpu.Alu.Inc16(Registers.IX);
            else if (ExecutionEngine.Opcode.Definition.IsIY)
                Registers.IY = Cpu.Alu.Inc16(Registers.IY);
            else
                Registers[register] = Cpu.Alu.Inc16(Registers[register]);
        }
    }
}
