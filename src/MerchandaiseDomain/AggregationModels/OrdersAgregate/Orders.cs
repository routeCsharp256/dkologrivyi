using System;
using System.Collections.Generic;
using System.Linq;
using MerchandaiseDomain.AggregationModels.EmployeeAgregate;
using MerchandaiseDomain.AggregationModels.MerchAgregate;
using MerchandaiseDomain.Exceptions;
using MerchandaiseDomain.Models;

namespace MerchandaiseDomain.AggregationModels.OrdersAgregate
{
    public class Orders : Entity
    {
        public Employee Employee { get; }
        public List<Merch> Merches { get; private set; }

        public Orders(Employee employee, List<Merch> merches)
        {
            Employee = employee;
            Merches = merches;
        }

        /// <summary>
        /// проверяет выдавался ли мерч сотруднику ранее. Если с момента выдачи прошло более года, то считаем, что не выдавался
        /// </summary>
        /// <param name="merchToFind"></param>
        /// <exception cref="MerchAlreadyIssuedException"></exception>
        public void CheckWasIssued(Merch merchToFind)
        {
            var result = (from currentMerch in Merches
                where Equals(currentMerch.Status, Status.Issued) && Equals(currentMerch.Type, merchToFind.Type) &&
                      (currentMerch.RequestDate.Value.AddYears(1) < DateTime.Now)
                select currentMerch);
            if (result.Any())
                throw new MerchAlreadyIssuedException("merch has been already issued");
        }

        /// <summary>
        /// проверяем не выдавался ли уже такой мерч и не был ли он уже запрошен, но еще не выдан.
        /// </summary>
        /// <param name="merchToRequest"></param>
        public void CheckWasRequested(Merch merchToRequest)
        {
            CheckWasIssued(merchToRequest);
            var result = (from currentMerch in Merches
                where !Equals(currentMerch.Status, Status.Issued) && currentMerch.Type.Id == merchToRequest.Type.Id
                select currentMerch);
            if (result.Any())
                throw new MerchAlreadyRequestedException(
                    "merch has been already requested, but not issued yet. Please wait for delivery on stock");
        }

        public void AddMerchToOrders(Merch merchToAdd)
        {
            Merches.Add(merchToAdd);
        }


        
    }
}