using System.Collections.Generic;
using MerchandaiseDomain.Models;

namespace MerchandaiseDomain.AggregationModels.MerchAgregate
{
    public class Merch : Entity
    {
        public Merch(Name name, MerchType type, List<MerchItem> merchItems)
        {
            Name = name;
            Type = type;
            MerchItems = merchItems;
        }

        public Name Name { get; set; }
        public MerchType Type { get; set; }
        public List<MerchItem> MerchItems { get; set; }
    }
}