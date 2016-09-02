using System;
using System.Collections.Generic;

namespace Jacobi.Zim80.Components.CpuZ80.Opcodes
{
    internal class MultiByteOpcode : Opcode
    {
        private readonly List<OpcodeByte> _parameters = new List<OpcodeByte>();
        
        public MultiByteOpcode(OpcodeDefinition opcodeDef)
        {
            Definition = opcodeDef;
        }

        public void AddParameter(OpcodeByte parameter)
        {
            _parameters.Add(parameter);
            FormatText();
        }

        public int ParameterCount
        {
            get { return _parameters.Count; }
        }

        public OpcodeByte GetParameter(int index)
        {
            return _parameters[index];
        }

        private void FormatText()
        {
            if (_parameters.Count > 1)
                Mnemonic.FormatParameter(CreateValue16(_parameters));
            else if (Definition.d)  // signed
                Mnemonic.FormatParameter((sbyte)_parameters[0].Value);
            else
                Mnemonic.FormatParameter(_parameters[0].Value);
        }

        private static UInt16 CreateValue16(IList<OpcodeByte> parameters)
        {
            UInt16 value = parameters[0].Value;
            value |= (UInt16)(parameters[1].Value << 8);
            return value;
        }
    }
}
