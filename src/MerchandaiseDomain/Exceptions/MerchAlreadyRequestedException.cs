using System;

namespace MerchandaiseDomain.Exceptions
{
    public class MerchAlreadyRequestedException:Exception
    {
        public MerchAlreadyRequestedException(string message) : base(message)
        {
            
        }

        public MerchAlreadyRequestedException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}