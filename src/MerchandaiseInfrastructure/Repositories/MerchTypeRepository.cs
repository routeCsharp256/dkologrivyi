using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MerchandaiseDomain.AggregationModels.MerchAgregate;
using MerchandaiseInfrastructure.Infrastructure.Interfaces;
using Npgsql;
using Dapper;
using MerchandaiseInfrastructure.Models;

namespace MerchandaiseInfrastructure.Repositories
{
    public class MerchTypeRepository : IMerchTypeRepository
    {
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IChangeTracker _changeTracker;
        private const int Timeout = 5;


        public MerchTypeRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory,
            IChangeTracker changeTracker)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _changeTracker = changeTracker;
        }

        public async Task<IEnumerable<MerchType>> GetAllTypes(CancellationToken token)
        {
            const string sql = @"
            SELECT id, name
	            FROM merchtypes;
            ";

            var commandDefinition = new CommandDefinition(
                sql,
                commandTimeout: Timeout,
                cancellationToken: token);
            var connection = await _dbConnectionFactory.CreateConnection(token);
            var merchTypes = await connection.QueryAsync<MerchTypeDb>(commandDefinition);
            var result = merchTypes.Select(x => new MerchType(x.Id, x.Name));
            return result;
        }
    }
}