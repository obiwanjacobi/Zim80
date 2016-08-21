namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class ExAFAFInstruction : SingleCycleInstruction
    {
        public ExAFAFInstruction(Die die)
            : base(die)
        { }

        protected override void OnLastCycleFirstM()
        {
            // swap AF/AF'
            var af = Registers.AlternateSet.AF;
            Registers.AlternateSet.AF = 
                Registers.PrimarySet.AF;
            Registers.PrimarySet.AF = af;
        }
    }
}