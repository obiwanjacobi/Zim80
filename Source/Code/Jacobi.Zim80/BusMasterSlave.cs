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
            Slave.ConnectTo(bus);
        }

        private class BusSlaveOfMaster : BusSlave
        {
            private BusMaster _owner;

            public BusSlaveOfMaster(BusMaster owner)
            {
                _owner = owner;
            }

            public BusSlaveOfMaster(BusMaster owner, string name)
                : base(name)
            {
                _owner = owner;
            }

            public BusSlaveOfMaster(BusMaster owner, Bus bus)
                : base(bus)
            {
                _owner = owner;
            }

            public BusSlaveOfMaster(BusMaster owner, Bus bus, string name)
                : base(bus, name)
            {
                _owner = owner;
            }

            protected override void OnValueChanged(BusChangedEventArgs<BusData> e)
            {
                if (e.BusMaster != _owner)
                    base.OnValueChanged(e);
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
            Slave.ConnectTo(bus);
        }

        private class BusSlaveOfMaster : BusSlave<T>
        {
            private BusMaster<T> _owner;

            public BusSlaveOfMaster(BusMaster<T> owner)
            {
                _owner = owner;
            }

            public BusSlaveOfMaster(BusMaster<T> owner, string name)
                : base(name)
            {
                _owner = owner;
            }

            public BusSlaveOfMaster(BusMaster<T> owner, Bus<T> bus, string name)
                : base(bus, name)
            {
                _owner = owner;
            }

            protected override void OnValueChanged(BusChangedEventArgs<BusData> e)
            {
                if (e.BusMaster != _owner)
                    base.OnValueChanged(e);
            }
        }
    }
}