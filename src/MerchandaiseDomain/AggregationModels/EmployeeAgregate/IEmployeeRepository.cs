using System.Threading;
using System.Threading.Tasks;
using MerchandaiseDomain.AggregationModels.Contracts;

namespace MerchandaiseDomain.AggregationModels.EmployeeAgregate
{
    public interface IEmployeeRepository:IRepository<Employee>
    {
        Task<Employee> FindEmployeeByEmail(string email, CancellationToken cancellationToken = default);
    }
}