using System;

namespace MerchandaiseDomain.Exceptions.OrdersValidation
{
    public class OrdersMerchesListNullException:Exception
    {
        public OrdersMerchesListNullException(string message) : base(message)
        {
            
        }

        public OrdersMerchesListNullException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}