using System.Threading;
using System.Threading.Tasks;

namespace MerchandaiseDomain.AggregationModels.Contracts
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}