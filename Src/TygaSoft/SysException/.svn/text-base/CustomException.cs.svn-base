using System;
using System.Runtime.Serialization;

namespace TygaSoft.SysException
{
    [Serializable]
    public class CustomException : Exception, ISerializable
    {
        public CustomException() { }

        public CustomException(string message): base(message){}

        public CustomException(string message, string paramName) : base(message) { }

        public CustomException(string message, Exception innerException): base(message, innerException)
        {
            Log.Error(message, innerException);
        }

        protected CustomException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
