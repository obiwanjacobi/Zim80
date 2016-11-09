using Jacobi.Zim80.Components.CpuZ80.Opcodes;

namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class LoadImmediateIndirect16Instruction : ReadParametersInstruction
    {
        private ReadT3InstructionPart _readInstructionLo;
        private ReadT3InstructionPart _readInstructionHi;

        public LoadImmediateIndirect16Instruction(Die die) 
            : base(die)
        { }

        private bool IsWrite
        {
            get { return ExecutionEngine.Opcode.Definition.Q == 0; } 
        }

        private Register16Table AffectedRegisters
        {
            get { return ExecutionEngine.Opcode.Definition.Register16FromP; }
        }

        protected override CpuState GetInstructionPart(MachineCycleNames machineCycle)
        {
            switch (machineCycle)
            {
                case MachineCycleNames.M2:
                case MachineCycleNames.M3:
                    return base.GetInstructionPart(machineCycle);
                case MachineCycleNames.M4:
                    return CreateInstructionPartM4();
                case MachineCycleNames.M5:
                    return CreateInstructionPartM5();
            }

            throw Errors.InvalidMachineCycle(machineCycle);
        }

        protected override void OnLastCycleLastM()
        {
            base.OnLastCycleLastM();

            if (!IsWrite)
            {
                var reg = AffectedRegisters;
                var val16 = OpcodeByte.MakeUInt16(_readInstructionLo.Data, _readInstructionHi.Data);
                if (reg == Register16Table.SP)
                    Registers.SP = val16;
                else
                    Registers.PrimarySet[AffectedRegisters] = val16;
            }
        }

        private byte GetValueLo()
        {
            var reg = AffectedRegisters;
            if (reg == Register16Table.SP)
                return (byte)(Registers.SP & 0xFF);

            return (byte)(Registers.PrimarySet[AffectedRegisters] & 0xFF);
        }

        private byte GetValueHi()
        {
            var reg = AffectedRegisters;
            if (reg == Register16Table.SP)
                return (byte)((Registers.SP & 0xFF00) >> 8);

            return (byte)((Registers.PrimarySet[AffectedRegisters] & 0xFF00) >> 8);
        }

        private ushort GetAddress()
        {
            return OpcodeByte.MakeUInt16(InstructionM2.Data, InstructionM3.Data);
        }

        private CpuState CreateInstructionPartM5()
        {
            ushort address = (ushort)(GetAddress() + 1);

            if (IsWrite)
            {
                return new WriteT3InstructionPart(Die, MachineCycleNames.M5, address)
                {
                    Data = new OpcodeByte(GetValueHi())
                };
            }

            _readInstructionHi = new ReadT3InstructionPart(Die, MachineCycleNames.M5, address);
            return _readInstructionHi;
        }

        private CpuState CreateInstructionPartM4()
        {
            ushort address = GetAddress();

            if (IsWrite)
            {
                return new WriteT3InstructionPart(Die, MachineCycleNames.M4, address)
                {
                    Data = new OpcodeByte(GetValueLo())
                };
            }

            _readInstructionLo = new ReadT3InstructionPart(Die, MachineCycleNames.M4, address);
            return _readInstructionLo;
        }
    }
}
