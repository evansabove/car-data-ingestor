using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Azure.WebJobs;

namespace CarDataIngestor.DatabaseSeeding
{
    public class DbInitializationService : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.AddExtension<DbSeedConfigProvider>();
        }
    }
}
