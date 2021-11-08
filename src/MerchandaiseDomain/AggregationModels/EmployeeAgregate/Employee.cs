using MerchandaiseDomain.Models;

namespace MerchandaiseDomain.AggregationModels.EmployeeAgregate
{
    public class Employee : Entity
    {
        public Employee(Id id,
            FirstName firstName, MiddleName middleName, LastName lastName,
            Email email,
            ClothingSize clothingSize)
        {
            Id = id;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Email = email;
            ClothingSize = clothingSize;
        }

        public Id Id { get; }
        public FirstName FirstName { get; }
        public MiddleName MiddleName { get; }
        public LastName LastName { get; }
        public Email Email { get; }
        public ClothingSize ClothingSize { get; }
    }
}