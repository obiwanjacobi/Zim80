using System;

namespace Jacobi.Zim80.CpuZ80.Opcodes
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

        public void FormatParameters(sbyte offset, byte param)
        {
            var offsetTxt = FormatOffest(offset);
            var paramTxt = FormatByte(param);

            Text = string.Format(_mnemonic, offsetTxt, paramTxt);
        }

        public void FormatParameter(sbyte value)
        {
            var valTxt = FormatOffest(value);
            Text = string.Format(_mnemonic, valTxt);
        }

        public void FormatParameter(byte value)
        {
            var valTxt = FormatByte(value);
            Text = string.Format(_mnemonic, valTxt);
        }

        public void FormatParameter(UInt16 value)
        {
            var valTxt = string.Format("{0:X4}", value);
            Text = string.Format(_mnemonic, value);
        }

        private static string FormatOffest(sbyte value)
        {
            if (value < 0)
                return string.Format("{0:D}", value);

            return string.Format("+{0:D}", value);
        }

        private static string FormatByte(byte value)
        {
            return string.Format("{0:X2}", value);
        }
    }
}
