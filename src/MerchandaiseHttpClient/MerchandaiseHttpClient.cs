using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MerchandaiseHttpClient.Interfaces;
using MerchandiseService.Models;

namespace MerchandaiseHttpClient
{
    public class MerchandaiseHttpClient:IMerchandaiseHttpClient
    {
        private readonly HttpClient _httpClient;

        public MerchandaiseHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<List<MerchandiseResponse>> V1GetIssuedMerches(long employeeId, CancellationToken token)
        {
            using var response = await _httpClient.GetAsync($"v1/api/merch/getIssuedMerches/{employeeId}", token);
            var result = await response.Content.ReadAsStringAsync(token);
            return JsonSerializer.Deserialize<List<MerchandiseResponse>>(result);
        }

        public async Task V1RequestMerch(MerchandiseRequest merchandiseRequest, CancellationToken token)
        {
            StringContent body = new StringContent(JsonSerializer.Serialize(merchandiseRequest), Encoding.UTF8, "application/json");
            using var response = await _httpClient.GetAsync("v1/api/merch/requestMerch", token);
            await response.Content.ReadAsStringAsync(token);
            
        }
    }
}