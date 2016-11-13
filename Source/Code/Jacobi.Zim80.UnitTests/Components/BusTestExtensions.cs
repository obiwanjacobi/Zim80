namespace Jacobi.Zim80.Components.UnitTests
{
    internal static class BusTestExtensions
    {
        public static BusMaster<T> CreateConnection<T>(this BusSlave<T> slave)
            where T : BusData, new()
        {
            var master = new BusMaster<T>();
            var bus = new Bus<T>();

            bus.Connect(slave);
            bus.Connect(master);

            master.IsEnabled = true;
            return master;
        }

        public static BusSlave<T> CreateConnection<T>(this BusMaster<T> master)
            where T : BusData, new()
        {
            var slave = new BusSlave<T>();
            var bus = new Bus<T>();

            bus.Connect(slave);
            bus.Connect(master);

            return slave;
        }

        public static Bus<T> CreateConnection<T>(this BusMaster<T> master, BusSlave<T> slave)
            where T : BusData, new()
        {
            if (slave == null)
                slave = new BusSlave<T>();
            var bus = new Bus<T>();

            bus.Connect(slave);
            bus.Connect(master);

            return bus;
        }

        public static Bus<T> CreateConnection<T>(this BusMasterSlave<T> ms1, BusMasterSlave<T> ms2)
            where T : BusData, new()
        {
            var bus = new Bus<T>();

            bus.Connect(ms2);
            bus.Connect(ms1);
            bus.Connect(ms2.Slave);
            bus.Connect(ms1.Slave);

            return bus;
        }
    }
}
