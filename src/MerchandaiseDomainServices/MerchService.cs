using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MerchandaiseDomain.AggregationModels.Contracts;
using MerchandaiseDomain.AggregationModels.EmployeeAgregate;
using MerchandaiseDomain.AggregationModels.MerchAgregate;
using MerchandaiseDomain.AggregationModels.OrdersAgregate;
using MerchandaiseDomain.Exceptions.EmployeeValidation;
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

        public async Task<Orders> RequestMerch(string employeeEmail, int merchId, CancellationToken token)
        {
            Orders orders;
            await _unitOfWork.StartTransaction(token);
            try
            {
                orders = await _ordersRepository.FindByEmloyeeEmailAsync(employeeEmail);
            }
            catch (Exception e)
            {
                var employee = new Employee(new Id(null), new FirstName(null), new MiddleName(null), new LastName(null),
                    new Email(employeeEmail));
                await _employeeRepository.CreateAsync(employee, token);
                orders = new Orders(
                    employee,
                    new List<Merch>()
                );
            }
            
            var merch = await _merchRepository.GetAvailableMerchByType(merchId, token);
            
            await _merchRepository.CreateAsync(merch, token);
            orders.CheckWasRequested(merch);
            orders.AddMerchToOrders(merch);
            await _ordersRepository.CreateAsync(orders);
            
            // if (await _stockGateway.CheckIsAvailableAsync(merch.MerchItems))
            // {
            //     if (await _stockGateway.TryDeliverSkuAsync(orders.Employee.Email.Value, merch.MerchItems))
            //     {
            //         merch.ChangeStatus(Status.Issued);
            //     }
            //     else merch.ChangeStatus(Status.Waiting);
            // }
            // else merch.ChangeStatus(Status.Waiting);

            await _merchRepository.UpdateAsync(merch);
            await _unitOfWork.SaveChangesAsync();
            return orders;
        }



        public async Task CheckWasIssued(long employeeId, MerchType merchType, CancellationToken token)
        {
            var orders = await _ordersRepository.FindByEmloyeeIdAsync(employeeId);
            //var merch = await _merchRepository.FindByMerchType(merchType.Id);
            //orders.CheckWasIssued(merch);
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

                await RequestMerch(empl.Email.Value, merchType.Id, token);
            }
        }

        public async Task<List<Merch>> GetAvailableMerchList(CancellationToken token)
        {
            return await _merchRepository.GetAvailableMerchList(token);
        }
        
    }
}