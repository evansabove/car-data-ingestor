using CarDataIngestor.Data;
using CarDataIngestor.Response;
using CarDataIngestor.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CarDataIngestor.Functions
{
    public class ListDrivesFunction
    {
        private readonly CarDataContext database;
        private readonly ILogger<ListDrivesFunction> logger;

        public ListDrivesFunction(CarDataContext database, ILogger<ListDrivesFunction> logger)
        {
            this.database = database;
            this.logger = logger;
        }

        [FunctionName("List-Drives")]
        public async Task<IActionResult> RunHttp([HttpTrigger(AuthorizationLevel.Function, "get", Route = "drives")] HttpRequest req)
        {
            var queryParams = req.GetQueryParameterDictionary();

            int page = 1;
            if (queryParams.ContainsKey("page"))
            {
                _ = int.TryParse(queryParams["page"], out page);
            }

            int pageSize = 10;
            if (queryParams.ContainsKey("pageSize"))
            {
                _ = int.TryParse(queryParams["pageSize"], out page);
            }

            var drives = database.Drives
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            var responses = drives
                .ToList()
                .Select(x => new DriveResponse
                {
                    Id = x.Id,
                    GetUri = ApiUriBuilder.BuildFunctionUri($"drives/{x.Id}")
                });

            return new OkObjectResult(responses);
        }
    }
}
