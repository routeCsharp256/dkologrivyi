using System;
using System.Collections.Generic;
using MerchandaiseDomain.Models;

namespace MerchandaiseDomain.AggregationModels.MerchAgregate
{
    public class RequestDate:ValueObject
    {
        public DateTime Value { get; }

        public RequestDate(DateTime value)
        {
            Value = value;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}