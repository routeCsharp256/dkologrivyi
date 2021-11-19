using System.Threading;
using System.Threading.Tasks;
using MerchandaiseDomain.AggregationModels.Contracts;

namespace MerchandaiseInfrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}