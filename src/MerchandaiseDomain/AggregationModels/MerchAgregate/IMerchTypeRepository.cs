using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MerchandaiseDomain.AggregationModels.MerchAgregate
{
    public interface IMerchTypeRepository
    {
        Task<IEnumerable<MerchType>> GetAllTypes(CancellationToken token);
    }
}