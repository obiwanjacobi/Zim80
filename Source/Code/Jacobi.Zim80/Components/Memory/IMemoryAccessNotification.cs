using System;

namespace Jacobi.Zim80.Components.Memory
{
    public interface IMemoryAccessNotification<AddressT, DataT>
        where AddressT : BusData, new()
        where DataT : BusData, new()
    {
        event EventHandler<MemoryNotificationEventArgs<AddressT, DataT>> OnMemoryRead;
        event EventHandler<MemoryNotificationEventArgs<AddressT, DataT>> OnMemoryWritten;
    }

    public class MemoryNotificationEventArgs<AddressT, DataT> : EventArgs
        where AddressT : BusData, new()
        where DataT : BusData, new()
    {
        public MemoryNotificationEventArgs(AddressT address, DataT data)
        {
            Address = address;
            Data = data;
        }

        public AddressT Address { get; private set; }
        public DataT Data { get; private set; }
    }
}
