namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class ExAFAFInstruction : SingleByteInstruction
    {
        public ExAFAFInstruction(Die die)
            : base(die)
        { }

        protected override void OnExecute()
        {
            // swap AF/AF'
            var af = Registers.AlternateSet.AF;
            Registers.AlternateSet.AF = 
                Registers.PrimarySet.AF;
            Registers.PrimarySet.AF = af;
        }
    }
}