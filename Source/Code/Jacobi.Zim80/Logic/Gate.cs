namespace Jacobi.Zim80.Logic
{
    public abstract class Gate : MultipleInputGate
    {
        private readonly DigitalSignalProvider _output;

        protected Gate()
        {
            _output = new DigitalSignalProvider();
        }

        public DigitalSignalProvider Output { get { return _output; } }
    }
}
