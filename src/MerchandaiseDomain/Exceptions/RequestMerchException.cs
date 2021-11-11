using System;

namespace MerchandaiseDomain.Exceptions
{
    public class RequestMerchException : Exception
    {
        public RequestMerchException(string message) : base(message)
        {
            
        }

        public RequestMerchException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}