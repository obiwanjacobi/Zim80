using System;

namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal abstract class ReadParametersInstruction : MultiByteInstruction
    {
        public ReadParametersInstruction(ExecutionEngine executionEngine) 
            : base(executionEngine)
        { }

        protected ReadT3Instruction InstructionM2 { get; private set; }
        protected ReadT3Instruction InstructionM3 { get; private set; }

        protected override CpuState GetInstructionPart(MachineCycleNames machineCycle)
        {
            if (!ExecutionEngine.Opcode.Definition.HasParameters)
                throw new InvalidOperationException("Use a SingleByteInstruction base class for parameter-less instructions.");

            switch (machineCycle)
            {
                case MachineCycleNames.M2:
                    InstructionM2 = new ReadT3Instruction(ExecutionEngine, machineCycle);
                    return InstructionM2;
                case MachineCycleNames.M3:
                    if (!ExecutionEngine.Opcode.Definition.nn)
                        throw new InvalidOperationException("Invalid machine cycle.");

                    InstructionM3 = new ReadT3Instruction(ExecutionEngine, machineCycle);
                    return InstructionM3;
                default:
                    throw new InvalidOperationException("Invalid machine cycle: " + machineCycle);
            }
        }
    }
}
