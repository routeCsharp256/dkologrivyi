using System.Collections.Generic;
using System.Threading.Tasks;
using MerchandaiseDomain.AggregationModels.MerchAgregate;
using MerchandaiseGrpc.StockApi;
using MerchandaiseGrpcClient;
using MerchandaiseDomainServices.Interfaces;

namespace MerchandaiseInfrastructure
{
    public class StockGateway:IStockGateway
    {
        private readonly IStockClient _stockClient;


        public StockGateway(IStockClient stockClient)
        {
            _stockClient = stockClient;
        }
        public async Task<bool> CheckIsAvailableAsync(List<MerchItem> merchItemsList)
        {
            var items = new List<Item>();
            foreach (var item in merchItemsList)
            {
                items.Add(new Item() {SkuId = item.Sku.Value, Quantity = item.Quantity.Value});
            }

            return await _stockClient.CheckIsAvailableAsync(items);
        }

        public async Task<bool> TryDeliverSkuAsync(string email, List<MerchItem> merchItemsList)
        {
            var items = new List<Item>();
            foreach (var item in merchItemsList)
            {
                items.Add(new Item() {SkuId = item.Sku.Value, Quantity = item.Quantity.Value});
            }

            return await _stockClient.TryDeliverSkuAsync(email, items);
        }
    }
}