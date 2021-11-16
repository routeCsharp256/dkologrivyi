using MerchandaiseDomain.AggregationModels.EmployeeAgregate;
using MerchandaiseDomain.AggregationModels.MerchAgregate;

namespace MerchandiseService.Models
{
    public class MerchandiseRequest
    {
        public Employee Employee { get; }
        public Merch Merch { get; }

        public MerchandiseRequest(Employee employee, Merch merch)
        {
            Employee = employee;
            Merch = merch;
        }
    }
}