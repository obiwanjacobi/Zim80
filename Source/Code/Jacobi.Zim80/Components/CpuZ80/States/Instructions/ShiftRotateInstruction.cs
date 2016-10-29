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
                switch (ExecutionEngine.Opcode.Definition.ShiftRotateFromY)
                {
                    case ShiftRotateOperations.RotateLeftCarry:
                        Registers.PrimarySet.A = Die.Alu.RotateLeftCarryA(Registers.PrimarySet.A);
                        break;
                    case ShiftRotateOperations.RotateRightCarry:
                        Registers.PrimarySet.A = Die.Alu.RotateRightCarryA(Registers.PrimarySet.A);
                        break;
                    case ShiftRotateOperations.RotateLeft:
                        Registers.PrimarySet.A = Die.Alu.RotateLeftA(Registers.PrimarySet.A);
                        break;
                    case ShiftRotateOperations.RotateRight:
                        Registers.PrimarySet.A = Die.Alu.RotateRightA(Registers.PrimarySet.A);
                        break;
                    default:
                        throw Errors.AssignedToIllegalOpcode();
                }
            }
            else
            {
                var reg = ExecutionEngine.Opcode.Definition.Register8FromZ;
                var op = ExecutionEngine.Opcode.Definition.ShiftRotateFromY;
                
                Die.Alu.DoShiftRotate(op, reg);
            }
        }
    }
}
