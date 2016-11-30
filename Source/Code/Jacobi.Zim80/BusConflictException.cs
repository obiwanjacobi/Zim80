using System;

namespace Jacobi.Zim80
{
    public class BusConflictException : Exception
    {
        public BusConflictException() { }
        public BusConflictException(string message) : base(message) { }
        public BusConflictException(string message, Exception inner) : base(message, inner) { }
    }
}
