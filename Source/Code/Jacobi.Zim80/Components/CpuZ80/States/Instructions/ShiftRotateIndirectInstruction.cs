using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using System;

namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class ShiftRotateIndirectInstruction : ReadIndirectInstruction
    {
        private ReadT3InstructionPart _readPart;
        private WriteT3InstructionPart _writePart;

        public ShiftRotateIndirectInstruction(Die die) 
            : base(die)
        { }

        protected override CpuState GetInstructionPart(MachineCycleNames machineCycle)
        {
            var isED = ExecutionEngine.Opcode.Definition.Ext1 == 0xED;

            switch (machineCycle)
            {
                case MachineCycleNames.M2:
                    return CreateReadAddressInstructionPart(machineCycle);
                case MachineCycleNames.M3:
                    if (!isED)
                        return CreateWriteAddressInstructionPart(machineCycle);
                    
                    // z80 computes
                    return new AutoCompleteInstructionPart(Die, machineCycle);
                case MachineCycleNames.M4:
                    if (!isED) throw Errors.InvalidMachineCycle(machineCycle);

                    return CreateWriteAddressInstructionPart(machineCycle);
                default:
                    throw Errors.InvalidMachineCycle(machineCycle);
            }
        }

        private ReadT3InstructionPart CreateReadAddressInstructionPart(MachineCycleNames machineCycle)
        {
            _readPart = new ReadT3InstructionPart(Die, machineCycle, GetAddress());
            return _readPart;
        }

        private WriteT3InstructionPart CreateWriteAddressInstructionPart(MachineCycleNames machineCycle)
        {
            _writePart = new WriteT3InstructionPart(Die, machineCycle, GetAddress())
            {
                Data = new OpcodeByte(DoShiftRotate(_readPart.Data.Value))
            };
            return _writePart;
        }

        private byte DoShiftRotate(byte value)
        {
            if (ExecutionEngine.Opcode.Definition.Ext1 == 0xED)
            {
                switch(ExecutionEngine.Opcode.Definition.Y)
                {
                    case 4: //RRD
                        return Die.Alu.RotateRightNibblesA(value);
                    case 5: //RLD
                        return Die.Alu.RotateLeftNibblesA(value);
                    default:
                        throw Errors.AssignedToIllegalOpcode();
                }
            }

            var shiftRotate = ExecutionEngine.Opcode.Definition.ShiftRotateFromY;

            switch (shiftRotate)
            {
                case Opcodes.ShiftRotateOperations.RotateLeftCarry:
                    return Die.Alu.RotateLeftCarry(value);
                case Opcodes.ShiftRotateOperations.RotateRightCarry:
                    return Die.Alu.RotateRightCarry(value);
                case Opcodes.ShiftRotateOperations.RotateLeft:
                    return Die.Alu.RotateLeft(value);
                case Opcodes.ShiftRotateOperations.RotateRight:
                    return Die.Alu.RotateRight(value);
                case Opcodes.ShiftRotateOperations.ShiftLeftArithmetic:
                    return Die.Alu.ShiftLeftArithmetic(value);
                case Opcodes.ShiftRotateOperations.ShiftRightArithmetic:
                    return Die.Alu.ShiftRightArithmetic(value);
                case Opcodes.ShiftRotateOperations.ShiftLeftLogical:
                    return Die.Alu.ShiftLeftLogical(value);
                case Opcodes.ShiftRotateOperations.ShiftRightLogical:
                    return Die.Alu.ShiftRightLogical(value);
            }

            throw new InvalidOperationException("Illegal value from OpcodeDefinition.Y");
        }


        /*
        RRD			X=1, Z=7, Y=4	ED-0
        RLD			X=1, Z=7, Y=5	ED-0
         */
    }
}
