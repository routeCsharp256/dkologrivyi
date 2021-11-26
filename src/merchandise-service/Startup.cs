using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MerchandaiseDomain.AggregationModels.Contracts;
using MerchandaiseDomain.AggregationModels.EmployeeAgregate;
using MerchandaiseDomain.AggregationModels.MerchAgregate;
using MerchandaiseDomain.AggregationModels.OrdersAgregate;
using MerchandaiseDomainServices;
using MerchandaiseDomainServices.Interfaces;
using MerchandaiseGrpc.StockApi;
using MerchandaiseGrpcClient;
using MerchandaiseInfrastructure;
using MerchandaiseInfrastructure.Configuration;
using MerchandaiseInfrastructure.Infrastructure;
using MerchandaiseInfrastructure.Infrastructure.Interfaces;
using MerchandaiseInfrastructure.Repositories;
using MerchandiseService.GrpcServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using MediatR;

namespace MerchandiseService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IMerchService, MerchService>();
            services.AddScoped<IStockClient, StockClient>();
            services.AddScoped<IStockGateway, StockGateway>();
           
            AddRepositories(services);
            AddMediator(services);
            AddDatabaseComponents(services);
            
            services.AddGrpcClient<StockApiGrpc.StockApiGrpcClient>(o =>
            {
                o.Address = new Uri(Configuration.GetSection("StockApiUrl").Value);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<MerchandiseGrpcService>();
                endpoints.MapControllers();
            });
        }
        private static void AddRepositories(IServiceCollection services)
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            services.AddScoped<IOrdersRepository, OrdersRepository>();
            services.AddScoped<IMerchRepository, MerchRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IMerchTypeRepository, MerchTypeRepository>();
        }
        
        private void AddDatabaseComponents(IServiceCollection services)
        {
            services.Configure<DatabaseConnectionOptions>(Configuration.GetSection(nameof(DatabaseConnectionOptions)));
            services.AddScoped<IDbConnectionFactory<NpgsqlConnection>, NpgsqlConnectionFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IChangeTracker, ChangeTracker>();
            
            
        }
        
        private static void AddMediator(IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup), typeof(DatabaseConnectionOptions));
        }
    }
}