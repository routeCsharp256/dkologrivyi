using System.Collections.Generic;
using System.Text.RegularExpressions;
using MerchandaiseDomain.Exceptions.EmployeeValidation;
using MerchandaiseDomain.Models;

namespace MerchandaiseDomain.AggregationModels.EmployeeAgregate
{
    public class Email:ValueObject
    {
        public string Value { get; }
        
        
        public Email(string email)
        {
            if (!Validate(email))
                throw new EmployeeEmailInvalidException("Employee email is incorrect!");
            Value = email;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
        
        private static bool Validate(string emailAddress)
        {
            var regex = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
            bool isValid = Regex.IsMatch(emailAddress, regex, RegexOptions.IgnoreCase);
            return isValid;
        }
        
    }
}