using System.Linq;

namespace Jacobi.Zim80.Logic
{
    public class OrGate : Gate
    {
        protected override void OnInputChanged(DigitalSignalConsumer input, DigitalSignalProvider source)
        {
            Output.Write(OrFunction());
        }

        protected DigitalLevel OrFunction()
        {
            if (Inputs.All((c) => c.Level == DigitalLevel.Low))
                return DigitalLevel.Low;

            return DigitalLevel.High;
        }
    }
}
