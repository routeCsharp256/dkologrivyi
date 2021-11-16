using System.Collections.Generic;
using MerchandaiseDomain.Models;

namespace MerchandaiseDomain.AggregationModels.EmployeeAgregate
{
    public class LastName:ValueObject
    {
        public string Value { get; }

        public LastName(string lastName)
        {
            Value = lastName;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}