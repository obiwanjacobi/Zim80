using System;

namespace Jacobi.Zim80.Components.CpuZ80.Opcodes
{
    internal sealed class Mnemonic
    {
        private string _mnemonic;

        public void SetMnemonic(OpcodeDefinition opcodeDefinition)
        {
            if (opcodeDefinition == null)
            {
                _mnemonic = null;
                Text = string.Empty;
                return;
            }

            _mnemonic = opcodeDefinition.Mnemonic;
            Text = _mnemonic;
            return;
        }

        public string Text { get; private set; }

        public void FormatParameter(sbyte value)
        {
            string valTxt;

            if (value < 0)
                valTxt = string.Format("{0:D}", value);
            else
                valTxt = string.Format("+{0:D}", value);

            Text = string.Format(_mnemonic, valTxt);
        }

        public void FormatParameter(byte value)
        {
            var valTxt = string.Format("{0:X2}", value);
            Text = string.Format(_mnemonic, valTxt);
        }

        public void FormatParameter(UInt16 value)
        {
            var valTxt = string.Format("{0:X4}", value);
            Text = string.Format(_mnemonic, value);
        }
    }
}
