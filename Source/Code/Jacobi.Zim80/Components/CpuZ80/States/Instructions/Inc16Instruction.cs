using Jacobi.Zim80.Components.CpuZ80.Opcodes;

namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class Inc16Instruction : SingleByteInstruction
    {
        public Inc16Instruction(ExecutionEngine executionEngine)
            : base(executionEngine)
        { }

        protected override void OnExecute()
        {
            switch (ExecutionEngine.Opcode.Definition.Register16FromP)
            {
                case Register16Table.BC:
                    ExecutionEngine.Die.Registers.PrimarySet.BC++;
                    break;
                case Register16Table.DE:
                    ExecutionEngine.Die.Registers.PrimarySet.DE++;
                    break;
                case Register16Table.HL:
                    ExecutionEngine.Die.Registers.PrimarySet.HL++;
                    break;
                case Register16Table.SP:
                    ExecutionEngine.Die.Registers.SP++;
                    break;
            }
        }
    }
}
