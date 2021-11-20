using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MerchandaiseDomain.AggregationModels.Contracts;
using MerchandaiseDomain.AggregationModels.EmployeeAgregate;
using MerchandaiseDomain.AggregationModels.MerchAgregate;
using MerchandaiseDomain.AggregationModels.OrdersAgregate;
using MerchandaiseDomain.Models;
using MerchandaiseDomainServices.Interfaces;
using MerchType = MerchandaiseDomain.AggregationModels.MerchAgregate.MerchType;


namespace MerchandaiseDomainServices
{
    public class MerchService : IMerchService
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMerchRepository _merchRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IStockGateway _stockGateway;

        public MerchService(IOrdersRepository ordersRepository, IUnitOfWork unitOfWork,
            IMerchRepository merchRepository, IEmployeeRepository employeeRepository, IStockGateway stockGateway)
        {
            _ordersRepository = ordersRepository;
            _unitOfWork = unitOfWork;
            _merchRepository = merchRepository;
            _employeeRepository = employeeRepository;
            _stockGateway = stockGateway;
        }

        public async Task RequestMerch(string employeeEmail, MerchType merchType, CancellationToken token)
        {
            await _unitOfWork.StartTransaction(token);
            var orders = await _ordersRepository.FindByEmloyeeEmailAsync(employeeEmail);
            var merch = await _merchRepository.FindByMerchType(merchType.Id);
            orders.CheckWasRequested(merch);
            orders.AddMerchToOrders(merch);

            if (await _stockGateway.CheckIsAvailableAsync(merch.MerchItems))
            {
                if (await _stockGateway.TryDeliverSkuAsync(orders.Employee.Email.Value, merch.MerchItems))
                {
                    merch.ChangeStatus(Status.Issued);
                }
                else merch.ChangeStatus(Status.Waiting);
            }
            else merch.ChangeStatus(Status.Waiting);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task RequestMerch(long employeeId, MerchType merchType, CancellationToken token)
        {
            var orders = await _ordersRepository.FindByEmloyeeIdAsync(employeeId);
            var merch = await _merchRepository.FindByMerchType(merchType.Id);
            orders.CheckWasRequested(merch);
            orders.AddMerchToOrders(merch);

            if (await _stockGateway.CheckIsAvailableAsync(merch.MerchItems))
            {
                if (await _stockGateway.TryDeliverSkuAsync(orders.Employee.Email.Value, merch.MerchItems))
                {
                    merch.ChangeStatus(Status.Issued);
                }
                else merch.ChangeStatus(Status.Waiting);
            }
            else merch.ChangeStatus(Status.Waiting);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task CheckWasIssued(long employeeId, MerchType merchType, CancellationToken token)
        {
            var orders = await _ordersRepository.FindByEmloyeeIdAsync(employeeId);
            var merch = await _merchRepository.FindByMerchType(merchType.Id);
            orders.CheckWasIssued(merch);
        }

        public async Task NewSupply(SupplyShippedEvent supplyShippedEvent, CancellationToken token)
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
                        if (await _stockGateway.CheckIsAvailableAsync(merch.MerchItems))
                        {
                            if (await _stockGateway.TryDeliverSkuAsync(employeeOrders.Employee.Email.Value, merch.MerchItems))
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

        public async Task NewNotification(NotificationEvent notificationEvent, CancellationToken token)
        {
            if (notificationEvent.Payload is MerchDeliveryEventPayload)
            {
                var merchType = ((MerchDeliveryEventPayload) notificationEvent.Payload).MerchType;
                var empl = await _employeeRepository.FindEmployeeByEmail(notificationEvent.EmployeeEmail);

                await RequestMerch(empl.Id.Value, merchType, token);
            }
        }
    }
}