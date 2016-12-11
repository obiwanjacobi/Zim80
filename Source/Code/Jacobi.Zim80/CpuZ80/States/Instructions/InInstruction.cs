﻿using Jacobi.Zim80.CpuZ80.Opcodes;

namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class InInstruction : MultiCycleInstruction
    {
        private ReadT3InstructionPart _readIndirectPart;
        private InputInstructionPart _inputPart;

        public InInstruction(CpuZ80 cpu) 
            : base(cpu)
        { }

        protected override void OnLastCycleLastM()
        {
            base.OnLastCycleLastM();

            var value = _inputPart.Data.Value;
            Registers.Flags.S = Alu.IsNegative(value);
            Registers.Flags.Z = Alu.IsZero(value);
            Registers.Flags.H = false;
            Registers.Flags.PV = Alu.IsParityEven(value);
            Registers.Flags.N = false;

            if (IsIndirect)
            {
                Registers.A = value;
            }
            else
            {
                var reg = ExecutionEngine.Opcode.Definition.Register8FromY;
                if (reg != Register8Table.HL)
                {
                    Registers[reg] = value;
                }
            }
        }

        protected override CpuState GetInstructionPart(MachineCycleNames machineCycle)
        {
            switch (machineCycle)
            {
                case MachineCycleNames.M2:
                    if (IsIndirect)
                        return CreateReadIndirectPart(machineCycle);
                    return CreateInputPart(machineCycle);
                case MachineCycleNames.M3:
                    if (!IsIndirect) throw Errors.InvalidMachineCycle(machineCycle);
                    return CreateInputPart(machineCycle);
                default:
                    throw Errors.InvalidMachineCycle(machineCycle);
            }
        }

        private CpuState CreateReadIndirectPart(MachineCycleNames machineCycle)
        {
            _readIndirectPart = new Instructions.ReadT3InstructionPart(Cpu, machineCycle);
            return _readIndirectPart;
        }

        private CpuState CreateInputPart(MachineCycleNames machineCycle)
        {
            _inputPart = new InputInstructionPart(Cpu, machineCycle, GetAddress());
            return _inputPart;
        }

        private ushort GetAddress()
        {
            if (IsIndirect)
            {
                return OpcodeByte.MakeUInt16(
                    _readIndirectPart.Data, 
                    new OpcodeByte(Registers.A));
            }

            return Registers.BC;
        }

        private bool IsIndirect
        {
            // non ED instruction is indirect
            get { return ExecutionEngine.Opcode.Definition.Ext1 == 0; }
        }
    }
}
