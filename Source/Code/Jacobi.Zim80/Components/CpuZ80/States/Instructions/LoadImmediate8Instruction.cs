using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using System;

namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class LoadImmediate8Instruction : ReadParametersInstruction
    {
        public LoadImmediate8Instruction(Die die)
            : base(die)
        { }

        protected override void OnExecute()
        {
            var ob = ExecutionEngine.MultiByteOpcode.GetParameter(0);

            switch (ExecutionEngine.Opcode.Definition.Register8FromY)
            {
                case Register8Table.B:
                    Registers.PrimarySet.B = ob.Value;
                    break;
                case Register8Table.C:
                    Registers.PrimarySet.C = ob.Value;
                    break;
                case Register8Table.D:
                    Registers.PrimarySet.D = ob.Value;
                    break;
                case Register8Table.E:
                    Registers.PrimarySet.E = ob.Value;
                    break;
                case Register8Table.H:
                    Registers.PrimarySet.H = ob.Value;
                    break;
                case Register8Table.L:
                    Registers.PrimarySet.L = ob.Value;
                    break;
                case Register8Table.A:
                    Registers.PrimarySet.A = ob.Value;
                    break;
            }
        }
    }
}
