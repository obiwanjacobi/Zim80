using Jacobi.Zim80.Components.CpuZ80.Opcodes;

namespace Jacobi.Zim80.Components.CpuZ80.States
{
    internal class CpuInterrupt : CpuState
    {
        public CpuInterrupt(Die die, OpcodeDefinition definition) 
            : base(die)
        {
            _definition = definition;
        }

        private readonly OpcodeDefinition _definition;
        public OpcodeDefinition Definition
        {
            get { return _definition; }
        }
    }
}
