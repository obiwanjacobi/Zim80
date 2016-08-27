namespace Jacobi.Zim80.Components.CpuZ80.Opcodes
{
    public abstract class Opcode
    {
        public OpcodeDefinition Definition { get; protected set; }
        public OpcodeByte Ext1 { get; internal set; }
        public OpcodeByte Ext2 { get; internal set; }
        public string Text { get; internal set; }

        public bool IsIX { get { return Ext1 != null && Ext1.IsDD; } }
        public bool IsIY { get { return Ext1 != null && Ext1.IsFD; } }
    }
}