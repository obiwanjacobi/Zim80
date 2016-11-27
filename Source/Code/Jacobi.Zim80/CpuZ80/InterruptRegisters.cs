namespace Jacobi.Zim80.CpuZ80
{
    public class InterruptRegisters
    {
        public InterruptRegisters()
        {
            _interruptMode = InterruptModes.InterruptMode0;
        }

        private InterruptModes _interruptMode;
        public InterruptModes InterruptMode
        {
            get { return _interruptMode; }
            set { _interruptMode = value; }
        }

        public bool IsEnabled
        {
            get { return IFF1 && !IsSuspended; }
        }

        // interrupt mode flags
        public bool IFF1 { get; set; }
        public bool IFF2 { get; set; }
        public bool IsSuspended { get; internal set; }
    }
}
