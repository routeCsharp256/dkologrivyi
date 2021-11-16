using System;

namespace MerchandaiseDomain.Exceptions.EmployeeValidation
{
    public class EmployeeEmailInvalidException: Exception
    {
        public EmployeeEmailInvalidException(string message) : base(message)
        {
            
        }

        public EmployeeEmailInvalidException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}