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
            if (opcodeByte == null)
                throw new ArgumentNullException("opcodeByte");

            return Process(opcodeByte);
        }

        private bool Process(OpcodeByte opcodeByte)
        {
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
                        throw new NotSupportedException("Illegal opcode:" + opcodeByte.ToString());

                    if (!_opcodeDef.HasParameters)
                    {
                        Opcode = new SingleByteOpcode(_opcodeDef);
                        IsDone = true;
                    }
                    else
                        Opcode = new MultiByteOpcode(_opcodeDef);
                }
            }
            else // parameters
            {
                var mbo = Opcode as MultiByteOpcode;

                if (mbo == null)
                    throw new InvalidOperationException("OpcodeBuilder already produced an Opcode -or- " +
                        "you are adding parameters to a SingleCycleOpcode. Did you forget to call Clear?");

                int paramCount = _opcodeDef.ParameterCount;
                
                if (mbo.ParameterCount >= paramCount)
                    throw new InvalidOperationException("Too many parameters.");

                mbo.AddParameter(opcodeByte);
                IsDone = mbo.ParameterCount == paramCount;
            }

            return Opcode != null;
        }

        // implements how additional extensions will replace others
        private void SetExtension(OpcodeByte opcodeByte)
        {
            _ext2 = null;
            
            if (_ext1 == null)
            {
                _ext1 = opcodeByte;
            }
            else if (opcodeByte.IsDD || opcodeByte.IsED || opcodeByte.IsFD)
            {
                // last one wins
                _ext1 = opcodeByte;
            }
            else if (opcodeByte.IsCB)
            {
                if (_ext1.IsED)
                {
                    // CB overwrites ED
                    _ext1 = opcodeByte;
                }
                else
                {
                    // but not DD/FD
                    _ext2 = opcodeByte;
                }
            }
            else
            {
                throw new InvalidOperationException("Opcode Extension Error!");
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
