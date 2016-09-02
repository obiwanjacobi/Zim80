using Jacobi.Zim80.Components.CpuZ80.Opcodes;

namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class DecIndirectInstruction : IndirectRegisterInstruction
    {
        public DecIndirectInstruction(Die die) 
            : base(die)
        { }

        private ReadT3InstructionPart _instructionM2;
        private WriteT3InstructionPart _instructionM3;

        protected override CpuState GetInstructionPart(MachineCycleNames machineCycle)
        {
            switch (machineCycle)
            {
                case MachineCycleNames.M2:
                    _instructionM2 = new ReadT3InstructionPart(Die, machineCycle, GetAddress());
                    return _instructionM2;
                case MachineCycleNames.M3:
                    _instructionM3 = new WriteT3InstructionPart(Die, machineCycle, GetAddress());
                    _instructionM3.Data = new OpcodeByte(Die.Alu.Dec8(_instructionM2.Data.Value));
                    return _instructionM3;
                default:
                    ThrowInvalidMachineCycle(machineCycle);
                    break;
            }

            return null;
        }
    }
}
