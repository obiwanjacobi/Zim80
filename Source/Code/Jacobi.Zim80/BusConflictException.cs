using System;

namespace Jacobi.Zim80
{
    //[System.Serializable]
    public class BusConflictException : Exception
    {
        public BusConflictException() { }
        public BusConflictException(string message) : base(message) { }
        public BusConflictException(string message, Exception inner) : base(message, inner) { }
        //protected BusConflictException(
        //  System.Runtime.Serialization.SerializationInfo info,
        //  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
