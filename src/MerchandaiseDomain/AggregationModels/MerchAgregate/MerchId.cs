using System.Collections.Generic;
using MerchandaiseDomain.Models;

namespace MerchandaiseDomain.AggregationModels.MerchAgregate
{
    public class MerchId : ValueObject
    {
        public long Value { get; }

        public MerchId(long merchId)
        {
            Value = merchId;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new System.NotImplementedException();
        }
    }
}