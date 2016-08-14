using System;

namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class NopInstruction : SingleByteInstruction
    {
        public NopInstruction(Die die)
            : base(die)
        { }

        protected override void OnExecute()
        {
            // no operation
        }
    }
}
