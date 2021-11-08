using System.Collections.Generic;
using MerchandaiseDomain.Models;

namespace MerchandaiseDomain.AggregationModels.EmployeeAgregate
{
    public class Id : ValueObject
    {
        public long Value { get; }

        public Id(long id)
        {
            Value = id;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new System.NotImplementedException();
        }
    }
}