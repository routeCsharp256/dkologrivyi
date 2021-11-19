using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MerchandaiseDomain.AggregationModels.MerchAgregate;
using MerchandaiseInfrastructure.Infrastructure.Interfaces;
using Npgsql;

namespace MerchandaiseInfrastructure.Repositories
{
    public class MerchTypeRepository:IMerchTypeRepository
    {
        public MerchTypeRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory, IChangeTracker changeTracker)
        {
            
        }
        
        public Task<IEnumerable<MerchType>> GetAllTypes(CancellationToken token)
        {
            throw new System.NotImplementedException();
        }
    }
}