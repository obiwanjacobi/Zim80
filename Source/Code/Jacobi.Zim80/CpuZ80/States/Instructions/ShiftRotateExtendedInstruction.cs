namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class ShiftRotateExtendedInstruction : ExtendedInstruction
    {
        public ShiftRotateExtendedInstruction(Die die) 
            : base(die)
        { }

        protected override byte ExecuteOperation(byte value)
        {
            return Die.Alu.DoShiftRotate(
                ExecutionEngine.Opcode.Definition.ShiftRotateFromY, value);
        }
    }
}
