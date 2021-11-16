using System.Collections.Generic;
using System.Threading.Tasks;
using MerchandaiseDomain.AggregationModels.MerchAgregate;


namespace MerchandaiseDomainServices.Interfaces
{
    public interface IStockGateway
    {
        Task<bool> CheckIsAvailableAsync(List<MerchItem> merchItemsList);

        Task<bool> TryDeliverSkuAsync(string email, List<MerchItem> merchItemsList);
    }
}