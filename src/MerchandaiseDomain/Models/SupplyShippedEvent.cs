using System.Collections.Generic;

namespace MerchandaiseDomain.Models
{
    public class SupplyShippedEvent
    {
        /// <summary>
        /// Идентификатор поставки.
        /// </summary>
        public long SupplyId { get; set; }

        /// <summary>
        /// Коллекция товаров в поставке.
        /// </summary>
        public ICollection<SupplyShippedItem> Items { get; set; }
    }

    public class SupplyShippedItem
    {
        /// <summary>
        /// Идентификатор товара
        /// </summary>
        public long SkuId { get; set; }

        /// <summary>
        /// Количество товаров.
        /// </summary>
        public long Quantity { get; set; }
    }
}