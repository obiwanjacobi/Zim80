using System;
using Jacobi.Zim80.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jacobi.Zim80.CpuZ80.UnitTests
{
    internal class MemoryLogger<AddressT, DataT>
        where AddressT : BusData, new()
        where DataT : BusData, new()
    {
        private readonly TestContext _testContext;
        private readonly string _instanceName;

        public MemoryLogger(string instanceName = null)
        {
            _instanceName = instanceName;
        }

        public MemoryLogger(TestContext testContext, string instanceName = null)
        {
            _testContext = testContext;
            _instanceName = instanceName;
        }

        public void Attach(IMemoryAccessNotification<AddressT, DataT> memoryEvents)
        {
            memoryEvents.OnMemoryRead += MemoryEvents_OnMemoryRead;
            memoryEvents.OnMemoryWritten += MemoryEvents_OnMemoryWritten;
        }

        private void MemoryEvents_OnMemoryWritten(object sender, MemoryNotificationEventArgs<AddressT, DataT> e)
        {
            var txt = string.Format(" {0:X4} <= {1:X2}", e.Address.ToUInt16(), e.Data.ToByte());
            Log(txt);
        }

        private void MemoryEvents_OnMemoryRead(object sender, MemoryNotificationEventArgs<AddressT, DataT> e)
        {
            var txt = string.Format(" {0:X4} => {1:X2}.", e.Address.ToUInt16(), e.Data.ToByte());
            Log(txt);
        }

        private void Log(string txt)
        {
            string prefix;

            if (_instanceName != null)
            {
                prefix = _instanceName;
            }
            else
            {
                prefix = "MEM";
            }

            if (_testContext != null)
            {
                _testContext.WriteLine(prefix + txt);
            }
            else
            {
                Console.WriteLine(prefix + txt);
            }
        }
    }
}
