using System.Threading.Tasks;
using MerchandaiseDomain.AggregationModels.MerchAgregate;
using MerchandaiseDomain.Models;


namespace MerchandaiseDomainServices.Interfaces
{
    public interface IMerchService
    {
        public Task RequestMerch(string employeeEmail, MerchType merchType);
        
        public Task RequestMerch(long employeeId, MerchType merchType);

        public Task CheckWasIssued(long employeeId, MerchType merchType);

        public Task NewSupply(SupplyShippedEvent supplyShippedEvent);
        
        public Task NewNotification(NotificationEvent notificationEvent);

    }
}