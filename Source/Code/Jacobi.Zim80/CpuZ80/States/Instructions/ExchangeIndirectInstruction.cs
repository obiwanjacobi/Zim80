using Jacobi.Zim80.CpuZ80.Opcodes;

namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class ExchangeIndirectInstruction : MultiCycleInstruction
    {
        private ReadT3InstructionPart _instructionM2;
        private ReadT3InstructionPart _instructionM3;

        public ExchangeIndirectInstruction(Die die) 
            : base(die)
        { }

        protected override void OnLastCycleLastM()
        {
            base.OnLastCycleLastM();

            if (ExecutionEngine.Opcode.Definition.IsIX)
            {
                Registers.GetIX().SetLo(_instructionM2.Data.Value);
                Registers.GetIX().SetHi(_instructionM3.Data.Value);
            }
            else if (ExecutionEngine.Opcode.Definition.IsIY)
            {
                Registers.GetIY().SetLo(_instructionM2.Data.Value);
                Registers.GetIY().SetHi(_instructionM3.Data.Value);
            }
            else
            {
                Registers.L = _instructionM2.Data.Value;
                Registers.H = _instructionM3.Data.Value;
            }
        }

        protected override CpuState GetInstructionPart(MachineCycleNames machineCycle)
        {
            switch (machineCycle)
            {
                case MachineCycleNames.M2:
                    _instructionM2 = new ReadT3InstructionPart(Die, machineCycle, Registers.SP);
                    return _instructionM2;
                case MachineCycleNames.M3:
                    _instructionM3 = new ReadT3InstructionPart(Die, machineCycle, (ushort)(Registers.SP + 1));
                    return _instructionM3;
                case MachineCycleNames.M4:
                    return new WriteT3InstructionPart(Die, machineCycle, Registers.SP)
                    {
                        Data = new OpcodeByte(GetValueLo())
                    };
                case MachineCycleNames.M5:
                    return new WriteT3InstructionPart(Die, machineCycle, (ushort)(Registers.SP + 1))
                    {
                        Data = new OpcodeByte(GetValueHi())
                    };
                default:
                    throw Errors.InvalidMachineCycle(machineCycle);
            }
        }

        private byte GetValueLo()
        {
            if (ExecutionEngine.Opcode.Definition.IsIX)
                return Registers.GetIX().GetLo();
            else if (ExecutionEngine.Opcode.Definition.IsIY)
                return Registers.GetIY().GetLo();
            else
                return Registers.L;
        }

        private byte GetValueHi()
        {
            if (ExecutionEngine.Opcode.Definition.IsIX)
                return Registers.GetIX().GetHi();
            else if (ExecutionEngine.Opcode.Definition.IsIY)
                return Registers.GetIY().GetHi();
            else
                return Registers.H;
        }
    }
}
