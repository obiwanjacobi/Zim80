namespace Jacobi.Zim80.Components
{
    // bidirectional bus connection
    public class BusMasterSlave<T> : BusMaster<T> 
        where T : BusData, new()
    {
        private BusSlave<T> _slave;

        public BusMasterSlave()
        {
            _slave = new BusSlave<T>();
        }

        public BusSlave<T> Slave
        {
            get { return _slave; }
        }
    }
}
