using System;
using System.Collections.Generic;
using MerchandaiseDomain.Exceptions.MerchValidation;
using MerchandaiseDomain.Models;

namespace MerchandaiseDomain.AggregationModels.MerchAgregate
{
    public class RequestDate:ValueObject
    {
        public DateTime Value { get; }

        public RequestDate(DateTime value)
        {
            ValidateRequestDate(value);
            Value = value;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        private bool ValidateRequestDate(DateTime date)
        {
            if (date > DateTime.Now)
                throw new MerchRequestDateInvalidExeption("Request date cannot be in future");
            return true;
        }
    }
}