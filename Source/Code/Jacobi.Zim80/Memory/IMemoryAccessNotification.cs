using System;

namespace Jacobi.Zim80.Memory
{
    public interface IMemoryAccessNotification<AddressT, DataT>
        where AddressT : BusData, new()
        where DataT : BusData, new()
    {
        event EventHandler<MemoryNotificationEventArgs<AddressT, DataT>> OnMemoryRead;
        event EventHandler<MemoryNotificationEventArgs<AddressT, DataT>> OnMemoryWritten;
    }
}
