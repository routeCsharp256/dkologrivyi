using System;

namespace MerchandaiseDomain.Exceptions
{
    public class NotEnoughtItemsInStockException : Exception
    {
        public NotEnoughtItemsInStockException(string message) : base(message)
        {
        }

        public NotEnoughtItemsInStockException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}