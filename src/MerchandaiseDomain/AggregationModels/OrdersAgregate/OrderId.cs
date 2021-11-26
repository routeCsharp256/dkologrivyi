using System.Collections.Generic;
using MerchandaiseDomain.Models;

namespace MerchandaiseDomain.AggregationModels.OrdersAgregate
{
    public class OrderId: ValueObject
    {
        public long Value { get; }

        public OrderId(long orderId)
        {
            Value = orderId;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}