using System;
using System.Collections.Generic;
using MerchandaiseDomain.AggregationModels.EmployeeAgregate;
using MerchandaiseDomain.AggregationModels.MerchAgregate;
using MerchandaiseDomain.AggregationModels.OrdersAgregate;
using MerchandaiseDomain.Exceptions;
using Xunit;

namespace MerchandaiseTests
{
    public class OrdersTests
    {
        [Fact]
        public void AddNewMerchTest()
        {
            var orders = new Orders(
                new Employee(new Id(13), new FirstName("John"), new MiddleName("Petrovich"), new LastName("Ivanov"),
                    new Email("frodo@gmail.com")),
                new List<Merch>()
                {
                    new Merch(new Name("Мерч нового сотрудника"), MerchType.WelcomePack,
                        new List<MerchItem>()
                        {
                            new MerchItem(new Sku(2), new MerchItemQuantity(2)),
                            new MerchItem(new Sku(1), new MerchItemQuantity(4))
                        },
                        Status.Processing,
                        new RequestDate(DateTime.Now)
                    )
                }
            );

            var newMerch = new Merch(new Name("Мерч слушателя курса"), MerchType.ConferenceListenerPack,
                new List<MerchItem>()
                {
                    new MerchItem(new Sku(4), new MerchItemQuantity(1)),
                    new MerchItem(new Sku(1), new MerchItemQuantity(1))
                },
                Status.Processing,
                new RequestDate(DateTime.Now)
            );

            //Act
            orders.AddMerchToOrders(newMerch);

            //Assert
            Assert.Equal(2, orders.Merches.Count);
        }

        [Fact]
        public void CheckWasRequestedTest()
        {
            var orders = new Orders(
                new Employee(new Id(13), new FirstName("John"), new MiddleName("Petrovich"), new LastName("Ivanov"),
                    new Email("frodo@gmail.com")),
                new List<Merch>()
                {
                    new Merch(new Name("Мерч нового сотрудника"), MerchType.WelcomePack,
                        new List<MerchItem>()
                        {
                            new MerchItem(new Sku(2), new MerchItemQuantity(2)),
                            new MerchItem(new Sku(1), new MerchItemQuantity(4))
                        }
                    )
                }
            );
            
            var newMerch = new Merch(new Name("Мерч нового сотрудника"), MerchType.WelcomePack,
                new List<MerchItem>()
                {
                    new MerchItem(new Sku(2), new MerchItemQuantity(2)),
                    new MerchItem(new Sku(1), new MerchItemQuantity(4))
                }
            );
            
            //Assert
            Assert.Throws<MerchAlreadyRequestedException>(() => orders.CheckWasRequested(newMerch));
        }

        [Fact]
        public void CheckWasIssuedTest()
        {
            var orders = new Orders(
                new Employee(new Id(13), new FirstName("John"), new MiddleName("Petrovich"), new LastName("Ivanov"),
                    new Email("frodo@gmail.com")),
                new List<Merch>()
                {
                    new Merch(new Name("Мерч нового сотрудника"), MerchType.WelcomePack,
                        new List<MerchItem>()
                        {
                            new MerchItem(new Sku(2), new MerchItemQuantity(2)),
                            new MerchItem(new Sku(1), new MerchItemQuantity(4))
                        },
                        Status.Issued,
                        new RequestDate(DateTime.Parse("2021-01-01"))
                    )
                }
            );
            
            
            var orders2 = new Orders(
                new Employee(new Id(13), new FirstName("John"), new MiddleName("Petrovich"), new LastName("Ivanov"),
                    new Email("frodo@gmail.com")),
                new List<Merch>()
                {
                    new Merch(new Name("Мерч нового сотрудника"), MerchType.WelcomePack,
                        new List<MerchItem>()
                        {
                            new MerchItem(new Sku(2), new MerchItemQuantity(2)),
                            new MerchItem(new Sku(1), new MerchItemQuantity(4))
                        },
                        Status.Issued,
                        new RequestDate(DateTime.Parse("2020-01-01"))
                    )
                }
            );
            
            var newMerch = new Merch(new Name("Мерч нового сотрудника"), MerchType.WelcomePack,
                new List<MerchItem>()
                {
                    new MerchItem(new Sku(2), new MerchItemQuantity(2)),
                    new MerchItem(new Sku(1), new MerchItemQuantity(4))
                }
            );

            try
            {
                orders.CheckWasIssued(newMerch);
                Assert.True(true);
            }
            catch (Exception e)
            {
                Assert.True(false);
            }

            Assert.Throws<MerchAlreadyIssuedException>(() => orders2.CheckWasIssued(newMerch));


        }
    }
}