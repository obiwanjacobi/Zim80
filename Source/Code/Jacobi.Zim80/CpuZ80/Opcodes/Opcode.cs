namespace Jacobi.Zim80.CpuZ80.Opcodes
{
    public abstract class Opcode
    {
        private readonly Mnemonic _mnemonic = new Mnemonic();
        internal Mnemonic Mnemonic { get { return _mnemonic; } }

        private OpcodeDefinition _opcodeDefinition;
        public OpcodeDefinition Definition
        {
            get { return _opcodeDefinition; }
            set
            {
                _opcodeDefinition = value;
                UpdateMnemonic();
            }
        }

        /// <summary>
        /// Formatted mnemonic text
        /// </summary>
        public string Text { get { return _mnemonic.Text; } }

        public ushort Address { get; set; }

        private void UpdateMnemonic()
        {
            if (_opcodeDefinition == null)
                _mnemonic.SetMnemonic(null);
            else
                _mnemonic.SetMnemonic(_opcodeDefinition);
        }
    }
}