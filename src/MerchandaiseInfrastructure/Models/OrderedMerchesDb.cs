using System;

namespace MerchandaiseInfrastructure.Models
{
    public class OrderedMerchesDb
    {
        public long MerchId { get; set; }
        public string Name { get; set; }
        public int MerchTypeId { get; set; }
        public int StatusId { get; set; }
        public DateTime RequestDate { get; set; }
    }
}