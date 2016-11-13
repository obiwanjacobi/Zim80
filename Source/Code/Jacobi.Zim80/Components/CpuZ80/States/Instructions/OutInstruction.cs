using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using System;

namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class OutInstruction : MultiCycleInstruction
    {
        private ReadT3InstructionPart _readIndirectPart;
        private OutputInstructionPart _outputPart;

        public OutInstruction(Die die) 
            : base(die)
        { }

        protected override CpuState GetInstructionPart(MachineCycleNames machineCycle)
        {
            switch (machineCycle)
            {
                case MachineCycleNames.M2:
                    if (IsIndirect)
                        return CreateReadIndirectPart(machineCycle);
                    return CreateOutputPart(machineCycle);
                case MachineCycleNames.M3:
                    if (!IsIndirect) throw Errors.InvalidMachineCycle(machineCycle);
                    return CreateOutputPart(machineCycle);
                default:
                    throw Errors.InvalidMachineCycle(machineCycle);
            }
        }

        private CpuState CreateReadIndirectPart(MachineCycleNames machineCycle)
        {
            _readIndirectPart = new Instructions.ReadT3InstructionPart(Die, machineCycle);
            return _readIndirectPart;
        }

        private CpuState CreateOutputPart(MachineCycleNames machineCycle)
        {
            _outputPart = new OutputInstructionPart(Die, machineCycle, GetAddress())
            {
                Data = new OpcodeByte(GetValue())
            };
            return _outputPart;
        }

        private byte GetValue()
        {
            if (IsIndirect)
                return Registers.A;

            var reg = ExecutionEngine.Opcode.Definition.Register8FromY;
            if (reg == Register8Table.HL) return 0;
            return Registers[reg];
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

        private class OutputInstructionPart : AutoCompleteInstructionPart
        {
            private readonly UInt16 _address;

            public OutputInstructionPart(Die die,
                MachineCycleNames activeMachineCycle, UInt16 address)
                : base(die, activeMachineCycle)
            {
                _address = address;
            }

            public OpcodeByte Data { get; set; }

            protected override void OnClockPos()
            {
                base.OnClockPos();

                switch (ExecutionEngine.Cycles.CycleName)
                {
                    case CycleNames.T1:
                        ExecutionEngine.SetAddressBus(_address);
                        break;
                    case CycleNames.T2:
                        Die.IoRequest.Write(DigitalLevel.Low);
                        Die.Write.Write(DigitalLevel.Low);
                        break;
                }
            }

            protected override void OnClockNeg()
            {
                base.OnClockNeg();

                switch (ExecutionEngine.Cycles.CycleName)
                {
                    case CycleNames.T1:
                        Write();
                        break;
                    case CycleNames.T4:
                        Die.Write.Write(DigitalLevel.High);
                        Die.IoRequest.Write(DigitalLevel.High);
                        Die.DataBus.IsEnabled = false;
                        break;
                }
            }

            private void Write()
            {
                Die.DataBus.IsEnabled = true;
                Die.DataBus.Write(new BusData8(Data.Value));
            }
        }
    }
}
