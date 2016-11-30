namespace Jacobi.Zim80.Logic
{
    public abstract class Gate : MultipleInputGate, INamedObject
    {
        private readonly DigitalSignalProvider _output;

        protected Gate()
        {
            _output = new DigitalSignalProvider();
        }

        public string Name { get; set; }

        public DigitalSignalProvider Output { get { return _output; } }
    }
}
