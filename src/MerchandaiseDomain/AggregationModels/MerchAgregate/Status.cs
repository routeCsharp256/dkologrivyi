using MerchandaiseDomain.Models;

namespace MerchandaiseDomain.AggregationModels.MerchAgregate
{
    public class Status:Enumeration
    {
        public static Status Processing = new(10, nameof(Processing));
        public static Status Waiting = new(20, nameof(Waiting));
        public static Status Issued = new(30, nameof(Issued));

        public Status(int id, string name) : base(id, name)
        {
        }
    }
}