using System.Linq;

namespace Jacobi.Zim80.Logic
{
    public class AndGate : Gate
    {
        protected DigitalLevel AndFunction()
        {
            if (Inputs.All((c) => c.Level == DigitalLevel.High))
                return DigitalLevel.High;

            return DigitalLevel.Low;
        }

        protected override void OnInputChanged(DigitalSignalConsumer input, DigitalSignalProvider source)
        {
            Output.Write(AndFunction());
        }
    }
}
