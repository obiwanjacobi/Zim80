using Jacobi.Zim80.CpuZ80.Opcodes;
using System;

namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class PushInstruction : MultiCycleInstruction
    {
        public PushInstruction(Die die) 
            : base(die)
        { }
        
        protected override CpuState GetInstructionPart(MachineCycleNames machineCycle)
        {
            switch (machineCycle)
            {
                case MachineCycleNames.M2:
                    Registers.SP--;
                    return CreateInstructionPartM2();
                case MachineCycleNames.M3:
                    Registers.SP--;
                    return CreateInstructionPartM3();
                default:
                    throw Errors.InvalidMachineCycle(machineCycle);
            }
        }

        protected virtual byte GetRegisterHighValue()
        {
            switch (ExecutionEngine.Opcode.Definition.P)
            {
                case 0:
                    return Registers.B;
                case 1:
                    return Registers.D;
                case 2:
                    if (ExecutionEngine.Opcode.Definition.IsIX)
                        return Registers.GetIX().GetHi();
                    if (ExecutionEngine.Opcode.Definition.IsIY)
                        return Registers.GetIY().GetHi();
                    return Registers.H;
                case 3:
                    return Registers.A;
            }

            throw new InvalidOperationException("Invalid OpcodeDefinition.P value.");
        }

        protected virtual byte GetRegisterLowValue()
        {
            switch (ExecutionEngine.Opcode.Definition.P)
            {
                case 0:
                    return Registers.C;
                case 1:
                    return Registers.E;
                case 2:
                    if (ExecutionEngine.Opcode.Definition.IsIX)
                        return Registers.GetIX().GetLo();
                    if (ExecutionEngine.Opcode.Definition.IsIY)
                        return Registers.GetIY().GetLo();
                    return Registers.L;
                case 3:
                    return Registers.F;
            }

            throw new InvalidOperationException("Invalid OpcodeDefinition.P value.");
        }

        #region Private
        private WriteT3InstructionPart CreateInstructionPartM2()
        {
            return new WriteT3InstructionPart(Die, MachineCycleNames.M2, Registers.SP)
            {
                Data = new OpcodeByte(GetRegisterHighValue())
            };
        }

        private WriteT3InstructionPart CreateInstructionPartM3()
        {
            return new WriteT3InstructionPart(Die, MachineCycleNames.M3, Registers.SP)
            {
                Data = new OpcodeByte(GetRegisterLowValue())
            };
        }
        
        #endregion
    }
}
