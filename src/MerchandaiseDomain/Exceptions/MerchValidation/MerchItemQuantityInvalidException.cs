using System;

namespace MerchandaiseDomain.Exceptions.MerchValidation
{
    public class MerchItemQuantityInvalidException:Exception
    {
        public MerchItemQuantityInvalidException(string message) : base(message)
        {
            
        }

        public MerchItemQuantityInvalidException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}