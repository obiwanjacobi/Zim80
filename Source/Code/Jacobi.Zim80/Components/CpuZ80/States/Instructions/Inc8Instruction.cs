namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class Inc8Instruction : SingleByteInstruction
    {
        public Inc8Instruction(ExecutionEngine executionEngine)
            : base(executionEngine)
        { }

        protected override void OnExecute()
        {
            switch (ExecutionEngine.Opcode.Definition.Register8FromY)
            {
                case Opcodes.Register8Table.B:
                    ExecutionEngine.Die.Registers.PrimarySet.B++;
                    break;
                case Opcodes.Register8Table.C:
                    ExecutionEngine.Die.Registers.PrimarySet.C++;
                    break;
                case Opcodes.Register8Table.D:
                    ExecutionEngine.Die.Registers.PrimarySet.D++;
                    break;
                case Opcodes.Register8Table.E:
                    ExecutionEngine.Die.Registers.PrimarySet.E++;
                    break;
                case Opcodes.Register8Table.H:
                    ExecutionEngine.Die.Registers.PrimarySet.H++;
                    break;
                case Opcodes.Register8Table.L:
                    ExecutionEngine.Die.Registers.PrimarySet.L++;
                    break;
                case Opcodes.Register8Table.A:
                    ExecutionEngine.Die.Registers.PrimarySet.A++;
                    break;
            }
        }
    }
}
