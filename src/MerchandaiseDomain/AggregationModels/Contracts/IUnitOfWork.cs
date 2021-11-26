using System.Threading;
using System.Threading.Tasks;

namespace MerchandaiseDomain.AggregationModels.Contracts
{
    public interface IUnitOfWork
    {
        ValueTask StartTransaction(CancellationToken token);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}