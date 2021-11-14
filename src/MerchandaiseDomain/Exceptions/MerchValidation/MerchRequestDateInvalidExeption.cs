using System;

namespace MerchandaiseDomain.Exceptions.MerchValidation
{
    public class MerchRequestDateInvalidExeption:Exception
    {
        public MerchRequestDateInvalidExeption(string message) : base(message)
        {
            
        }

        public MerchRequestDateInvalidExeption(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}