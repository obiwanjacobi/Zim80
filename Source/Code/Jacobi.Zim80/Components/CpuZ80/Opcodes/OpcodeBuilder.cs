using System;

namespace Jacobi.Zim80.Components.CpuZ80.Opcodes
{
    internal class OpcodeBuilder
    {
        private OpcodeByte _ext1;
        private OpcodeByte _ext2;
        private OpcodeDefinition _opcodeDef;

        public bool Add(byte opcodeByte)
        {
            return Add(new OpcodeByte(opcodeByte));
        }

        public bool Add(OpcodeByte opcodeByte)
        {
            return Process(opcodeByte);
        }

        private bool Process(OpcodeByte opcodeByte)
        {
            if (opcodeByte == null)
                throw new ArgumentNullException("opcodeByte");

            if (_opcodeDef == null)
            {
                if (opcodeByte.IsExtension)
                {
                    SetExtension(opcodeByte);
                }
                else
                {
                    _opcodeDef = OpcodeDefinition.Find(opcodeByte, _ext1, _ext2);

                    if (_opcodeDef == null)
                        throw new NotSupportedException("Illegal opcode:" + opcodeByte.Value);

                    if (!_opcodeDef.HasParameters)
                    {
                        Opcode = new SingleByteOpcode(_opcodeDef);
                        IsDone = true;
                    }
                    else
                        Opcode = new MultiByteOpcode(_opcodeDef);

                    Opcode.Ext1 = _ext1;
                    Opcode.Ext2 = _ext2;
                }
            }
            else // parameters
            {
                var mbo = Opcode as MultiByteOpcode;

                if (mbo == null)
                    throw new InvalidOperationException("OpcodeBuilder already produced an Opcode -or- " +
                        "you are adding parameters to a SingleCycleOpcode. Did you forget to call Clear?");

                int paramCount = 1;
                if (_opcodeDef.nn)
                    paramCount++;

                if (mbo.ParameterCount >= paramCount)
                    throw new InvalidOperationException("Too many parameters.");

                mbo.AddParameter(opcodeByte);
                IsDone = mbo.ParameterCount == paramCount;
            }

            return Opcode != null;
        }

        private void SetExtension(OpcodeByte opcodeByte)
        {
            if (_ext1 == null)
            {
                _ext1 = opcodeByte;
            }
            else
            {
                // TODO: additional extension added will replace others
                _ext2 = opcodeByte;
            }
        }

        public void Clear()
        {
            _opcodeDef = null;
            _ext1 = null;
            _ext2 = null;
            Opcode = null;
            IsDone = false;
        }

        public Opcode Opcode { get; private set; }

        public bool IsDone { get; private set; }

        public bool HasShiftExtension
        {
            get { return _ext1 != null && _ext1.IsDD || _ext1.IsFD; }
        }
    }
}
