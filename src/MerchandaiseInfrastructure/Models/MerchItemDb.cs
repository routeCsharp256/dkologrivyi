namespace MerchandaiseInfrastructure.Models
{
    public class MerchItemDb
    {
        public MerchItemDb(long? id, long skuId, long quantity, long orderedMerchId)
        {
            Id = id;
            SkuId = skuId;
            Quantity = quantity;
            OrderedMerchId = orderedMerchId;
        }
        public long? Id { get; set; }
        public long SkuId { get; set; }
        public long Quantity { get; set; }
        public long OrderedMerchId { get; set; }
    }
}