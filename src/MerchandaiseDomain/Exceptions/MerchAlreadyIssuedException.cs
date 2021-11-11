using System;

namespace MerchandaiseDomain.Exceptions
{
    public class MerchAlreadyIssuedException : Exception
    {
        public MerchAlreadyIssuedException(string message) : base(message)
        {
        }

        public MerchAlreadyIssuedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}