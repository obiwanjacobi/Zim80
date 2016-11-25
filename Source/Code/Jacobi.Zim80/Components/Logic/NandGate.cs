namespace Jacobi.Zim80.Components.Logic
{
    public class NandGate : AndGate
    {
        protected override void OnInputChanged(DigitalSignalConsumer input, DigitalSignalProvider source)
        {
            Output.Write(InvertorGate.Invert(AndFunction()));
        }
    }
}
