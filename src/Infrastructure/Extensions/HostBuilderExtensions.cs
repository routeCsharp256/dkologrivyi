using System.Reflection;
using Infrastructure.Filters;
using Infrastructure.Interceptors;
using Infrastructure.Middlewares;
using Infrastructure.StartupFilters;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Infrastructure.Extensions
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder AddInfrastructure(this IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {

                services.AddSingleton<IStartupFilter, RequestStartUpFilter>();
                services.AddSingleton<IStartupFilter, ResponceStartupFilter>();
                services.AddSingleton<IStartupFilter, VersionStartupFilter>();
                services.AddSingleton<IStartupFilter, LiveStartupFilter>();
                services.AddSingleton<IStartupFilter, ReadyStartupFilter>();

                services.AddSingleton<IStartupFilter, SwaggerStartupFilter>();
                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo {Title = "Api", Version = "v1"});
                });

                services.AddGrpc(options => options.Interceptors.Add<LoggingInterceptor>());


                services.AddControllers(options => options.Filters.Add<GlobalExceptionFilter>());
            });
            
            return builder;
        }
    }
}