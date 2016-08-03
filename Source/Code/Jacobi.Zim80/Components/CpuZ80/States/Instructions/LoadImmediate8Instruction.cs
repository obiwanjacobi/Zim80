using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using System;

namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class LoadImmediate8Instruction : ReadParametersInstruction
    {
        public LoadImmediate8Instruction(ExecutionEngine executionEngine) 
            : base(executionEngine)
        { }

        protected override void OnExecute()
        {
            var ob = ExecutionEngine.MultiByteOpcode.GetParameter(0);

            switch (ExecutionEngine.Opcode.Definition.Register8FromY)
            {
                case Register8Table.B:
                    ExecutionEngine.Die.Registers.PrimarySet.B = ob.Value;
                    break;
                case Register8Table.C:
                    ExecutionEngine.Die.Registers.PrimarySet.C = ob.Value;
                    break;
                case Register8Table.D:
                    ExecutionEngine.Die.Registers.PrimarySet.D = ob.Value;
                    break;
                case Register8Table.E:
                    ExecutionEngine.Die.Registers.PrimarySet.E = ob.Value;
                    break;
                case Register8Table.H:
                    ExecutionEngine.Die.Registers.PrimarySet.H = ob.Value;
                    break;
                case Register8Table.L:
                    ExecutionEngine.Die.Registers.PrimarySet.L = ob.Value;
                    break;
                case Register8Table.A:
                    ExecutionEngine.Die.Registers.PrimarySet.A = ob.Value;
                    break;
            }
        }
    }
}
