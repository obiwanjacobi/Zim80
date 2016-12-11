namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class SubRegister16Instruction : MultiCycleInstruction
    {
        public SubRegister16Instruction(CpuZ80 cpu) 
            : base(cpu)
        { }

        protected override CpuState GetInstructionPart(MachineCycleNames machineCycle)
        {
            // these cycles perform the add in hardware
            return new AutoCompleteInstructionPart(Cpu, machineCycle);
        }

        protected override void OnLastCycleLastM()
        {
            var reg16 = ExecutionEngine.Opcode.Definition.Register16FromP;

            Registers.HL = Cpu.Alu.Sub16(Registers.HL, Registers[reg16], subCarry: true);
        }
    }
}
