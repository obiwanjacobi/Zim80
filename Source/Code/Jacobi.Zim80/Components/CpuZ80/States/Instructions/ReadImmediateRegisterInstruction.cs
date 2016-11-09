using Jacobi.Zim80.Components.CpuZ80.Opcodes;

namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class ReadImmediateRegisterInstruction : ReadParametersInstruction
    {
        public ReadImmediateRegisterInstruction(Die die) 
            : base(die)
        { }

        private ReadT3InstructionPart _instructionM4;
        private ReadT3InstructionPart _instructionM5;

        protected override CpuState GetInstructionPart(MachineCycleNames machineCycle)
        {
            var regA = ExecutionEngine.Opcode.Definition.P == 3;

            switch (machineCycle)
            {
                case MachineCycleNames.M4:
                    _instructionM4 = new ReadT3InstructionPart(Die, machineCycle, GetAddress());
                    return _instructionM4;
                case MachineCycleNames.M5:
                    if (regA) throw Errors.AssignedToIllegalOpcode();
                    _instructionM5 = new ReadT3InstructionPart(Die, machineCycle, (ushort)(GetAddress() + 1));
                    return _instructionM5;
            }

            return base.GetInstructionPart(machineCycle);
        }

        protected override void OnLastCycleLastM()
        {
            var regA = ExecutionEngine.Opcode.Definition.P == 3;

            if (regA)
            {
                Registers.A = _instructionM4.Data.Value;
            }
            else if (ExecutionEngine.Opcode.Definition.IsIX)
            {
                Registers.GetIX().SetLo(_instructionM4.Data.Value);
                Registers.GetIX().SetHi(_instructionM5.Data.Value);
            }
            else if (ExecutionEngine.Opcode.Definition.IsIY)
            {
                Registers.GetIY().SetLo(_instructionM4.Data.Value);
                Registers.GetIY().SetHi(_instructionM5.Data.Value);
            }
            else
            {
                Registers.L = _instructionM4.Data.Value;
                Registers.H = _instructionM5.Data.Value;
            }

            base.OnLastCycleLastM();
        }

        private ushort GetAddress()
        {
            return OpcodeByte.MakeUInt16(InstructionM2.Data, InstructionM3.Data);
        }
    }
}
