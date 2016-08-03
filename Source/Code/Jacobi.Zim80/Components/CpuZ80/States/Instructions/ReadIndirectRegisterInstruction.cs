using System;

namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class ReadIndirectRegisterInstruction : MultiByteInstruction
    {
        private ReadT3Instruction _instructionPart;

        public ReadIndirectRegisterInstruction(ExecutionEngine executionEngine) 
            : base(executionEngine)
        { }

        protected override CpuState GetInstructionPart(MachineCycleNames machineCycle)
        {
            switch (machineCycle)
            {
                case MachineCycleNames.M2:
                    _instructionPart = new ReadT3Instruction(ExecutionEngine, machineCycle, GetAddress());
                    return _instructionPart;
                default:
                    throw new InvalidOperationException("Invalid machine cycle: " + machineCycle);
            }
        }

        protected override void OnExecute()
        {
            var z = ExecutionEngine.Opcode.Definition.Z;
            var q = ExecutionEngine.Opcode.Definition.Q;
            var p = ExecutionEngine.Opcode.Definition.P;

            // TODO: check z -for other instructions

            ExecutionEngine.Die.Registers.PrimarySet.A = _instructionPart.Data.Value;
        }

        private UInt16 GetAddress()
        {
            var z = ExecutionEngine.Opcode.Definition.Z;
            var q = ExecutionEngine.Opcode.Definition.Q;
            var p = ExecutionEngine.Opcode.Definition.P;

            // TODO: check z -for other instructions

            switch (p)
            {
                case 0:
                    return ExecutionEngine.Die.Registers.PrimarySet.BC;
                case 1:
                    return ExecutionEngine.Die.Registers.PrimarySet.DE;
            }

            throw new InvalidOperationException();
        }
    }
}
