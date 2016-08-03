using System;

namespace Jacobi.Zim80
{
    //[System.Serializable]
    public class DigitalSignalConflictException : Exception
    {
        public DigitalSignalConflictException() { }
        public DigitalSignalConflictException(string message) : base(message) { }
        public DigitalSignalConflictException(string message, Exception inner) : base(message, inner) { }
        //protected DigitalSignalConflictException(
        //  System.Runtime.Serialization.SerializationInfo info,
        //  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
