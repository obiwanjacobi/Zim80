using Jacobi.Zim80.Components.CpuZ80.Opcodes;

namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class IndirectBitSetResInstruction : MultiCycleInstruction
    {
        private ReadT3InstructionPart _instructionM2;
        private ushort _address;

        public IndirectBitSetResInstruction(Die die)
            : base(die)
        { }

        protected override CpuState GetInstructionPart(MachineCycleNames machineCycle)
        {
            switch (machineCycle)
            {
                case MachineCycleNames.M2:
                    _instructionM2 = new ReadT3InstructionPart(Die, machineCycle, _address);
                    return _instructionM2;
                case MachineCycleNames.M3:
                    if (IsBitInstruction) throw Errors.AssignedToIllegalOpcode();
                    return new WriteT3InstructionPart(Die, machineCycle, _address)
                    {
                        Data = new OpcodeByte(GetValue())
                    };
                default:
                    throw Errors.AssignedToIllegalOpcode();
            }
        }

        protected override void OnLastCycleFirstM()
        {
            _address = Registers.HL;

            base.OnLastCycleFirstM();
        }

        protected override void OnLastCycleLastM()
        {
            if (IsBitInstruction)
                GetValue();

            base.OnLastCycleLastM();
        }

        private byte GetValue()
        {
            var bit = ExecutionEngine.Opcode.Definition.Y;
            var value = _instructionM2.Data.Value;

            switch (ExecutionEngine.Opcode.Definition.X)
            {
                case 1: // bit
                    Die.Alu.TestBit(bit, value);
                    return 0;
                case 2: // res
                    return Die.Alu.ResetBit(bit, value);
                case 3: // set
                    return Die.Alu.SetBit(bit, value);
                default:
                    throw Errors.AssignedToIllegalOpcode();
            }
        }

        private bool IsBitInstruction
        {
            get { return ExecutionEngine.Opcode.Definition.X == 1; }
        }
    }
}
