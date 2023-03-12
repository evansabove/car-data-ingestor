using CarDataIngestor.Data;
using CarDataIngestor.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CarDataIngestor.Functions
{
    public class ListDriveSnapshotsFunction
    {
        private readonly CarDataContext database;
        private readonly ILogger<ListDriveSnapshotsFunction> logger;

        public ListDriveSnapshotsFunction(CarDataContext database, ILogger<ListDriveSnapshotsFunction> logger)
        {
            this.database = database;
            this.logger = logger;
        }

        [FunctionName("List-Drive-Snapshots")]
        public async Task<IActionResult> RunHttp([HttpTrigger(AuthorizationLevel.Function, "get", Route = "drives/{id}")] HttpRequest req, string id)
        {
            if (!Guid.TryParse(id, out var driveId))
            {
                return new BadRequestObjectResult("Drive ID must be a GUID");
            }

            var drive = database.Drives.SingleOrDefault(x => x.Id == driveId);

            if (drive == default)
            {
                return new NotFoundObjectResult($"Drive with ID {driveId} could not be found");
            }

            var queryParams = req.GetQueryParameterDictionary();

            int page = 1;
            if (queryParams.ContainsKey("page"))
            {
                _ = int.TryParse(queryParams["page"], out page);
            }

            int pageSize = 100;
            if (queryParams.ContainsKey("pageSize"))
            {
                _ = int.TryParse(queryParams["pageSize"], out page);
            }

            var snapshots = database.DriveSnapshots
                .Where(x => x.DriveId == driveId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new DriveSnapshotResponse
                {
                    CoolantTemp = x.CoolantTemp,
                    EngineLoad = x.EngineLoad,
                    FuelLevel = x.FuelLevel,
                    IntakeTemperature = x.IntakeTemperature,
                    RPM = x.RPM,
                    Id = x.Id,
                    SequenceNumber = x.SequenceNumber,
                    Speed = x.Speed
                })
                .ToList();

            return new OkObjectResult(snapshots);
        }
    }
}
