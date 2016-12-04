using Jacobi.Zim80.CpuZ80.Opcodes;

namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class CallInstruction : ReadParametersInstruction
    {
        public CallInstruction(Die die)
            : base(die)
        { }

        protected override CpuState GetInstructionPart(MachineCycleNames machineCycle)
        {
            switch (machineCycle)
            {
                // read parameters (by base class)
                case MachineCycleNames.M2:
                case MachineCycleNames.M3:
                    return base.GetInstructionPart(machineCycle);
                // just like push
                case MachineCycleNames.M4:
                    Registers.SP--;
                    return CreateInstructionPartM4();
                case MachineCycleNames.M5:
                    Registers.SP--;
                    return CreateInstructionPartM5();
                default:
                    throw Errors.InvalidMachineCycle(machineCycle);
            }
        }

        protected override void OnClockPos()
        {
            if (ExecutionEngine.Cycles.IsLastCycle)
            {
                switch (ExecutionEngine.Cycles.MachineCycle)
                {
                    case MachineCycleNames.M3:
                        if (!IsConditionMet())
                        {
                            ExecutionEngine.Cycles.SetAltCycles();
                        }
                        break;
                    case MachineCycleNames.M5:
                        Registers.GetPC().SetLo(base.InstructionM2.Data.Value);
                        Registers.GetPC().SetHi(base.InstructionM3.Data.Value);
                        break;
                }
            }

            base.OnClockPos();
        }

        protected virtual bool IsConditionMet()
        {
            if (ExecutionEngine.Opcode.Definition.Z == 4)
            {
                return Registers.FlagFromOpcodeY(ExecutionEngine.Opcode.Definition.Y);
            }
            else if (ExecutionEngine.Opcode.Definition.Z == 5)
            {
                // call (no condition)
                return true;
            }

            throw Errors.AssignedToIllegalOpcode();
        }

        #region Private
        private WriteT3InstructionPart CreateInstructionPartM4()
        {
            return new WriteT3InstructionPart(Die, MachineCycleNames.M4, Registers.SP)
            {
                Data = new OpcodeByte(Registers.GetPC().GetHi())
            };
        }

        private WriteT3InstructionPart CreateInstructionPartM5()
        {
            return new WriteT3InstructionPart(Die, MachineCycleNames.M5, Registers.SP)
            {
                Data = new OpcodeByte(Registers.GetPC().GetLo())
            };
        }
        #endregion
    }
}
