using System;

namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class IntInstruction : Instruction
    {
        public IntInstruction(Die die) 
            : base(die)
        { }

        protected override void OnLastCycleFirstM()
        {
            throw new NotImplementedException();
        }
    }
}
