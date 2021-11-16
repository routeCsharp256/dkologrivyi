using System.Collections.Generic;
using MerchandaiseDomain.Models;

namespace MerchandaiseDomain.AggregationModels.MerchAgregate
{
    public class MerchItem:Entity
    {
        public MerchItem(Sku sku, MerchItemQuantity quantity)
        {
            Sku = sku;
            Quantity = quantity;
        }
        
        public Sku Sku { get; set; }
        public MerchItemQuantity Quantity { get; set; }

    }
}