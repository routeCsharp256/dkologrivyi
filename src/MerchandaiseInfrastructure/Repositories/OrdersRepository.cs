using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MerchandaiseDomain.AggregationModels.Contracts;
using MerchandaiseDomain.AggregationModels.EmployeeAgregate;
using MerchandaiseDomain.AggregationModels.OrdersAgregate;
using MerchandaiseInfrastructure.Exeptions;
using MerchandaiseInfrastructure.Infrastructure.Interfaces;
using Npgsql;

namespace MerchandaiseInfrastructure.Repositories
{
    public class OrdersRepository:IOrdersRepository
    {
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IChangeTracker _changeTracker;
        private readonly IEmployeeRepository _employeeRepository;
        private const int Timeout = 5;
        
        public OrdersRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory, IChangeTracker changeTracker, IEmployeeRepository employeeRepository)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _changeTracker = changeTracker;
            _employeeRepository = employeeRepository;
        }
        public async Task<Orders> CreateAsync(Orders itemToCreate, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Orders> UpdateAsync(Orders itemToUpdate, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Orders> FindByEmloyeeIdAsync(long id, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Orders> FindByEmloyeeEmailAsync(string employeeEmail, CancellationToken token)
        {
            Employee employee= await _employeeRepository.FindEmployeeByEmail(employeeEmail, token);
            if (employee is null)
                throw new EmployeeNotFoundInDbExeption();
            
            const string sql = @"
                SELECT orderid, employeeid, orderedmerchid
	                FROM orders WHERE employeeid=@employeeid;";
            var parameters = new
            {
                employeeid = employee.Id
            };
            
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: token);
            var connection = await _dbConnectionFactory.CreateConnection(token);
            throw new EmployeeNotFoundInDbExeption();
            
        }

        public async Task<List<Orders>> GetUnIssuedOrders(CancellationToken token)
        {
            throw new System.NotImplementedException();
        }
    }
}