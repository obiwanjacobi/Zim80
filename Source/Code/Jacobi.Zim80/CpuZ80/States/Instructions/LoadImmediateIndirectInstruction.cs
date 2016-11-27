namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class LoadImmediateIndirectInstruction : ReadParametersInstruction
    {
        public LoadImmediateIndirectInstruction(Die die) 
            : base(die)
        { }

        protected override CpuState GetInstructionPart(MachineCycleNames machineCycle)
        {
            switch (machineCycle)
            {
                case MachineCycleNames.M3:
                    if (IsHL)
                        return new WriteT3InstructionPart(Die, machineCycle, Registers.HL)
                        {
                            Data = InstructionM2.Data
                        };
                    break;  // another param
                case MachineCycleNames.M4:
                    if (IsHL) throw Errors.InvalidMachineCycle(machineCycle);
                    return new WriteT3InstructionPart(Die, machineCycle, GetAddress())
                    {
                        Data = InstructionM3.Data
                    };
                case MachineCycleNames.M5:
                case MachineCycleNames.M6:
                    throw Errors.InvalidMachineCycle(machineCycle);
            }

            return base.GetInstructionPart(machineCycle);
        }

        private bool IsHL
        {
            get { return ExecutionEngine.Opcode.Definition.Ext1 == 0; }
        }

        private ushort GetAddress()
        {
            ushort address;

            if (ExecutionEngine.Opcode.Definition.IsIX)
                address = Registers.IX;
            else if (ExecutionEngine.Opcode.Definition.IsIY)
                address = Registers.IY;
            else
                throw Errors.AssignedToIllegalOpcode();

            return Alu.Add(address, (sbyte)InstructionM2.Data.Value);
        }
    }
}
