using Jacobi.Zim80.CpuZ80.Opcodes;

namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class AddRegister16Instruction : MultiCycleInstruction
    {
        public AddRegister16Instruction(CpuZ80 cpu) 
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

            if (ExecutionEngine.Opcode.Definition.IsIX)
                AddIX16(reg16);
            else if (ExecutionEngine.Opcode.Definition.IsIY)
                AddIY16(reg16);
            else if (ExecutionEngine.Opcode.Definition.Ext1 == 0xED)
                AddCarry16(reg16);
            else
                Add16(reg16);
        }

        private void Add16(Register16Table reg16)
        {
            Registers.HL = Cpu.Alu.Add16(Registers.HL, Registers[reg16]);
        }

        private void AddIX16(Register16Table reg16)
        {
            if (reg16 == Register16Table.HL)
                Registers.IX = Cpu.Alu.Add16(Registers.IX, Registers.IX);
            else
                Registers.IX = Cpu.Alu.Add16(Registers.IX, Registers[reg16]);
        }

        private void AddIY16(Register16Table reg16)
        {
            if (reg16 == Register16Table.HL)
                Registers.IY = Cpu.Alu.Add16(Registers.IY, Registers.IY);
            else
                Registers.IY = Cpu.Alu.Add16(Registers.IY, Registers[reg16]);
        }

        private void AddCarry16(Register16Table reg16)
        {
            Registers.HL = Cpu.Alu.Add16(Registers.HL, Registers[reg16], addCarry: true);
        }
    }
}
