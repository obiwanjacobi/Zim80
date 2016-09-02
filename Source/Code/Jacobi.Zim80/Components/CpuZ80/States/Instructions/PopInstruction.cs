namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class PopInstruction : MultiCycleInstruction
    {
        public PopInstruction(Die die) 
            : base(die)
        { }

        protected override CpuState GetInstructionPart(MachineCycleNames machineCycle)
        {
            switch (machineCycle)
            {
                case MachineCycleNames.M2:
                    _instructionM2 = CreateInstructionPartM2();
                    return _instructionM2;
                case MachineCycleNames.M3:
                    _instructionM3 = CreateInstructionPartM3();
                    return _instructionM3;
                default:
                    throw Errors.InvalidMachineCycle(machineCycle);
            }
        }

        protected override void OnInstructionPartCompleted(CpuState completedPart)
        {
            base.OnInstructionPartCompleted(completedPart);

            if (_instructionM2 == completedPart)
            {
                SetRegisterLowValue(_instructionM2.Data.Value);
                Registers.SP++;
            }
            if (_instructionM3 == completedPart)
            {
                SetRegisterHighValue(_instructionM3.Data.Value);
                Registers.SP++;
            }
        }

        protected virtual void SetRegisterHighValue(byte value)
        {
            switch (ExecutionEngine.Opcode.Definition.P)
            {
                case 0:
                    Registers.PrimarySet.B = value;
                    break;
                case 1:
                    Registers.PrimarySet.D = value;
                    break;
                case 2:
                    if (!ExecuteShiftedInstructionHi(value))
                        Registers.PrimarySet.H = value;
                    break;
                case 3:
                    Registers.PrimarySet.A = value;
                    break;
            }
        }

        protected virtual void SetRegisterLowValue(byte value)
        {
            switch (ExecutionEngine.Opcode.Definition.P)
            {
                case 0:
                    Registers.PrimarySet.C = value;
                    break;
                case 1:
                    Registers.PrimarySet.E = value;
                    break;
                case 2:
                    if (!ExecuteShiftedInstructionLo(value))
                        Registers.PrimarySet.L = value;
                    break;
                case 3:
                    Registers.PrimarySet.F = value;
                    break;
            }
        }

        #region Private
        private ReadT3InstructionPart _instructionM2;
        private ReadT3InstructionPart _instructionM3;

        private ReadT3InstructionPart CreateInstructionPartM2()
        {
            return new ReadT3InstructionPart(Die, MachineCycleNames.M2, Registers.SP);
        }

        private ReadT3InstructionPart CreateInstructionPartM3()
        {
            return new ReadT3InstructionPart(Die, MachineCycleNames.M3, Registers.SP);
        }

        private bool ExecuteShiftedInstructionHi(byte value)
        {
            if (ExecutionEngine.Opcode.IsIX)
            {
                Registers.GetIX().SetHi(value);
                return true;
            }
            if (ExecutionEngine.Opcode.IsIY)
            {
                Registers.GetIY().SetHi(value);
                return true;
            }

            return false;
        }

        private bool ExecuteShiftedInstructionLo(byte value)
        {
            if (ExecutionEngine.Opcode.IsIX)
            {
                Registers.GetIX().SetLo(value);
                return true;
            }
            if (ExecutionEngine.Opcode.IsIY)
            {
                Registers.GetIY().SetLo(value);
                return true;
            }

            return false;
        }
        #endregion
    }
}
