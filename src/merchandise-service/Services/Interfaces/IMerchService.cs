using System.Threading.Tasks;
using MerchandiseService.Models;

namespace MerchandiseService.Services.Interfaces
{
    public interface IMerchService
    {
        public Task RequestMerch(long employeeId, MerchType merchType);

        public Task CheckWasIssued(long employeeId, MerchType merchType);

        public Task NewSupply(SupplyShippedEvent supplyShippedEvent);
        
        public Task NewNotification(NotificationEvent notificationEvent);

    }
}