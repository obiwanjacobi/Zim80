using System;

namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal abstract class ReadParametersInstruction : MultiByteInstruction
    {
        public ReadParametersInstruction(ExecutionEngine executionEngine) 
            : base(executionEngine)
        { }

        protected override CpuState GetInstructionPart(MachineCycleNames machineCycle)
        {
            if (!ExecutionEngine.Opcode.Definition.HasParameters)
                throw new InvalidOperationException("Use a SingleByteInstruction base class for parameter-less instructions.");

            switch (machineCycle)
            {
                case MachineCycleNames.M2:
                    return new ReadT3Instruction(ExecutionEngine, machineCycle);
                case MachineCycleNames.M3:
                    if (!ExecutionEngine.Opcode.Definition.nn)
                        throw new InvalidOperationException("Invalid machine cycle.");

                    return new ReadT3Instruction(ExecutionEngine, machineCycle);
                default:
                    throw new InvalidOperationException("Invalid machine cycle: " + machineCycle);
            }
        }
    }
}
