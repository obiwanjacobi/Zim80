﻿namespace Jacobi.Zim80.Components.Logic
{
    public class InvertorGate
    {
        private readonly DigitalSignalConsumer _input;
        private readonly DigitalSignalProvider _output;

        public InvertorGate()
        {
            _input = new DigitalSignalConsumer("Invertor Input");
            _output = new DigitalSignalProvider("Invertor Output");

            _input.OnChanged += Input_OnChanged;
        }

        private void Input_OnChanged(object sender, DigitalLevelChangedEventArgs e)
        {
            Output.Write(Invert(e.Level));
        }

        public DigitalSignalConsumer Input { get { return _input; } }
        public DigitalSignalProvider Output { get { return _output; } }

        public static DigitalLevel Invert(DigitalLevel level)
        {
            if (level == DigitalLevel.Floating) return DigitalLevel.Floating;
            var newLevel = level + 2;

            if (newLevel > DigitalLevel.NegEdge)
                newLevel -= 4;

            return newLevel;
        }
    }
}
