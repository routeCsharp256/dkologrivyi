using System;
using System.Collections.Generic;
using System.Globalization;
using MerchandaiseDomain.AggregationModels.EmployeeAgregate;
using MerchandaiseDomain.AggregationModels.MerchAgregate;
using MerchandaiseDomain.AggregationModels.OrdersAgregate;
using MerchandaiseDomain.Exceptions;
using Xunit;
using Id = MerchandaiseDomain.AggregationModels.EmployeeAgregate.Id;

namespace MerchandaiseTests
{
    public class OrdersTests
    {
        [Fact]
        public void AddMerchToOrders_AddMerchToMerchItemsCollection()
        {
            var orders = GetOrdersForTest(DateTime.Now.AddMonths(-6), Status.Issued, MerchType.WelcomePack);

            var newMerch = GetMerchForTest(DateTime.Now, Status.Processing, MerchType.ConferenceListenerPack);

            //Act
            orders.AddMerchToOrders(newMerch);

            //Assert
            Assert.Equal(2, orders.Merches.Count);
            Assert.Equal(MerchType.ConferenceListenerPack, orders.Merches[^1].Type);
        }

        [Fact]
        public void CheckWasRequested_IfRequested_Throws()
        {
            var orders = GetOrdersForTest(DateTime.Now, Status.Processing, MerchType.WelcomePack);
            var newMerch = GetMerchForTest(DateTime.Now, Status.Processing, MerchType.WelcomePack);

            //Assert
            Assert.Throws<MerchAlreadyRequestedException>(() => orders.CheckWasRequested(newMerch));
        }

        [Fact]
        public void CheckWasIssued_SecondWelcomePackBeforeYearWork_ThrowsTest()
        {
            var orders = GetOrdersForTest(DateTime.Now.AddDays(-364), Status.Issued, MerchType.WelcomePack);
            var newMerch = GetMerchForTest(DateTime.Now, Status.Processing, MerchType.WelcomePack);
            Assert.Throws<MerchAlreadyIssuedException>(() => orders.CheckWasIssued(newMerch));
        }

        [Fact]
        public void CheckWasIssued_SecondWelcomePackAfterYearWork_DoesNotThrowsTest()
        {
            var orders = GetOrdersForTest(DateTime.Now.AddDays(-366), Status.Issued, MerchType.WelcomePack);
            var newMerch = GetMerchForTest(DateTime.Now, Status.Processing, MerchType.WelcomePack);
            orders.CheckWasIssued(newMerch);
        }


        private static Orders GetOrdersForTest(DateTime welcomePackRequestDate, Status status, MerchType merchType)
        {
            return new Orders(
                new Employee(new Id(13), new FirstName("John"), new MiddleName("Petrovich"), new LastName("Ivanov"),
                    new Email("frodo@gmail.com")),
                new List<Merch>()
                {
                    GetMerchForTest(welcomePackRequestDate, status, merchType)
                }
            );
        }

        private static Orders GetOrdersForTest()
        {
            return new Orders(
                new Employee(new Id(13), new FirstName("John"), new MiddleName("Petrovich"), new LastName("Ivanov"),
                    new Email("frodo@gmail.com")),
                new List<Merch>()
            );
        }

        private static Merch GetMerchForTest(DateTime requestDate, Status status, MerchType merchType)
        {
            return new Merch(
                new MerchId(1),
                new Name("Мерч пак"), merchType,
                new List<MerchItem>()
                {
                    new MerchItem(new Sku(2), new MerchItemQuantity(2)),
                    new MerchItem(new Sku(1), new MerchItemQuantity(4))
                },
                status,
                new RequestDate(requestDate));
        }
    }
}