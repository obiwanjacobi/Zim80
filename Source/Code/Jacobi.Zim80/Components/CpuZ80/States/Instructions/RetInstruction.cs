namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class RetInstruction : PopInstruction
    {
        public RetInstruction(Die die) 
            : base(die)
        { }

        protected override void OnLastCycleFirstM()
        {
            if (!IsConditionMet())
            {
                ExecutionEngine.Cycles.SetAltCycles();
            }
        }

        protected virtual bool IsConditionMet()
        {
            if (ExecutionEngine.Opcode.Definition.Z == 0)
            {
                return Registers.PrimarySet.FlagFromOpcodeY(ExecutionEngine.Opcode.Definition.Y);
            }
            else if (ExecutionEngine.Opcode.Definition.Z == 1)
            {
                // ret (no condition)
                return true;
            }

            throw Errors.AssignedToIllegalOpcode();
        }

        protected override void SetRegisterHighValue(byte value)
        {
            Registers.GetPC().SetHi(value);
        }

        protected override void SetRegisterLowValue(byte value)
        {
            Registers.GetPC().SetLo(value);
        }
    }
}
