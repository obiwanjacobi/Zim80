using System;

namespace Jacobi.Zim80
{
    /// <summary>
    /// Consumes a DigitalSignal. 
    /// Multiple instances can consume the same DigitalSignal instance.
    /// </summary>
    public class DigitalSignalConsumer : INamedObject
    {
        public DigitalSignalConsumer()
        { }

        public DigitalSignalConsumer(string name)
        {
            Name = name;
        }

        public DigitalSignalConsumer(DigitalSignal digitalSignal, string name)
        {
            Name = name;
            ConnectTo(digitalSignal);
        }

        public event EventHandler<DigitalLevelChangedEventArgs> OnChanged;

        private DigitalSignal _digitalSignal;

        public DigitalSignal DigitalSignal
        {
            get { return _digitalSignal; }
        }

        public void ConnectTo(DigitalSignal digitalSignal)
        {
            if (digitalSignal == null)
                throw new ArgumentNullException(nameof(digitalSignal));
            if (_digitalSignal != null)
                throw new InvalidOperationException("DigitalConsumer is already connected.");

            _digitalSignal = digitalSignal;
            _digitalSignal.Attach(this);

            _digitalSignal.OnChanged += DigitalSignal_OnChanged;
        }

        private string _name;

        public string Name
        {
            get
            {
                if (IsConnected && _name == null)
                    return _digitalSignal.Name;
                return _name;
            }
            set
            { _name = value; }
        }

        public DigitalLevel Level
        {
            get
            {
                if (!IsConnected) return DigitalLevel.Floating;

                return _digitalSignal.Level;
            }
        }

        public bool IsConnected
        {
            get { return _digitalSignal != null; }
        }

        private void DigitalSignal_OnChanged(object sender, DigitalLevelChangedEventArgs e)
        {
            OnChanged?.Invoke(this, e);
        }

        private void ThrowIfNotConnected()
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException(
                    "DigitalConsumer is not connected to a DigitalSignal.");
            }
        }
    }
}
