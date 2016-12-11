using Jacobi.Zim80.CpuZ80.Opcodes;

namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    /*
     * LD [ActingRegister], (IX/IY+d)
     * [Operation] [ActingRegister]
     * LD (IX/IY+d), [ActingRegister]
     * 
     * ActingRegister (B, C, D, E, H, L, A) => HL is none.
     */
    internal abstract class ExtendedInstruction : MultiCycleInstruction
    {
        private ReadT3InstructionPart _readPart;
        private WriteT3InstructionPart _writePart;

        public ExtendedInstruction(CpuZ80 cpu) 
            : base(cpu)
        { }

        // derived class performs the instruction operation
        protected abstract byte ExecuteOperation(byte value);

        protected override CpuState GetInstructionPart(MachineCycleNames machineCycle)
        {
            switch (machineCycle)
            {
                case MachineCycleNames.M2:
                    return CreateReadAddressInstructionPart(machineCycle);
                case MachineCycleNames.M3:
                    return CreateWriteAddressInstructionPart(machineCycle);
                default:
                    throw Errors.InvalidMachineCycle(machineCycle);
            }
        }

        // IX/IY+d
        protected ushort GetAddress()
        {
            return GetHLOrIXIY();
        }

        // HL == none
        protected Register8Table ActingRegister
        {
            get { return ExecutionEngine.Opcode.Definition.Register8FromZ; }
        }

        protected byte GetValue()
        {
            return _readPart.Data.Value;
        }

        private ReadT3InstructionPart CreateReadAddressInstructionPart(MachineCycleNames machineCycle)
        {
            _readPart = new ReadT3InstructionPart(Cpu, machineCycle, GetAddress());
            return _readPart;
        }

        private WriteT3InstructionPart CreateWriteAddressInstructionPart(MachineCycleNames machineCycle)
        {
            _writePart = new WriteT3InstructionPart(Cpu, machineCycle, GetAddress())
            {
                Data = new OpcodeByte(GetValueToWriteBack(GetValue()))
            };
            return _writePart;
        }

        private byte GetValueToWriteBack(byte value)
        {
            var newValue = ExecuteOperation(value);

            var reg = ActingRegister;
            if (reg != Register8Table.HL)
                Registers[reg] = newValue;

            return newValue;
        }
    }
}
