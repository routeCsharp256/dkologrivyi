using System.Collections.Generic;
using MerchandaiseDomain.AggregationModels.MerchAgregate;
using MerchandaiseDomain.Models;

namespace MerchandaiseDomain.AggregationModels.EmployeeAgregate
{
    public class Employee : Entity
    {
        public Employee(Id id,
            FirstName firstName, MiddleName middleName, LastName lastName,
            Email email 
            )
        {
            Id = id;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Email = email;
        }

        public new Id Id { get; }
        public FirstName FirstName { get; }
        public MiddleName MiddleName { get; }
        public LastName LastName { get; }
        public Email Email { get; }
    }
}