namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class SubRegister16Instruction : MultiCycleInstruction
    {
        public SubRegister16Instruction(Die die) 
            : base(die)
        { }

        protected override CpuState GetInstructionPart(MachineCycleNames machineCycle)
        {
            // these cycles perform the add in hardware
            return new AutoCompleteInstructionPart(Die, machineCycle);
        }

        protected override void OnLastCycleLastM()
        {
            var reg16 = ExecutionEngine.Opcode.Definition.Register16FromP;

            Registers.HL = Die.Alu.Sub16(Registers.HL, Registers[reg16], subCarry: true);
        }
    }
}
