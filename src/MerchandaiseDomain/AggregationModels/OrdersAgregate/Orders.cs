using System;
using System.Collections.Generic;
using System.Linq;
using MerchandaiseDomain.AggregationModels.EmployeeAgregate;
using MerchandaiseDomain.AggregationModels.MerchAgregate;
using MerchandaiseDomain.Exceptions;
using MerchandaiseDomain.Exceptions.OrdersValidation;
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
        /// <exception cref="OrdersMerchNullException"></exception>
        public void CheckWasIssued(Merch merchToFind)
        {
            if (merchToFind is null)
                throw new OrdersMerchNullException("merch to find cannot be null!");

            var result = (from currentMerch in Merches
                where
                    Equals(currentMerch.Status, Status.Issued) && Equals(currentMerch.Type, merchToFind.Type) &&
                    (currentMerch.RequestDate.Value.AddYears(1) > DateTime.Now)
                select currentMerch).ToList();
            if (result.Count > 0)
                throw new MerchAlreadyIssuedException("merch has been already issued");
        }

        /// <summary>
        /// проверяем не выдавался ли уже такой мерч и не был ли он уже запрошен, но еще не выдан.
        /// </summary>
        /// <param name="merchToRequest"></param>
        /// <exception cref="OrdersMerchNullException"></exception>
        /// <exception cref="MerchAlreadyRequestedException"></exception>
        public void CheckWasRequested(Merch merchToRequest)
        {
            if (merchToRequest is null)
                throw new OrdersMerchNullException("merch to request cannot be null!");
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
            if (merchToAdd is null)
                throw new OrdersMerchNullException("merch to add cannot be null!");
            
            Merches.Add(merchToAdd);
        }

        private bool OrdersCreationValidation(Employee employee, List<Merch> merches)
        {
            if (employee is null)
                throw new OrdersEmployeeNullException("employee cannot be null");
            if (merches is null)
                throw new OrdersMerchesListNullException("Merches list cannot be null");
            return true;
        }
    }
}