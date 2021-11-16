using System.Collections.Generic;
using MerchandaiseDomain.Exceptions.MerchValidation;
using MerchandaiseDomain.Models;

namespace MerchandaiseDomain.AggregationModels.MerchAgregate
{
    public class Name : ValueObject
    {
        public string Value { get; }

        public Name(string name)
        {
            ValidateName(name);
            Value = name;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        private bool ValidateName(string name)
        {
            if (name is null)
                throw new MerchNameInvalidException("Name cannot be null!");
            return true;
        }
    }
}