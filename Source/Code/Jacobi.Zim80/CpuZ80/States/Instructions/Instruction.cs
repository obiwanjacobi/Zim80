using Jacobi.Zim80.CpuZ80.Opcodes;
using System;

namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal abstract class Instruction : CpuState
    {
        protected Instruction(Die die)
            : base(die)
        { }

        protected Registers Registers { get { return Die.Registers; } }

        protected abstract void OnLastCycleFirstM();

        internal static T Create<T>(Die die, OpcodeDefinition opcodeDefinition)
            where T : Instruction
        {
            if (opcodeDefinition == null)
                throw new ArgumentNullException("opcodeDefintion", 
                    "There is no OpcodeDefinition available.");
            if (opcodeDefinition.Instruction == null)
                throw new InvalidOperationException(
                    "The OpcodeDefinition has no associated Instruction: "
                        + opcodeDefinition.ToString());
            if (!typeof(T).IsAssignableFrom(opcodeDefinition.Instruction))
                throw Errors.AssignedToIllegalOpcode();

            return (T)Activator.CreateInstance(opcodeDefinition.Instruction, die);
        }
    }
}
