using System.Threading;
using System.Threading.Tasks;
using MerchandaiseDomain.AggregationModels.MerchAgregate;
using MerchandaiseDomain.Models;


namespace MerchandaiseDomainServices.Interfaces
{
    public interface IMerchService
    {
        public Task RequestMerch(string employeeEmail, MerchType merchType, CancellationToken token);
        
        public Task RequestMerch(long employeeId, MerchType merchType, CancellationToken token);

        public Task CheckWasIssued(long employeeId, MerchType merchType, CancellationToken token);

        public Task NewSupply(SupplyShippedEvent supplyShippedEvent, CancellationToken token);
        
        public Task NewNotification(NotificationEvent notificationEvent, CancellationToken token);

    }
}