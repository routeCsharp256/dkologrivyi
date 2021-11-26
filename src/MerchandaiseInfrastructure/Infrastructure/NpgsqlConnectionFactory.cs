using System.Data;
using System.Threading;
using System.Threading.Tasks;
using MerchandaiseInfrastructure.Configuration;
using MerchandaiseInfrastructure.Infrastructure.Interfaces;
using Microsoft.Extensions.Options;
using Npgsql;

namespace MerchandaiseInfrastructure.Infrastructure
{
    public class NpgsqlConnectionFactory : IDbConnectionFactory<NpgsqlConnection>
    {
        private readonly DatabaseConnectionOptions _options;
        private NpgsqlConnection _conn;

        public NpgsqlConnectionFactory(IOptions<DatabaseConnectionOptions> options)
        {
            _options = options.Value;
        }

        public async Task<NpgsqlConnection> CreateConnection(CancellationToken token)
        {
            if (_conn is not null)
            {
                return _conn;
            }

            _conn = new NpgsqlConnection(_options.ConnectionString);
            await _conn.OpenAsync(token);
            _conn.StateChange += ((o, e) =>
            {
                if (e.CurrentState == ConnectionState.Closed)
                {
                    _conn = null;
                }
            });
            return _conn;
        }

        public void Dispose()
        {
            _conn.Dispose();
        }
    }
}