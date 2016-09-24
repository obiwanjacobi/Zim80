namespace Jacobi.Zim80.Components.CpuZ80
{
    public enum InterruptTypes
    {
        /// <summary>Reset</summary>
        Rst,

        /// <summary>Bus Request</summary>
        Brq,

        /// <summary>Non-Maskable Interrupt</summary>
        Nmi,

        /// <summary>Maskable Interrupt</summary>
        Int,
    }

    public enum InterruptModes
    {
        InterruptMode0,
        InterruptMode1,
        InterruptMode2,
    }
}
