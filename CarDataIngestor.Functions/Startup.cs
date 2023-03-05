using CarDataIngestor;
using CarDataIngestor.Data;
using CarDataIngestor.DatabaseSeeding;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(Startup))]
[assembly: WebJobsStartup(typeof(DbInitializationService), "DbSeeder")]
namespace CarDataIngestor
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var connectionString = Environment.GetEnvironmentVariable("DatabaseConnectionString", EnvironmentVariableTarget.Process);
            builder.Services.AddDbContext<CarDataContext>(options => options.UseSqlServer(connectionString));
        }
    }

}
