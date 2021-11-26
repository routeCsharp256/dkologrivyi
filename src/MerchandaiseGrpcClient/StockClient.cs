using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using MerchandaiseGrpc.StockApi;


namespace MerchandaiseGrpcClient
{
    public class StockClient : IStockClient
    {

        public StockClient(StockApiGrpc.StockApiGrpcClient client)
        {
            _client = client;
        }

        private readonly StockApiGrpc.StockApiGrpcClient _client;

        public async Task<bool> CheckIsAvailableAsync(List<Item> items)
        {
            var request = new CheckIsAvailableRequest() {Items = {items}};
            var responce = await _client.CheckIsAvailableAsync(request);
            return responce.Value;
        }

        public async Task<bool> TryDeliverSkuAsync(string employeeEmail, List<Item> items)
        {
            var request = new TryDeliverSkuRequest() {EmployeeEmail = employeeEmail, Items = {items}};
            var responce = await _client.TryDeliverSkuAsync(request);
            return responce.Value;
        }
    }
}