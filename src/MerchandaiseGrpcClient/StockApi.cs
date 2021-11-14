using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using MerchandaiseGrpc.StockApi;
using Microsoft.Extensions.Configuration;

namespace MerchandaiseGrpcClient
{
    public class StockApi : IStockApi
    {

        public StockApi(StockApiGrpc.StockApiGrpcClient client)
        {
            //string url = config.GetSection("StockApiUrl").Value;
            //var channel = GrpcChannel.ForAddress("https://localhost:5001");
            //var client = new StockApiGrpc.StockApiGrpcClient(channel);
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