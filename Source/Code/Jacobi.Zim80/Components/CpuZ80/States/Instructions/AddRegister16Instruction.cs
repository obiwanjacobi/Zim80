using Jacobi.Zim80.Components.CpuZ80.Opcodes;

namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class AddRegister16Instruction : MultiCycleInstruction
    {
        public AddRegister16Instruction(Die die) 
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

            if (ExecutionEngine.Opcode.Definition.IsIX)
            {
                AddIX16(reg16);
            }
            else if (ExecutionEngine.Opcode.Definition.IsIY)
            {
                AddIY16(reg16);
            }
            else if (ExecutionEngine.Opcode.Definition.Ext1 == 0xED)
            {
                AddCarry16(reg16);
            }
            else
            {
                Add16(reg16);
            }
        }

        private void Add16(Register16Table reg16)
        {
            switch (reg16)
            {
                case Register16Table.BC:
                    Registers.PrimarySet.HL = Die.Alu.Add16(Registers.PrimarySet.HL, Registers.PrimarySet.BC);
                    break;
                case Register16Table.DE:
                    Registers.PrimarySet.HL = Die.Alu.Add16(Registers.PrimarySet.HL, Registers.PrimarySet.DE);
                    break;
                case Register16Table.HL:
                    Registers.PrimarySet.HL = Die.Alu.Add16(Registers.PrimarySet.HL, Registers.PrimarySet.HL);
                    break;
                case Register16Table.SP:
                    Registers.PrimarySet.HL = Die.Alu.Add16(Registers.PrimarySet.HL, Registers.SP);
                    break;
            }
        }

        private void AddIX16(Register16Table reg16)
        {
            switch (reg16)
            {
                case Register16Table.BC:
                    Registers.IX = Die.Alu.Add16(Registers.IX, Registers.PrimarySet.BC);
                    break;
                case Register16Table.DE:
                    Registers.IX = Die.Alu.Add16(Registers.IX, Registers.PrimarySet.DE);
                    break;
                case Register16Table.HL:
                    Registers.IX = Die.Alu.Add16(Registers.IX, Registers.IX);
                    break;
                case Register16Table.SP:
                    Registers.IX = Die.Alu.Add16(Registers.IX, Registers.SP);
                    break;
            }
        }

        private void AddIY16(Register16Table reg16)
        {
            switch (reg16)
            {
                case Register16Table.BC:
                    Registers.IY = Die.Alu.Add16(Registers.IY, Registers.PrimarySet.BC);
                    break;
                case Register16Table.DE:
                    Registers.IY = Die.Alu.Add16(Registers.IY, Registers.PrimarySet.DE);
                    break;
                case Register16Table.HL:
                    Registers.IY = Die.Alu.Add16(Registers.IY, Registers.IY);
                    break;
                case Register16Table.SP:
                    Registers.IY = Die.Alu.Add16(Registers.IY, Registers.SP);
                    break;
            }
        }

        private void AddCarry16(Register16Table reg16)
        {
            switch (reg16)
            {
                case Register16Table.BC:
                    Registers.PrimarySet.HL = Die.Alu.Add16(Registers.PrimarySet.HL, Registers.PrimarySet.BC, addCarry: true);
                    break;
                case Register16Table.DE:
                    Registers.PrimarySet.HL = Die.Alu.Add16(Registers.PrimarySet.HL, Registers.PrimarySet.DE, addCarry: true);
                    break;
                case Register16Table.HL:
                    Registers.PrimarySet.HL = Die.Alu.Add16(Registers.PrimarySet.HL, Registers.PrimarySet.HL, addCarry: true);
                    break;
                case Register16Table.SP:
                    Registers.PrimarySet.HL = Die.Alu.Add16(Registers.PrimarySet.HL, Registers.SP, addCarry: true);
                    break;
            }
        }
    }
}
