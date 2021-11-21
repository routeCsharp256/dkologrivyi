using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MerchandaiseDomain.AggregationModels.Contracts;

namespace MerchandaiseDomain.AggregationModels.MerchAgregate
{
    public interface IMerchRepository:IRepository<Merch>
    {
        Task<Merch> GetAvailableMerchByType(int merchTypeId, CancellationToken cancellationToken);

        Task<List<Merch>> GetAvailableMerchList(CancellationToken cancellationToken);
    }
}