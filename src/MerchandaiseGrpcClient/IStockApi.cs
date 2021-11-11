using System.Collections.Generic;
using System.Threading.Tasks;
using MerchandaiseGrpc.StockApi;

namespace MerchandaiseGrpcClient
{
    public interface IStockApi
    {
        Task<bool> CheckIsAvailableAsync(List<Item> items);
        Task<bool> TryDeliverSkuAsync(string employeeEmail, List<Item> items);
    }
}