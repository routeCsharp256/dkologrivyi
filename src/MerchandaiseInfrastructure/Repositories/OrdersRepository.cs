using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MerchandaiseDomain.AggregationModels.Contracts;
using MerchandaiseDomain.AggregationModels.EmployeeAgregate;
using MerchandaiseDomain.AggregationModels.OrdersAgregate;
using MerchandaiseInfrastructure.Exeptions;
using MerchandaiseInfrastructure.Infrastructure.Interfaces;
using Npgsql;
using System.Linq;
using System.Xml.Schema;
using MerchandaiseDomain.AggregationModels.MerchAgregate;
using MerchandaiseInfrastructure.Models;

namespace MerchandaiseInfrastructure.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IChangeTracker _changeTracker;
        private readonly IEmployeeRepository _employeeRepository;
        private const int Timeout = 5;

        public OrdersRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory,
            IChangeTracker changeTracker, IEmployeeRepository employeeRepository)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _changeTracker = changeTracker;
            _employeeRepository = employeeRepository;
        }

        public async Task<Orders> FindByEmloyeeEmailAsync(string employeeEmail, CancellationToken token)
        {
            Employee employee = await _employeeRepository.FindEmployeeByEmail(employeeEmail, token);
            if (employee is null) return null; // если сотрудника в бд нет, то и заказов его нет. Сотрудник новый
            //по сотруднику находим все его заказы, делаем join по всем необходимым объектам
            const string sql =
                @"SELECT orders.orderid, orders.employeeid, orders.orderedmerchid, orderedmerches.merchid, orderedmerches.name, 
                orderedmerches.merchtypeid, orderedmerches.statusid, orderedmerches.requestdate,
                orderedmerchitems.id, orderedmerchitems.skuid, orderedmerchitems.quantity, orderedmerchitems.orderedmerchid,
                employees.employeeid, employees.firstname, employees.middlename, employees.lastname, employees.email,
                merchtypes.id, merchtypes.name
                FROM orders
                INNER JOIN orderedmerches ON orders.orderedmerchid = orderedmerches.merchid
                INNER JOIN orderedmerchitems ON orderedmerches.merchid = orderedmerchitems.orderedmerchid
                INNER JOIN employees ON orders.employeeid = employees.employeeid
                INNER JOIN merchtypes ON orderedmerches.merchtypeid = merchtypes.id
                WHERE employees.employeeid=@employeeId;";

            var parameters = new
            {
                employeeId = employee.Id.Value
            };

            var connection = await _dbConnectionFactory.CreateConnection(token);
            var result = await connection
                .QueryAsync<OrdersDb, OrderedMerchesDb, OrderedMerchItemDb, EmployeeDb, MerchTypeDb,
                    FindOrdersByEmloyeeResponse>(
                    sql,
                    ((ordersDb, orderedMerchesDb, orderedMerchItemDb, employeeDb, merchTypeDb) =>
                        {
                            return new FindOrdersByEmloyeeResponse(ordersDb, orderedMerchesDb, orderedMerchItemDb,
                                employeeDb,
                                merchTypeDb);
                        }
                    ),
                    splitOn: "merchid,id,employeeid,id",
                    param: parameters
                );

            //заказанные мерчи в формате БД
            var orderedMerchesDb =
                result.Select(e => e.OrderedMerchesDb).GroupBy(x => x.MerchId).Select(x => x.First());

            //Items относящиеся к заказам сотрудника 
            var orderedMerchItemDbGroups = result.Select(e => e.OrderedMerchItemDb).GroupBy(x => x.OrderedMerchId);

            // собираем коллекцию в которой в ключе будет храниться id мерча, а в значении коллекция items, относящихся к этому мерчу
            Dictionary<long, List<MerchItem>> merchItemsDict = new Dictionary<long, List<MerchItem>>();
            foreach (var orderedMerchItemDbGroup in orderedMerchItemDbGroups)
            {
                List<MerchItem> merchItemList = new List<MerchItem>();
                long orderedMerchId = 0;
                foreach (var item in orderedMerchItemDbGroup)
                {
                    merchItemList.Add(new MerchItem(
                        new Sku(item.SkuId),
                        new MerchItemQuantity(item.Quantity)
                    ));
                    if (orderedMerchId == 0)
                        orderedMerchId = item.OrderedMerchId;
                }

                if (orderedMerchId == 0) throw new Exception("Ошибка чтения БД. merchItem не относится к Merch");
                merchItemsDict.Add(orderedMerchId, merchItemList);
            }

            var merchTypes = result.Select(e => e.MerchTypeDb).GroupBy(x => x.Id)
                .Select(x => x.First()).Select(x => new MerchType(x.Id, x.Name));

            List<Merch> merches = orderedMerchesDb.Select(x =>
                new Merch(
                    new MerchId(x.MerchId),
                    new Name(x.Name),
                    merchTypes.Where(mt => mt.Id == x.MerchTypeId).FirstOrDefault(),
                    merchItemsDict.GetValueOrDefault(x.MerchId),
                    Status.FromId(x.StatusId),
                    new RequestDate(x.RequestDate)
                )
            ).ToList();

            Orders orders = new Orders(
                employee,
                merches
            );

            return orders;
        }

        public async Task<List<Orders>> GetUnIssuedOrders(CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        public async Task CreateAsync(long employeeId, long orderedMerchId, CancellationToken cancellationToken)
        {
            const string sql = @"INSERT INTO public.orders(
	            employeeid, orderedmerchid)
	            VALUES (@EmployeeId, @OrderedmerchId);
	            ";

            var parameters = new
            {
                EmployeeId = employeeId,
                OrderedmerchId = orderedMerchId,
            };
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            await connection.ExecuteScalarAsync<long>(commandDefinition);
        }
    }
}