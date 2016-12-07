using System;

namespace Jacobi.Zim80.Test
{
    public static class BusExtensions
    {
        public static Bus<T> ConnectTo<T>(this BusMaster<T> master, BusSlave<T> slave, string busName = null)
            where T : BusData, new()
        {
            Bus<T> bus = Connect(master, slave, busName);

            if (bus == null)
            {
                if (busName == null) busName = master.Name;
                bus = master.GetOrAddBus(busName);
                slave.ConnectTo(bus);
            }

            return bus;
        }

        public static Bus<T> ConnectTo<T>(this BusSlave<T> slave, BusMaster<T> master, string busName = null)
            where T : BusData, new()
        {
            Bus<T> bus = Connect(master, slave, busName);

            if (bus == null)
            {
                if (busName == null) busName = slave.Name;
                bus = slave.GetOrAddBus(busName);
                master.ConnectTo(bus);
            }

            return bus;
        }

        private static Bus<T> Connect<T>(BusMaster<T> master, BusSlave<T> slave, string busName = null)
            where T : BusData, new()
        {
            if (master.IsConnected && slave.IsConnected)
            {
                if (master.Bus != slave.Bus)
                    throw new InvalidOperationException("Both Master and Slave are already connected to different buses.");

                return master.Bus;
            }

            Bus<T> bus = null;

            if (master.IsConnected)
            {
                bus = master.Bus;
                slave.ConnectTo(bus);
            }
            else if (slave.IsConnected)
            {
                bus = slave.Bus;
                master.ConnectTo(bus);
            }

            return bus;
        }

        public static BusMaster<T> CreateConnection<T>(this BusSlave<T> slave, string busName = null)
            where T : BusData, new()
        {
            var bus = slave.GetOrAddBus(busName);
            return new BusMaster<T>(bus, busName);
        }

        public static BusSlave<T> CreateConnection<T>(this BusMaster<T> master, string busName = null)
            where T : BusData, new()
        {
            var bus = master.GetOrAddBus(busName);
            return new BusSlave<T>(bus, busName);
        }

        public static BusMaster CreateConnection(this BusSlave slave, string busName = null)
        {
            var bus = slave.GetOrAddBus(busName);
            return new BusMaster(bus, busName);
        }

        public static BusSlave CreateConnection(this BusMaster master, string busName = null)
        {
            var bus = master.GetOrAddBus(busName);
            return new BusSlave(bus, busName);
        }

        public static Bus<T> GetOrAddBus<T>(this BusMaster<T> master, string busName = null)
            where T : BusData, new()
        {
            Bus<T> bus;
            if (!master.IsConnected)
            {
                bus = new Bus<T>(busName);
                master.ConnectTo(bus);
            }
            else
            {
                bus = master.Bus;
            }

            return bus;
        }

        public static Bus<T> GetOrAddBus<T>(this BusSlave<T> slave, string busName = null)
            where T : BusData, new()
        {
            Bus<T> bus;
            if (!slave.IsConnected)
            {
                bus = new Bus<T>(busName);
                slave.ConnectTo(bus);
            }
            else
            {
                bus = slave.Bus;
            }

            return bus;
        }

        public static Bus GetOrAddBus(this BusMaster master, string busName = null)
        {
            Bus bus;
            if (!master.IsConnected)
            {
                bus = new Bus(master.Value.Width, busName);
                master.ConnectTo(bus);
            }
            else
            {
                bus = master.Bus;
            }

            return bus;
        }

        public static Bus GetOrAddBus(this BusSlave slave, string busName = null)
        {
            Bus bus;
            if (!slave.IsConnected)
            {
                bus = new Bus(slave.Value.Width, busName);
                slave.ConnectTo(bus);
            }
            else
            {
                bus = slave.Bus;
            }

            return bus;
        }
    }
}
