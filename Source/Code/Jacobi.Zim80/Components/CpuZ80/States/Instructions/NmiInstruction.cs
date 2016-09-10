using System;

namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class NmiInstruction : Instruction
    {
        public NmiInstruction(Die die) 
            : base(die)
        { }

        protected override void OnLastCycleFirstM()
        {
            throw new NotImplementedException();
        }
    }
}
