using System;

namespace MerchandaiseDomain.Exceptions.MerchValidation
{
    public class MerchTypeInvalidException:Exception
    {
        public MerchTypeInvalidException(string message) : base(message)
        {
            
        }

        public MerchTypeInvalidException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}