using System;

namespace MerchandaiseDomain.Exceptions
{
    public class MerchChangeStatusException:Exception
    {
        public MerchChangeStatusException(string message) : base(message)
        {
            
        }

        public MerchChangeStatusException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
        
    }
}