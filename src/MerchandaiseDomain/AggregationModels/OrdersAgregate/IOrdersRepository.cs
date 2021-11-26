using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MerchandaiseDomain.AggregationModels.Contracts;
using MerchandaiseDomain.AggregationModels.MerchAgregate;

namespace MerchandaiseDomain.AggregationModels.OrdersAgregate
{
    public interface IOrdersRepository
    {
        /// <summary>
        /// Найти заказы пользователя по его id
        /// </summary>
        /// <param name="employeeEmail"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Orders> FindByEmloyeeEmailAsync(string employeeEmail, CancellationToken cancellationToken);
        Task<List<Orders>> GetOrdersByStatus(Status status, CancellationToken cancellationToken);

        Task CreateAsync(long employeeId, long orderedMerchId, CancellationToken cancellationToken);
    }
}