using System.Collections.Generic;
using MerchandaiseDomain.Exceptions.MerchValidation;
using MerchandaiseDomain.Models;

namespace MerchandaiseDomain.AggregationModels.MerchAgregate
{
    public class MerchItemQuantity : ValueObject
    {
        public long Value { get; set; }

        public MerchItemQuantity(long value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        private bool Validate(int value)
        {
            if (value <= 0)
                throw new MerchItemQuantityInvalidException("quantity cannot be below 0");

            return true;
        }
        
    }
}