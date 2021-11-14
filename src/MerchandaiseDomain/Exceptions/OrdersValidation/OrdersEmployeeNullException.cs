using System;

namespace MerchandaiseDomain.Exceptions.OrdersValidation
{
    public class OrdersEmployeeNullException:Exception
    {
        public OrdersEmployeeNullException(string message) : base(message)
        {
            
        }

        public OrdersEmployeeNullException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}