using System.Threading;
using System.Threading.Tasks;
using MerchandaiseDomain.AggregationModels.Contracts;

namespace MerchandaiseDomain.AggregationModels.MerchAgregate
{
    public class MerchRepository:IMerchRepository
    {
        public IUnitOfWork UnitOfWork { get; }
        public async Task<Merch> CreateAsync(Merch itemToCreate, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Merch> UpdateAsync(Merch itemToUpdate, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Merch> FindByMerchType(int merchTypeId)
        {
            throw new System.NotImplementedException();
        }
    }
}