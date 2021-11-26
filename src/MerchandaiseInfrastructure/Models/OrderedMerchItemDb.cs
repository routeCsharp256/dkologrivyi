namespace MerchandaiseInfrastructure.Models
{
    public class OrderedMerchItemDb
    {
        //id, skuid, quantity, availablemerchid

        public long Id { get; set; }
        public long SkuId { get; set; }
        public int Quantity { get; set; }
        public long OrderedMerchId { get; set; }
    }
}