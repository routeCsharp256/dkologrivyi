using System.Collections.Generic;
using MerchandaiseDomain.Models;

namespace MerchandaiseDomain.AggregationModels.MerchAgregate
{
    public class Sku:ValueObject
    {
        public long Value { get; }

        public Sku(long value)
        {
            Value = value;
        }
        
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}