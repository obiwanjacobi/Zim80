namespace Jacobi.Zim80.Components.CpuZ80.Opcodes
{
    public abstract class Opcode
    {
        public OpcodeDefinition Definition { get; protected set; }
        public string Text { get; internal set; }
    }
}