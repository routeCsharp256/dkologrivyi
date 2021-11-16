using System.Threading.Tasks;
using MerchandaiseDomain.AggregationModels.Contracts;

namespace MerchandaiseDomain.AggregationModels.MerchAgregate
{
    public interface IMerchRepository:IRepository<Merch>
    {
        Task<Merch> FindByMerchType(int merchTypeId);
        
        
    }
}