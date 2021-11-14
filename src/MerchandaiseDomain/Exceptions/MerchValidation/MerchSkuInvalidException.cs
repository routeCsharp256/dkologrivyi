using System;

namespace MerchandaiseDomain.Exceptions.MerchValidation
{
    public class MerchSkuInvalidException:Exception
    {
        public MerchSkuInvalidException(string message) : base(message)
        {
            
        }

        public MerchSkuInvalidException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}