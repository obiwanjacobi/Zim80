namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class MathRegister8Instruction : SingleCycleInstruction
    {
        public MathRegister8Instruction(Die die) 
            : base(die)
        { }

        protected override void OnLastCycleFirstM()
        {
            var reg = ExecutionEngine.Opcode.Definition.Register8FromZ;

            switch (ExecutionEngine.Opcode.Definition.Y)
            {
                case 0: //add
                    Registers.PrimarySet.A = Die.Alu.Add8(Registers.PrimarySet.A, Registers.PrimarySet[reg]);
                    break;
                case 1: //adc
                    Registers.PrimarySet.A = Die.Alu.Add8(Registers.PrimarySet.A, Registers.PrimarySet[reg], addCarry: true);
                    break;
                case 2: //sub
                    Registers.PrimarySet.A = Die.Alu.Sub8(Registers.PrimarySet.A, Registers.PrimarySet[reg]);
                    break;
                case 3: //sbc
                    Registers.PrimarySet.A = Die.Alu.Sub8(Registers.PrimarySet.A, Registers.PrimarySet[reg], subCarry: true);
                    break;
                case 4: //and
                    Registers.PrimarySet.A = Die.Alu.And8(Registers.PrimarySet.A, Registers.PrimarySet[reg]);
                    break;
                case 5: //xor
                    Registers.PrimarySet.A = Die.Alu.Xor8(Registers.PrimarySet.A, Registers.PrimarySet[reg]);
                    break;
                case 6: //or
                    Registers.PrimarySet.A = Die.Alu.Or8(Registers.PrimarySet.A, Registers.PrimarySet[reg]);
                    break;
                case 7: //cp
                    // compare is sub without storing the result
                    Die.Alu.Sub8(Registers.PrimarySet.A, Registers.PrimarySet[reg]);
                    break;
            }
        }
    }
}
