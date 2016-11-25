﻿namespace Jacobi.Zim80.Components.Logic
{
    public class NorGate : OrGate
    {
        protected override void OnInputChanged(DigitalSignalConsumer input, DigitalSignalProvider source)
        {
            Output.Write(InvertorGate.Invert(OrFunction()));
        }
    }
}
