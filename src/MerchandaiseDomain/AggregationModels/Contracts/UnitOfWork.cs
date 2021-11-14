using System.Threading;
using System.Threading.Tasks;

namespace MerchandaiseDomain.AggregationModels.Contracts
{
    public class UnitOfWork : IUnitOfWork
    {
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}