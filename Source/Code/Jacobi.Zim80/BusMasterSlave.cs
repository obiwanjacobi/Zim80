using System;
using System.Diagnostics;

namespace Jacobi.Zim80
{
    // bidirectional bus connection
    [DebuggerDisplay("{Name}: {Value}")]
    public class BusMasterSlave : BusMaster
    {
        private BusSlaveOfMaster _slave;

        public BusMasterSlave()
        {
            _slave = new BusSlaveOfMaster(this);
        }

        public BusMasterSlave(string name)
            : base(name)
        {
            _slave = new BusSlaveOfMaster(this, name + "-Slave");
        }

        public BusMasterSlave(Bus bus)
            : base(bus)
        {
            _slave = new BusSlaveOfMaster(this, bus);
        }

        public BusMasterSlave(Bus bus, string name)
            : base(bus, name)
        {
            _slave = new BusSlaveOfMaster(this, bus, name + "-Slave");
        }

        public BusSlave Slave
        {
            get { return _slave; }
        }

        public override void ConnectTo(Bus bus)
        {
            base.ConnectTo(bus);
            _slave.ConnectToInternal(bus);
        }

        private void ConnectToInternal(Bus bus)
        {
            base.ConnectTo(bus);
        }

        private class BusSlaveOfMaster : BusSlave
        {
            private BusMasterSlave _owner;

            public BusSlaveOfMaster(BusMasterSlave owner)
            {
                _owner = owner;
            }

            public BusSlaveOfMaster(BusMasterSlave owner, string name)
                : base(name)
            {
                _owner = owner;
            }

            public BusSlaveOfMaster(BusMasterSlave owner, Bus bus)
                : base(bus)
            {
                _owner = owner;
            }

            public BusSlaveOfMaster(BusMasterSlave owner, Bus bus, string name)
                : base(bus, name)
            {
                _owner = owner;
            }

            protected override void OnValueChanged(BusChangedEventArgs<BusData> e)
            {
                if (e.BusMaster != _owner)
                    base.OnValueChanged(e);
            }

            public override void ConnectTo(Bus bus)
            {
                base.ConnectTo(bus);
                _owner.ConnectToInternal(bus);
            }

            internal new void ConnectToInternal(Bus bus)
            {
                base.ConnectToInternal(bus);
            }
        }
    }


    // bidirectional bus connection
    [DebuggerDisplay("{Name}: {Value}")]
    public class BusMasterSlave<T> : BusMaster<T>
        where T : BusData, new()
    {
        private BusSlaveOfMaster _slave;

        public BusMasterSlave()
        {
            _slave = new BusSlaveOfMaster(this);
        }

        public BusMasterSlave(string name)
            : base(name)
        {
            _slave = new BusSlaveOfMaster(this, name + "-Slave");
        }

        public BusMasterSlave(Bus<T> bus, string name)
            : base(bus, name)
        {
            _slave = new BusSlaveOfMaster(this, bus, name + "-Slave");
        }

        public BusSlave<T> Slave
        {
            get { return _slave; }
        }

        public override void ConnectTo(Bus<T> bus)
        {
            base.ConnectTo(bus);
            _slave.ConnectToInternal(bus);
        }

        private void ConnectToInternal(Bus<T> bus)
        {
            base.ConnectTo(bus);
        }

        private class BusSlaveOfMaster : BusSlave<T>
        {
            private BusMasterSlave<T> _owner;

            public BusSlaveOfMaster(BusMasterSlave<T> owner)
            {
                _owner = owner;
            }

            public BusSlaveOfMaster(BusMasterSlave<T> owner, string name)
                : base(name)
            {
                _owner = owner;
            }

            public BusSlaveOfMaster(BusMasterSlave<T> owner, Bus<T> bus, string name)
                : base(bus, name)
            {
                _owner = owner;
            }

            protected override void OnValueChanged(BusChangedEventArgs<BusData> e)
            {
                if (e.BusMaster != _owner)
                    base.OnValueChanged(e);
            }

            public override void ConnectTo(Bus<T> bus)
            {
                base.ConnectTo(bus);
                _owner.ConnectToInternal(bus);
            }

            internal void ConnectToInternal(Bus<T> bus)
            {
                base.ConnectToInternal(bus);
            }
        }
    }
}