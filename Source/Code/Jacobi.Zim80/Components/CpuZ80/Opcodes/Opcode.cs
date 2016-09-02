namespace Jacobi.Zim80.Components.CpuZ80.Opcodes
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

        private OpcodeByte _ext1;
        public OpcodeByte Ext1
        {
            get { return _ext1; }
            set
            {
                _ext1 = value;
                _mnemonic.IsIX = IsIX;
                _mnemonic.IsIY = IsIY;
            }
        }

        public OpcodeByte Ext2 { get; internal set; }

        /// <summary>
        /// Formatted mnemonic text
        /// </summary>
        public string Text { get { return _mnemonic.Text; } }

        public bool IsIX { get { return Ext1 != null && Ext1.IsDD; } }
        public bool IsIY { get { return Ext1 != null && Ext1.IsFD; } }

        private void UpdateMnemonic()
        {
            if (_opcodeDefinition == null)
                _mnemonic.SetMnemonic(null);
            else
                _mnemonic.SetMnemonic(_opcodeDefinition.Mnemonic);
        }
    }
}