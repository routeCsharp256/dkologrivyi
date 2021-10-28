using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MerchandiseService.Models;

namespace MerchandaiseHttpClient.Interfaces
{
    public interface IMerchandaiseHttpClient
    {
        Task<List<MerchandiseResponse>> V1GetIssuedMerches(long employeeId, CancellationToken token);
        Task V1RequestMerch(MerchandiseRequest merchandiseRequest, CancellationToken token);
    }
    
    
}