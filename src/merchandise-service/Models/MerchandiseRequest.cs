using MerchandaiseDomain.AggregationModels.EmployeeAgregate;
using MerchandaiseDomain.AggregationModels.MerchAgregate;

namespace MerchandiseService.Models
{
    public class MerchandiseRequest
    {
        public string Email { get; }
        public int MerchId { get; }

        public MerchandiseRequest(string Email, int MerchId)
        {
            this.Email = Email;
            this.MerchId = MerchId;
        }
    }
}