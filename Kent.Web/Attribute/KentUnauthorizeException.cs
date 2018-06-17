using System;
using System.Runtime.Serialization;

namespace Kent.Web.Attribute
{
    [Serializable]
    internal class KentUnauthorizeException : Exception
    {
        public KentUnauthorizeException()
        {
        }

        public KentUnauthorizeException(string message) : base(message)
        {
        }

        public KentUnauthorizeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected KentUnauthorizeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}