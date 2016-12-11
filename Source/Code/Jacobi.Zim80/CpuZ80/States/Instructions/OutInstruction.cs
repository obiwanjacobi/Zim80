using Jacobi.Zim80.CpuZ80.Opcodes;

namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class OutInstruction : MultiCycleInstruction
    {
        private ReadT3InstructionPart _readIndirectPart;
        private OutputInstructionPart _outputPart;

        public OutInstruction(CpuZ80 cpu) 
            : base(cpu)
        { }

        protected override CpuState GetInstructionPart(MachineCycleNames machineCycle)
        {
            switch (machineCycle)
            {
                case MachineCycleNames.M2:
                    if (IsIndirect)
                        return CreateReadIndirectPart(machineCycle);
                    return CreateOutputPart(machineCycle);
                case MachineCycleNames.M3:
                    if (!IsIndirect) throw Errors.InvalidMachineCycle(machineCycle);
                    return CreateOutputPart(machineCycle);
                default:
                    throw Errors.InvalidMachineCycle(machineCycle);
            }
        }

        private CpuState CreateReadIndirectPart(MachineCycleNames machineCycle)
        {
            _readIndirectPart = new Instructions.ReadT3InstructionPart(Cpu, machineCycle);
            return _readIndirectPart;
        }

        private CpuState CreateOutputPart(MachineCycleNames machineCycle)
        {
            _outputPart = new OutputInstructionPart(Cpu, machineCycle, GetAddress())
            {
                Data = new OpcodeByte(GetValue())
            };
            return _outputPart;
        }

        private byte GetValue()
        {
            if (IsIndirect)
                return Registers.A;

            var reg = ExecutionEngine.Opcode.Definition.Register8FromY;
            if (reg == Register8Table.HL) return 0;
            return Registers[reg];
        }

        private ushort GetAddress()
        {
            if (IsIndirect)
            {
                return OpcodeByte.MakeUInt16(
                    _readIndirectPart.Data,
                    new OpcodeByte(Registers.A));
            }

            return Registers.BC;
        }

        private bool IsIndirect
        {
            // non ED instruction is indirect
            get { return ExecutionEngine.Opcode.Definition.Ext1 == 0; }
        }
    }
}
