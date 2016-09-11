using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using Jacobi.Zim80.Components.CpuZ80.States.Instructions;
using System;

namespace Jacobi.Zim80.Components.CpuZ80.States
{
    internal class CpuInterrupt : CpuState
    {
        private readonly Interrupt _interrupt;

        public CpuInterrupt(Die die, OpcodeDefinition definition) 
            : base(die)
        {
            if (definition == null)
                throw new ArgumentNullException("definition");

            _definition = definition;

            if (_definition.Instruction == null)
                throw new InvalidOperationException("The Interrupt OpcodeDefinition has no associated Instruction: "
                    + ExecutionEngine.Cycles.OpcodeDefinition.ToString());

            _interrupt = (Interrupt)Activator.CreateInstance(
                _definition.Instruction, Die);
        }

        private readonly OpcodeDefinition _definition;
        public OpcodeDefinition Definition
        {
            get { return _definition; }
        }

        public override void OnClock(DigitalLevel level)
        {
            base.OnClock(level);
            _interrupt.OnClock(level);
        }
    }
}
