using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using System;

namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class WriteIndirectRegisterInstruction : IndirectRegisterInstruction
    {
        private WriteT3InstructionPart _instructionPart;

        public WriteIndirectRegisterInstruction(Die die)
            : base(die)
        { }

        protected override CpuState GetInstructionPart(MachineCycleNames machineCycle)
        {
            switch (machineCycle)
            {
                case MachineCycleNames.M2:
                    _instructionPart = new WriteT3InstructionPart(Die, machineCycle, GetAddress());
                    return _instructionPart;
                default:
                    ThrowInvalidMachineCycle(machineCycle);
                    break;
            }

            return null;
        }

        protected override void OnClockNeg()
        {
            OnClockNegWrite();
        }

        protected override void OnExecute()
        {
            _instructionPart.Data = new OpcodeByte(Registers.PrimarySet.A);
        }
    }
}
