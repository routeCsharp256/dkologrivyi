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
	            VALUES (@firstname, @middlename, @lastname, @email);";

            var parameters = new
            {
                firstname = itemToCreate.FirstName.Value,
                middlename = itemToCreate.MiddleName.Value,
                lastname = itemToCreate.LastName.Value,
                email = itemToCreate.Email.Value
            };
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            int id = await connection.QuerySingleAsync<int>(commandDefinition);
            itemToCreate.Id = new Id(id);
            _changeTracker.Track(itemToCreate);
            return itemToCreate;
        }

        public async Task<Employee> UpdateAsync(Employee itemToUpdate, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
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
                    new Id(employee.EmploieeId),
                    new FirstName(employee.Firstname),
                    new MiddleName(employee.Middlename),
                    new LastName(employee.Lastname),
                    new Email(employee.Email)
                );
                return result;
            }

            throw new EmployeeNotFoundInDbExeption();

        }
    }
}