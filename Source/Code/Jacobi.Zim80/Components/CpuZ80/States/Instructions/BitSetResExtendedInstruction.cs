namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal class BitSetResExtendedInstruction : ExtendedInstruction
    {
        public BitSetResExtendedInstruction(Die die) 
            : base(die)
        { }

        protected override void OnLastCycleLastM()
        {
            base.OnLastCycleLastM();

            if (ExecutionEngine.Opcode.Definition.X == 1)   //bit
            {
                var bit = ExecutionEngine.Opcode.Definition.Y;
                Die.Alu.TestBit(bit, GetValue());
            }
        }

        protected override byte ExecuteOperation(byte value)
        {
            var bit = ExecutionEngine.Opcode.Definition.Y;

            switch (ExecutionEngine.Opcode.Definition.X)
            {
                case 2: //res
                    return Die.Alu.ResetBit(bit, value);
                case 3: //set
                    return Die.Alu.SetBit(bit, value);
                default:
                    throw Errors.AssignedToIllegalOpcode();
            }
        }
    }
}
