using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MerchandaiseDomain.AggregationModels.MerchAgregate;
using MerchandaiseDomain.AggregationModels.OrdersAgregate;
using MerchandaiseDomain.Models;


namespace MerchandaiseDomainServices.Interfaces
{
    public interface IMerchService
    {
        public Task<Orders> RequestMerch(string employeeEmail, int merchTypeId, CancellationToken token);
        
        public Task CheckWasIssued(long employeeId, MerchType merchType, CancellationToken token);

        public Task NewSupply(SupplyShippedEvent supplyShippedEvent, CancellationToken token);
        
        public Task NewNotification(NotificationEvent notificationEvent, CancellationToken token);

        public Task<List<Merch>> GetAvailableMerchList(CancellationToken token);
    }
}