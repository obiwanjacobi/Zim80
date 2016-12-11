namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class PopInstruction : MultiCycleInstruction
    {
        public PopInstruction(CpuZ80 cpu) 
            : base(cpu)
        { }

        protected override CpuState GetInstructionPart(MachineCycleNames machineCycle)
        {
            switch (machineCycle)
            {
                case MachineCycleNames.M2:
                    _instructionM2 = new ReadT3InstructionPart(Cpu, machineCycle, Registers.SP);
                    return _instructionM2;
                case MachineCycleNames.M3:
                    _instructionM3 = new ReadT3InstructionPart(Cpu, machineCycle, Registers.SP);
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
                    Registers.B = value;
                    break;
                case 1:
                    Registers.D = value;
                    break;
                case 2:
                    if (!ExecuteShiftedInstructionHi(value))
                        Registers.H = value;
                    break;
                case 3:
                    Registers.A = value;
                    break;
            }
        }

        protected virtual void SetRegisterLowValue(byte value)
        {
            switch (ExecutionEngine.Opcode.Definition.P)
            {
                case 0:
                    Registers.C = value;
                    break;
                case 1:
                    Registers.E = value;
                    break;
                case 2:
                    if (!ExecuteShiftedInstructionLo(value))
                        Registers.L = value;
                    break;
                case 3:
                    Registers.F = value;
                    break;
            }
        }

        #region Private
        private ReadT3InstructionPart _instructionM2;
        private ReadT3InstructionPart _instructionM3;

        private bool ExecuteShiftedInstructionHi(byte value)
        {
            if (ExecutionEngine.Opcode.Definition.IsIX)
            {
                Registers.GetIX().SetHi(value);
                return true;
            }
            if (ExecutionEngine.Opcode.Definition.IsIY)
            {
                Registers.GetIY().SetHi(value);
                return true;
            }

            return false;
        }

        private bool ExecuteShiftedInstructionLo(byte value)
        {
            if (ExecutionEngine.Opcode.Definition.IsIX)
            {
                Registers.GetIX().SetLo(value);
                return true;
            }
            if (ExecutionEngine.Opcode.Definition.IsIY)
            {
                Registers.GetIY().SetLo(value);
                return true;
            }

            return false;
        }
        #endregion
    }
}
