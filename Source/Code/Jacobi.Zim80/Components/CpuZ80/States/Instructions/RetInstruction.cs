namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class RetInstruction : PopInstruction
    {
        public RetInstruction(Die die) 
            : base(die)
        { }

        protected override void OnLastCycleFirstM()
        {
            if (ExecutionEngine.Opcode.Definition.AltCycles != null &&
                !IsConditionMet())
            {
                ExecutionEngine.Cycles.SetAltCycles();
            }
        }

        protected virtual bool IsConditionMet()
        {
            if (ExecutionEngine.Opcode.Definition.Z == 0)
            {
                switch (ExecutionEngine.Opcode.Definition.Y)
                {
                    case 0: // nz
                        return !Registers.PrimarySet.Flags.Z;
                    case 1: // z
                        return Registers.PrimarySet.Flags.Z;
                    case 2: // nc
                        return !Registers.PrimarySet.Flags.C;
                    case 3: // c
                        return Registers.PrimarySet.Flags.C;
                    case 4: // po
                        return !Registers.PrimarySet.Flags.PV;
                    case 5: // pe
                        return Registers.PrimarySet.Flags.PV;
                    case 6: // p
                        return !Registers.PrimarySet.Flags.S;
                    case 7: // m
                        return Registers.PrimarySet.Flags.S;
                }
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
