using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MerchandaiseDomain.AggregationModels.Contracts;

namespace MerchandaiseDomain.AggregationModels.OrdersAgregate
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

        public async Task<List<Orders>> GetUnIssuedOrders(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}