namespace Jacobi.Zim80.UnitTests
{
    internal static class BusTestExtensions
    {
        public static BusMaster<T> CreateConnection<T>(this BusSlave<T> slave, string busName = null)
            where T : BusData, new()
        {
            var master = new BusMaster<T>();
            var bus = new Bus<T>(busName);

            slave.ConnectTo(bus);
            master.ConnectTo(bus);

            return master;
        }

        public static BusSlave<T> CreateConnection<T>(this BusMaster<T> master, string busName = null)
            where T : BusData, new()
        {
            var slave = new BusSlave<T>();
            var bus = new Bus<T>(busName);

            slave.ConnectTo(bus);
            master.ConnectTo(bus);

            return slave;
        }

        public static Bus<T> CreateConnection<T>(this BusMaster<T> master, BusSlave<T> slave, string busName = null)
            where T : BusData, new()
        {
            if (slave == null)
                slave = new BusSlave<T>();
            var bus = new Bus<T>(busName);

            slave.ConnectTo(bus);
            master.ConnectTo(bus);

            return bus;
        }

        public static Bus<T> CreateConnection<T>(this BusMasterSlave<T> ms1, BusMasterSlave<T> ms2, string busName = null)
            where T : BusData, new()
        {
            var bus = new Bus<T>(busName);

            ms1.ConnectTo(bus);
            ms2.ConnectTo(bus);

            return bus;
        }
    }
}
