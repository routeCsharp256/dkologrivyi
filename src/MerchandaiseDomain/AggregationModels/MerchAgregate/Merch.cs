#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using MerchandaiseDomain.Exceptions;
using MerchandaiseDomain.Models;

namespace MerchandaiseDomain.AggregationModels.MerchAgregate
{
    public class Merch : Entity
    {
        /// <summary>
        /// объект для уже запрошенных мерчей
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="merchItems"></param>
        /// <param name="status"></param>
        /// <param name="requestDate"></param>
        public Merch(Name name, MerchType type, List<MerchItem> merchItems, Status status, RequestDate requestDate)
        {
            Name = name;
            Type = type;
            MerchItems = merchItems;
            Status = status;
            RequestDate = requestDate;
        }

        /// <summary>
        /// Объект для добавления нового мерча
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="merchItems"></param>
        public Merch(Name name, MerchType type, List<MerchItem> merchItems)
        {
            Name = name;
            Type = type;
            MerchItems = merchItems;
            Status = Status.Processing;
            RequestDate = new RequestDate(DateTime.Now);
        }

        public Name Name { get; }
        public MerchType Type { get; }
        public List<MerchItem> MerchItems { get; }
        public Status Status { get; private set;}
        public RequestDate RequestDate { get; private set; }

        public void ChangeStatus(Status status)
        {
            if (Equals(Status, Status.Issued))
                throw new MerchChangeStatusException("Merch is issued. Can't change status!");
            Status = status;
            RequestDate = new RequestDate(DateTime.Now);
        }

        public bool CanBeShipped(List<MerchItem> items)
        {
            if (!Equals(Status, Status.Waiting)) throw new RequestMerchException("merch is in wrong status!");
            bool canBeShipped = false;
            //проверяем есть ли в поставке хотябы одна позиция которая доставлена на склад
            foreach (var item in items)
            {
                var result = from merchItem in MerchItems
                    where Equals(item.Sku, merchItem.Sku) && item.Quantity.Value > merchItem.Quantity.Value
                    select merchItem;
                if (result.Any()) canBeShipped = true;
            }

            return canBeShipped;
        }
        
    }
}