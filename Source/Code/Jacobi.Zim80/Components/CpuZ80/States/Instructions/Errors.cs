using System;

namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal static class Errors
    {
        public static Exception AssignedToIllegalOpcode()
        {
            return new InvalidOperationException("Instruction assigned to an illegal opcode.");
        }

        public static Exception InvalidMachineCycle(MachineCycleNames machineCycle)
        {
            return new InvalidOperationException("The implementation of the instruction does not expect this machine cycle. Invalid machine cycle: " + machineCycle);
        }

        public static Exception NoParameters()
        {
            return new InvalidOperationException("Use a SingleByteInstruction base class for parameter-less instructions.");
        }

        public static Exception NextInstructionPartIsNull()
        {
            return new InvalidOperationException("GetInstructionPart returned null.");
        }

        public static Exception InstructionPartWasNotCompleted()
        {
            return new InvalidOperationException("InstructionPart was not completed!");
        }
    }
}
