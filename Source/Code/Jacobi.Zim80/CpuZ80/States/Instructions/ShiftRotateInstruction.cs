using Jacobi.Zim80.CpuZ80.Opcodes;

namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class ShiftRotateInstruction : SingleCycleInstruction
    {
        public ShiftRotateInstruction(CpuZ80 cpu) 
            : base(cpu)
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
                        Registers.A = Cpu.Alu.RotateLeftCarryA(Registers.A);
                        break;
                    case ShiftRotateOperations.RotateRightCarry:
                        Registers.A = Cpu.Alu.RotateRightCarryA(Registers.A);
                        break;
                    case ShiftRotateOperations.RotateLeft:
                        Registers.A = Cpu.Alu.RotateLeftA(Registers.A);
                        break;
                    case ShiftRotateOperations.RotateRight:
                        Registers.A = Cpu.Alu.RotateRightA(Registers.A);
                        break;
                    default:
                        throw Errors.AssignedToIllegalOpcode();
                }
            }
            else
            {
                var reg = ExecutionEngine.Opcode.Definition.Register8FromZ;
                var op = ExecutionEngine.Opcode.Definition.ShiftRotateFromY;
                
                Cpu.Alu.DoShiftRotate(op, reg);
            }
        }
    }
}
