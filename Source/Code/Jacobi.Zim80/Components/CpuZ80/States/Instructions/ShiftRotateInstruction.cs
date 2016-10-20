using Jacobi.Zim80.Components.CpuZ80.Opcodes;

namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class ShiftRotateInstruction : SingleCycleInstruction
    {
        public ShiftRotateInstruction(Die die) 
            : base(die)
        { }

        protected override void OnLastCycleFirstM()
        {
            var isCB = ExecutionEngine.Opcode.Definition.Ext1 == 0xCB;

            if (!isCB)
            {
                // different flag results
                switch (ExecutionEngine.Opcode.Definition.Y)
                {
                    case 0: //rlca
                        Registers.PrimarySet.A = Die.Alu.RotateLeftCarryA(Registers.PrimarySet.A);
                        break;
                    case 1: //rrca
                        Registers.PrimarySet.A = Die.Alu.RotateRightCarryA(Registers.PrimarySet.A);
                        break;
                    case 2: //rla
                        Registers.PrimarySet.A = Die.Alu.RotateLeftA(Registers.PrimarySet.A);
                        break;
                    case 3: //rra
                        Registers.PrimarySet.A = Die.Alu.RotateRightA(Registers.PrimarySet.A);
                        break;
                    default:
                        throw Errors.AssignedToIllegalOpcode();
                }
            }
            else
            {
                var reg = ExecutionEngine.Opcode.Definition.Register8FromZ;

                switch (ExecutionEngine.Opcode.Definition.Y)
                {
                    case 0: //rlc
                        Registers.PrimarySet[reg] = Die.Alu.RotateLeftCarry(Registers.PrimarySet[reg]);
                        break;
                    case 1: //rrc
                        Registers.PrimarySet[reg] = Die.Alu.RotateRightCarry(Registers.PrimarySet[reg]);
                        break;
                    case 2: //rl
                        Registers.PrimarySet[reg] = Die.Alu.RotateLeft(Registers.PrimarySet[reg]);
                        break;
                    case 3: //rr
                        Registers.PrimarySet[reg] = Die.Alu.RotateRight(Registers.PrimarySet[reg]);
                        break;
                    case 4: //sla
                        Registers.PrimarySet[reg] = Die.Alu.ShiftLeftArithmetic(Registers.PrimarySet[reg]);
                        break;
                    case 5: //sra
                        Registers.PrimarySet[reg] = Die.Alu.ShiftRightArithmetic(Registers.PrimarySet[reg]);
                        break;
                    case 6: //sll
                        Registers.PrimarySet[reg] = Die.Alu.ShiftLeftLogical(Registers.PrimarySet[reg]);
                        break;
                    case 7: //srl
                        Registers.PrimarySet[reg] = Die.Alu.ShiftRightLogical(Registers.PrimarySet[reg]);
                        break;
                }
            }
        }
    }
}
