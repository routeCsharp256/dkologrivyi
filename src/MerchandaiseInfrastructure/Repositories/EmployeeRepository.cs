using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MerchandaiseDomain.AggregationModels.Contracts;
using MerchandaiseDomain.AggregationModels.EmployeeAgregate;
using MerchandaiseInfrastructure.Exeptions;
using MerchandaiseInfrastructure.Infrastructure.Interfaces;
using MerchandaiseInfrastructure.Models;
using Npgsql;

namespace MerchandaiseInfrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IChangeTracker _changeTracker;
        private const int Timeout = 5;
        public EmployeeRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory,
            IChangeTracker changeTracker)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _changeTracker = changeTracker;
        }

        public IUnitOfWork UnitOfWork { get; }

        public async Task<Employee> CreateAsync(Employee itemToCreate, CancellationToken cancellationToken = default)
        {
            const string sql = @"INSERT INTO employees(
	            firstname, middlename, lastname, email)
	            VALUES (@FirstName, @MiddleName, @LastName, @Email)
	            RETURNING employeeid;";

            var parameters = new
            {
                FirstName = itemToCreate.FirstName.Value,
                MiddleName = itemToCreate.MiddleName.Value,
                LastName = itemToCreate.LastName.Value,
                Email = itemToCreate.Email.Value
            };
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            long id = await connection.ExecuteScalarAsync<long>(commandDefinition);
            itemToCreate.Id =  new Id(id);
            _changeTracker.Track(itemToCreate);
            return itemToCreate;
        }

        public async Task<Employee> UpdateAsync(Employee itemToUpdate, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                UPDATE public.employees
	                SET firstname=@FirstName, middlename=@MiddleName, lastname=@LastName, email=@Email
	                WHERE employeeid=@EmployeeId;";
            
            var parameters = new
            {
                FirstName=itemToUpdate.FirstName.Value,
                MiddleName=itemToUpdate.MiddleName.Value,
                LastName=itemToUpdate.LastName.Value,
                Email = itemToUpdate.Email.Value,
                EmployeeId=itemToUpdate.Id.Value
            };
            
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            await connection.ExecuteAsync(commandDefinition);
            _changeTracker.Track(itemToUpdate);
            return itemToUpdate;
        }

        public async Task<Employee> FindEmployeeByEmail(string email, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                SELECT employeeid, firstname, middlename, lastname, email
	                FROM employees WHERE email=@email;";
            
            var parameters = new
            {
                email = email
            };
            
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);

            var employees = await connection.QueryAsync<EmployeeDb>(commandDefinition);
            var employee = employees.FirstOrDefault();
            
            if (employee is not null)
            {
                var result = new Employee(
                    new Id(employee.EmployeeId),
                    new FirstName(employee.Firstname),
                    new MiddleName(employee.Middlename),
                    new LastName(employee.Lastname),
                    new Email(employee.Email)
                );
                _changeTracker.Track(result);
                return result;
            }

            return null;

        }
    }
}