using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MerchandaiseDomain.AggregationModels.Contracts;
using MerchandaiseDomain.AggregationModels.MerchAgregate;
using MerchandaiseInfrastructure.Infrastructure.Interfaces;
using MerchandaiseInfrastructure.Models;
using Npgsql;

namespace MerchandaiseInfrastructure.Repositories
{
    public class MerchRepository : IMerchRepository
    {
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IChangeTracker _changeTracker;
        private readonly IMerchTypeRepository _merchTypeRepository;
        private const int Timeout = 5;

        public MerchRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory, IChangeTracker changeTracker,
            IMerchTypeRepository merchTypeRepository)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _changeTracker = changeTracker;
            _merchTypeRepository = merchTypeRepository;
        }

        public IUnitOfWork UnitOfWork { get; }

        public async Task<Merch> CreateAsync(Merch itemToCreate, CancellationToken cancellationToken)
        {
            itemToCreate = await InsertMerch(itemToCreate, cancellationToken);
            itemToCreate = await InsertMerchItems(itemToCreate, cancellationToken);
            return itemToCreate;
        }

        private async Task<Merch> InsertMerch(Merch itemToCreate, CancellationToken cancellationToken)
        {
            const string sql = @"
                INSERT INTO public.orderedmerches(
	            name, merchtypeid, statusid, requestdate) 
	            VALUES (@Name, @MerchTypeId, @StatusId, @RequestDate)
	            RETURNING merchid;	            
	            ";

            var parameters = new
            {
                //Merchid = itemToCreate.Id,
                Name = itemToCreate.Name.Value,
                MerchTypeId = itemToCreate.Type.Id,
                StatusId = itemToCreate.Status.Id,
                RequestDate = itemToCreate.RequestDate.Value
            };
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            long id = await connection.ExecuteScalarAsync<long>(commandDefinition);
            itemToCreate.MerchId = new MerchId(id);
            _changeTracker.Track(itemToCreate);
            return itemToCreate;
        }

        private async Task<Merch> InsertMerchItems(Merch itemToCreate, CancellationToken cancellationToken)
        {
            const string sql = @"
	            INSERT INTO public.orderedmerchitems(
	            skuid, quantity, orderedmerchid)
	            VALUES (@SkuId, @Quantity, @OrderedMerchId);	
	            ";
            var merchItemDbList = itemToCreate.MerchItems.Select(
                x => new MerchItemDb(
                    id: null,
                    skuId: x.Sku.Value,
                    quantity: x.Quantity.Value,
                    orderedMerchId: itemToCreate.MerchId.Value
                )
            );
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: merchItemDbList,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            await connection.ExecuteAsync(commandDefinition);
            
            _changeTracker.Track(itemToCreate);
            return itemToCreate;
        }


        public async Task<Merch> UpdateAsync(Merch itemToUpdate, CancellationToken cancellationToken)
        {
            const string sql = @"
                UPDATE orderedmerches
	                SET name=@Name, merchtypeid=@MerchTypeId, 
	                    statusid=@StatusId, requestdate=@RequestDate
	                WHERE merchid=@MerchId;
	                ";

            var parameters = new
            {
                Name=itemToUpdate.Name.Value,
                MerchTypeId=itemToUpdate.Type.Id,
                StatusId=itemToUpdate.Status.Id,
                RequestDate=itemToUpdate.RequestDate.Value,
                MerchId=itemToUpdate.MerchId.Value
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

        public async Task<Merch> GetAvailableMerchByType(int merchTypeId, CancellationToken cancellationToken)
        {
            var merches = await GetAvailableMerchList(cancellationToken);
            var result = (merches.Where(x => x.Type.Id == merchTypeId)).FirstOrDefault();
            return result;
        }

        public async Task<List<Merch>> GetAvailableMerchList(CancellationToken cancellationToken)
        {
            var availableMerchDbs = await GetAvailableMerchDbs(cancellationToken);
            var availableMerchItemDbs = await GetAvailableMerchItemDbs(cancellationToken);
            var merchType = await _merchTypeRepository.GetAllTypes(cancellationToken);

            var result = availableMerchDbs.Select(
                merchDb =>
                    new Merch(
                        new Name(merchDb.Name),
                        (merchType.Where(x => x.Id == merchDb.MerchTypeId)).First(),
                        availableMerchItemDbs.Where(merchItemDb => merchItemDb.AvailableMerchId == merchDb.MerchId)
                            .Select(
                                merchItemDb =>
                                    new MerchItem(
                                        new Sku(merchItemDb.SkuId),
                                        new MerchItemQuantity(merchItemDb.Quantity)
                                    )
                            ).ToList()
                    )
            );
            return result.ToList();
        }

        private async Task<IEnumerable<AvailableMerchDb>> GetAvailableMerchDbs(CancellationToken cancellationToken)
        {
            const string sql = @"
            SELECT merchid, name, merchtypeid
	            FROM public.availablemerches;

            ";

            var commandDefinition = new CommandDefinition(
                sql,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var availableMerches = await connection.QueryAsync<AvailableMerchDb>(commandDefinition);
            return availableMerches;
        }

        private async Task<IEnumerable<AvailableMerchItemDb>> GetAvailableMerchItemDbs(
            CancellationToken cancellationToken)
        {
            const string sql = @"
            SELECT id, skuid, quantity, availablemerchid
	        FROM public.availablemerchitems;
            ";

            var commandDefinition = new CommandDefinition(
                sql,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var availableMercheItems = await connection.QueryAsync<AvailableMerchItemDb>(commandDefinition);
            return availableMercheItems;
        }
    }
}