using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MerchandaiseDomain.AggregationModels.Contracts;
using MerchandaiseDomain.AggregationModels.EmployeeAgregate;
using MerchandaiseDomain.AggregationModels.MerchAgregate;
using MerchandaiseDomain.AggregationModels.OrdersAgregate;
using MerchandaiseDomain.Models;
using MerchandaiseDomain.Services.Interfaces;
using MerchandaiseGrpc.StockApi;
using MerchandaiseGrpcClient;
using MerchType = MerchandaiseDomain.AggregationModels.MerchAgregate.MerchType;


namespace MerchandaiseDomain.Services
{
    public class MerchService : IMerchService
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMerchRepository _merchRepository;
        private readonly IStockApi _stockApi;
        private readonly IEmployeeRepository _employeeRepository;

        public MerchService(IOrdersRepository ordersRepository, IUnitOfWork unitOfWork,
            IMerchRepository merchRepository, IStockApi stockApi, IEmployeeRepository employeeRepository)
        {
            _ordersRepository = ordersRepository;
            _unitOfWork = unitOfWork;
            _merchRepository = merchRepository;
            _stockApi = stockApi;
            _employeeRepository = employeeRepository;
        }

        public async Task RequestMerch(long employeeId, MerchType merchType)
        {
            var orders = await _ordersRepository.FindByEmloyeeIdAsync(employeeId);
            var merch = await _merchRepository.FindByMerchType(merchType.Id);
            orders.CheckWasRequested(merch);
            orders.AddMerchToOrders(merch);

            var items = new List<Item>();
            foreach (var item in merch.MerchItems)
            {
                items.Add(new Item() {SkuId = item.Sku.Value, Quantity = item.Quantity.Value});
            }

            if (await _stockApi.CheckIsAvailableAsync(items))
            {
                if (await _stockApi.TryDeliverSkuAsync(orders.Employee.Email.Value, items))
                {
                    merch.ChangeStatus(Status.Issued);
                }
                else merch.ChangeStatus(Status.Waiting);
            }
            else merch.ChangeStatus(Status.Waiting);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task CheckWasIssued(long employeeId, MerchType merchType)
        {
            var orders = await _ordersRepository.FindByEmloyeeIdAsync(employeeId);
            var merch = await _merchRepository.FindByMerchType(merchType.Id);
            orders.CheckWasIssued(merch);
        }

        public async Task NewSupply(SupplyShippedEvent supplyShippedEvent)
        {
            List<MerchItem> merchItems = new List<MerchItem>();
            foreach (var item in supplyShippedEvent.Items)
            {
                merchItems.Add(new MerchItem(new Sku(item.SkuId), new MerchItemQuantity(item.Quantity)));
            }

            //получаем набор всех заказов которые не были отправлены
            List<Orders> employeesOrders = await _ordersRepository.GetUnIssuedOrders();

            //по заказам каждого сотрудника смотрим какой из них можно переотправить
            foreach (var employeeOrders in employeesOrders)
            {
                foreach (var merch in employeeOrders.Merches)
                {
                    if (merch.CanBeShipped(merchItems))
                    {
                        var items = new List<Item>();
                        foreach (var item in merch.MerchItems)
                        {
                            items.Add(new Item() {SkuId = item.Sku.Value, Quantity = item.Quantity.Value});
                        }

                        if (await _stockApi.CheckIsAvailableAsync(items))
                        {
                            if (await _stockApi.TryDeliverSkuAsync(employeeOrders.Employee.Email.Value, items))
                            {
                                merch.ChangeStatus(Status.Issued);
                            }
                            else merch.ChangeStatus(Status.Waiting);
                        }
                        else merch.ChangeStatus(Status.Waiting);
                    }
                }
            }
        }

        public async Task NewNotification(NotificationEvent notificationEvent)
        {
            if (notificationEvent.Payload is MerchDeliveryEventPayload)
            {
                var merchType = ((MerchDeliveryEventPayload) notificationEvent.Payload).MerchType;
                var empl = await _employeeRepository.FindEmployeeByEmail(notificationEvent.EmployeeEmail);

                await RequestMerch(empl.Id.Value, merchType);
            }
        }
    }
}