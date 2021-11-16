using System;

namespace MerchandaiseDomain.Exceptions.OrdersValidation
{
    public class OrdersMerchNullException:Exception
    {
        public OrdersMerchNullException(string message) : base(message)
        {
            
        }

        public OrdersMerchNullException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}