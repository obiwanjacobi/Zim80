using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using System;

namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class WriteIndirectRegisterInstruction : IndirectRegisterInstruction
    {
        private WriteT3InstructionPart _instructionPart;

        public WriteIndirectRegisterInstruction(ExecutionEngine executionEngine) 
            : base(executionEngine)
        { }

        protected override CpuState GetInstructionPart(MachineCycleNames machineCycle)
        {
            switch (machineCycle)
            {
                case MachineCycleNames.M2:
                    _instructionPart = new WriteT3InstructionPart(ExecutionEngine, machineCycle, GetAddress());
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
            _instructionPart.Data = new OpcodeByte(ExecutionEngine.Die.Registers.PrimarySet.A);
        }
    }
}
