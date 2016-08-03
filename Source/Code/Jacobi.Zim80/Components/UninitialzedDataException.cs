using System;

namespace Jacobi.Zim80.Components
{
    public class UninitialzedDataException : Exception
    {
        public UninitialzedDataException() { }
        public UninitialzedDataException(string message) : base(message) { }
        public UninitialzedDataException(string message, Exception inner) : base(message, inner) { }

        public UninitialzedDataException(int address)
            : base (String.Format("Data was not initialized at address {0}.", address))
        {}
    }
}
