namespace Jacobi.Zim80.Memory
{
    public interface IDirectMemoryAccess<DataT>
        where DataT : BusData, new()
    {
        DataT this[int address] { get; set; }
    }
}
