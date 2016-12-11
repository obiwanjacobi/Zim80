namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class ShiftRotateExtendedInstruction : ExtendedInstruction
    {
        public ShiftRotateExtendedInstruction(CpuZ80 cpu) 
            : base(cpu)
        { }

        protected override byte ExecuteOperation(byte value)
        {
            return Cpu.Alu.DoShiftRotate(
                ExecutionEngine.Opcode.Definition.ShiftRotateFromY, value);
        }
    }
}
