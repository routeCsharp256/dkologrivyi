using System;
using System.Threading;
using System.Threading.Tasks;

namespace MerchandaiseInfrastructure.Infrastructure.Interfaces
{
    public interface IDbConnectionFactory<TConnection>:IDisposable
    {
        /// <summary>
        /// Создать подключение к БД.
        /// </summary>
        /// <returns></returns>
        Task<TConnection> CreateConnection(CancellationToken token);
    }
}