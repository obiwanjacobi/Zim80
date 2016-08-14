using System;

namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal abstract class ReadParametersInstruction : MultiByteInstruction
    {
        public ReadParametersInstruction(ExecutionEngine executionEngine) 
            : base(executionEngine)
        { }

        protected ReadT3InstructionPart InstructionM2 { get; private set; }
        protected ReadT3InstructionPart InstructionM3 { get; private set; }

        protected override CpuState GetInstructionPart(MachineCycleNames machineCycle)
        {
            if (!ExecutionEngine.Opcode.Definition.HasParameters)
                throw new InvalidOperationException("Use a SingleByteInstruction base class for parameter-less instructions.");

            switch (machineCycle)
            {
                case MachineCycleNames.M2:
                    InstructionM2 = new ReadT3InstructionPart(ExecutionEngine, machineCycle);
                    return InstructionM2;
                case MachineCycleNames.M3:
                    if (!ExecutionEngine.Opcode.Definition.nn)
                        ThrowInvalidMachineCycle(machineCycle);

                    InstructionM3 = new ReadT3InstructionPart(ExecutionEngine, machineCycle);
                    return InstructionM3;
                default:
                    ThrowInvalidMachineCycle(machineCycle);
                    break;
            }

            return null;
        }
    }
}
