using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MerchandaiseDomain.AggregationModels.Contracts;
using MerchandaiseDomain.AggregationModels.OrdersAgregate;

namespace MerchandaiseInfrastructure.Repositories
{
    public class OrdersRepository:IOrdersRepository
    {
        public IUnitOfWork UnitOfWork { get; }
        public async Task<Orders> CreateAsync(Orders itemToCreate, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Orders> UpdateAsync(Orders itemToUpdate, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Orders> FindByEmloyeeIdAsync(long id, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Orders> FindByEmloyeeEmailAsync(string employeeEmail, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Orders>> GetUnIssuedOrders(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}