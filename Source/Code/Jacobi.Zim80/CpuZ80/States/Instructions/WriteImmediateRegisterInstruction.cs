using Jacobi.Zim80.CpuZ80.Opcodes;

namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class WriteImmediateRegisterInstruction : ReadParametersInstruction
    {
        public WriteImmediateRegisterInstruction(CpuZ80 cpu) 
            : base(cpu)
        { }

        protected override CpuState GetInstructionPart(MachineCycleNames machineCycle)
        {
            switch (machineCycle)
            {
                case MachineCycleNames.M4:
                    return new WriteT3InstructionPart(Cpu, machineCycle, GetAddress())
                    {
                        Data = new OpcodeByte(GetValueLo())
                    };
                case MachineCycleNames.M5:
                    var regA = ExecutionEngine.Opcode.Definition.P == 3;
                    if (regA) throw Errors.AssignedToIllegalOpcode();

                    return new WriteT3InstructionPart(Cpu, machineCycle, (ushort)(GetAddress() + 1))
                    {
                        Data = new OpcodeByte(GetValueHi())
                    };
            }

            return base.GetInstructionPart(machineCycle);
        }

        private ushort GetAddress()
        {
            return OpcodeByte.MakeUInt16(InstructionM2.Data, InstructionM3.Data);
        }

        private byte GetValueLo()
        {
            var regA = ExecutionEngine.Opcode.Definition.P == 3;

            if (regA)
            {
                return Registers.A;
            }
            else if (ExecutionEngine.Opcode.Definition.IsIX)
            {
                return Registers.GetIX().GetLo();
            }
            else if (ExecutionEngine.Opcode.Definition.IsIY)
            {
                return Registers.GetIY().GetLo();
            }
            else
            {
                return Registers.L;
            }
        }

        private byte GetValueHi()
        {
            if (ExecutionEngine.Opcode.Definition.IsIX)
            {
                return Registers.GetIX().GetHi();
            }
            else if (ExecutionEngine.Opcode.Definition.IsIY)
            {
                return Registers.GetIY().GetHi();
            }
            else
            {
                return Registers.H;
            }
        }
    }
}
