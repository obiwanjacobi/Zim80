using System;

namespace Jacobi.Zim80.CpuZ80.States.Instructions
{
    internal class ExchangeInstruction : SingleCycleInstruction
    {
        public ExchangeInstruction(CpuZ80 cpu)
            : base(cpu)
        { }

        protected override void OnLastCycleFirstM()
        {
            var x = ExecutionEngine.Opcode.Definition.X;
            var z = ExecutionEngine.Opcode.Definition.Z;

            if (x == 0 && z == 0)
                ExAfAf();
            else if (x == 3 && z == 1)
                Exx();
            else if (x == 3 && z == 3)
                ExHLDE();
            else
                throw Errors.AssignedToIllegalOpcode();
        }

        private void ExHLDE()
        {
            var hl = Registers.HL;
            Registers.HL = Registers.DE;
            Registers.DE = hl;
        }

        private void Exx()
        {
            var bc = Registers.BC;
            var de = Registers.DE;
            var hl = Registers.HL;

            Registers.BC = Registers.AlternateSet.BC;
            Registers.DE = Registers.AlternateSet.DE;
            Registers.HL = Registers.AlternateSet.HL;

            Registers.AlternateSet.BC = bc;
            Registers.AlternateSet.DE = de;
            Registers.AlternateSet.HL = hl;
        }

        private void ExAfAf()
        {
            var af = Registers.AlternateSet.AF;
            Registers.AlternateSet.AF = Registers.AF;
            Registers.AF = af;
        }
    }
}