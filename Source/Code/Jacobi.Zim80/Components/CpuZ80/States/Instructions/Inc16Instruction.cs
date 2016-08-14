using Jacobi.Zim80.Components.CpuZ80.Opcodes;

namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class Inc16Instruction : SingleByteInstruction
    {
        public Inc16Instruction(Die die)
            : base(die)
        { }

        protected override void OnExecute()
        {
            switch (ExecutionEngine.Opcode.Definition.Register16FromP)
            {
                case Register16Table.BC:
                    Registers.PrimarySet.BC =
                        Die.Alu.Inc16(Registers.PrimarySet.BC);
                    break;
                case Register16Table.DE:
                    Registers.PrimarySet.DE =
                        Die.Alu.Inc16(Registers.PrimarySet.DE);
                    break;
                case Register16Table.HL:
                    Registers.PrimarySet.HL =
                        Die.Alu.Inc16(Registers.PrimarySet.HL);
                    break;
                case Register16Table.SP:
                    Registers.SP =
                        Die.Alu.Inc16(Registers.SP);
                    break;
            }
        }
    }
}
