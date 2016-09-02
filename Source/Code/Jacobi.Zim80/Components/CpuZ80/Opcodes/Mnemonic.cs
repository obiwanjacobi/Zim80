using System;

namespace Jacobi.Zim80.Components.CpuZ80.Opcodes
{
    internal sealed class Mnemonic
    {
        private string _mnemonic;

        public bool IsIX { get; set; }
        public bool IsIY { get; set; }

        public void SetMnemonic(string mnemonic)
        {
            if (mnemonic == null)
            {
                _mnemonic = null;
                return;
            }

            if (!IsIX && !IsIY)
            {
                _mnemonic = mnemonic;
                return;
            }

            var altText = IsIY ? "IY" : "IX";

            if (mnemonic.Contains("(") && mnemonic.Contains(")"))
            {
                // IX/IY+d
                altText += "{0}";
            }

            _mnemonic = mnemonic.Replace("HL", altText);
            Text = _mnemonic;
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
