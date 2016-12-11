﻿namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class JumpRelativeInstruction : ReadParametersInstruction
    {
        public JumpRelativeInstruction(CpuZ80 cpu) 
            : base(cpu)
        { }

        protected override CpuState GetInstructionPart(MachineCycleNames machineCycle)
        {
            switch (machineCycle)
            {
                case MachineCycleNames.M3:
                    // just chews thru the M3 cycles.
                    return new AutoCompleteInstructionPart(Cpu, machineCycle);
                default:
                    return base.GetInstructionPart(machineCycle);
            }
        }

        protected override void OnLastCycleLastM()
        {
            // perform jump
            if (IsConditionMet())
            {
                var d = (sbyte)InstructionM2.Data.Value;
                Registers.PC = Alu.Add(Registers.PC, d);
            }
        }

        protected override void OnLastCycleFirstM()
        {
            if (ExecutionEngine.Opcode.Definition.AltCycles != null &&
                !IsConditionMet())
            {
                ExecutionEngine.Cycles.SetAltCycles();
            }
        }

        protected virtual bool IsConditionMet()
        {
            switch(ExecutionEngine.Opcode.Definition.Y)
            {
                case 3: // jr d
                    return true;
                case 4: // jr nz, d
                    return !Registers.Flags.Z;
                case 5: // jr z, d
                    return Registers.Flags.Z;
                case 6: // jr nc, d
                    return !Registers.Flags.C;
                case 7: // jr c, d
                    return Registers.Flags.C;
                default:
                    throw Errors.AssignedToIllegalOpcode();
            }
        }
    }
}
