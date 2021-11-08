using System.Collections.Generic;
using MerchandaiseDomain.Models;

namespace MerchandaiseDomain.AggregationModels.MerchAgregate
{
    public class MerchItem:Entity
    {
        public MerchItem()
        {
                
        }

        public MerchItemType ItemType { get; set; }
        public MerchItemQuantity Quantity { get; set; }

    }
}