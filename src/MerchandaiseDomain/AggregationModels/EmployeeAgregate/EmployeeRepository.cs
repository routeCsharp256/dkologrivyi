using System.Threading;
using System.Threading.Tasks;
using MerchandaiseDomain.AggregationModels.Contracts;

namespace MerchandaiseDomain.AggregationModels.EmployeeAgregate
{
    public class EmployeeRepository:IEmployeeRepository
    {
        public IUnitOfWork UnitOfWork { get; }
        public async Task<Employee> CreateAsync(Employee itemToCreate, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Employee> UpdateAsync(Employee itemToUpdate, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Employee> FindEmployeeByEmail(string email, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}