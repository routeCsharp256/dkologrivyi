using System;

namespace MerchandaiseDomain.Exceptions.MerchValidation
{
    public class MerchNameInvalidException:Exception
    {
        public MerchNameInvalidException(string message) : base(message)
        {
            
        }

        public MerchNameInvalidException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}