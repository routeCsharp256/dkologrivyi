using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MerchandaiseDomain.AggregationModels.Contracts;

namespace MerchandaiseDomain.AggregationModels.OrdersAgregate
{
    public interface IOrdersRepository:IRepository<Orders>
    {

        /// <summary>
        /// Найти заказы пользователя по его id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Orders> FindByEmloyeeIdAsync(long id, CancellationToken cancellationToken = default);

        Task<Orders> FindByEmloyeeEmailAsync(string employeeEmail, CancellationToken cancellationToken = default);
        Task<List<Orders>> GetUnIssuedOrders(CancellationToken cancellationToken = default);

    }
}