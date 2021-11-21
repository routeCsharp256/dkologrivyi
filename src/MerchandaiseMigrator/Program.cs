using System;
using System.IO;
using System.Linq;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

using System;

namespace MerchandaiseMigrator
{
    class Program
    {
        static void Main(string[] args)
        {
             var configuration = new ConfigurationBuilder()
                 //.SetBasePath(@"C:\code\ozon\dkologrivyi\src\merchandise-service\")
                 .SetBasePath(@"C:\code\dkologrivyi\src\merchandise-service\")
                 //.SetBasePath(Directory.GetCurrentDirectory())C:\code\dkologrivyi\src\merchandise-service
                 .AddJsonFile("appsettings.json")
                 .AddEnvironmentVariables()
                 .Build();
            
             var connectionString = configuration.GetSection("DatabaseConnectionOptions:ConnectionString").Get<string>();
             var services = new ServiceCollection()
                 .AddFluentMigratorCore()
                 .ConfigureRunner(
                     rb => rb
                         .AddPostgres()
                         .WithGlobalConnectionString(connectionString)
                         .ScanIn(typeof(Program).Assembly)
                         .For.Migrations())
                 .AddLogging(lb => lb.AddFluentMigratorConsole());
            
             var serviceProvider = services.BuildServiceProvider(false);
            
             using (serviceProvider.CreateScope())
             {
                 var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
                 if (args.Contains("--dryrun"))
                 {
                     runner.ListMigrations();
                 }
                 else
                 {
                     runner.MigrateUp();
                 }
            
                 using var connection = new NpgsqlConnection(connectionString);
                 connection.Open();
                 connection.ReloadTypes();
            }

        }
    }
}