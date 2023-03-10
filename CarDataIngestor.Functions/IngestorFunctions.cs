using CarDataIngestor.Data;
using CarDataIngestor.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CarDataIngestor
{
    public class IngestorFunctions
    {
        private readonly CarDataContext database;
        private readonly ILogger<IngestorFunctions> logger;

        public IngestorFunctions(CarDataContext database, ILogger<IngestorFunctions> logger)
        {
            this.database = database;
            this.logger = logger;
        }

        [FunctionName("Ingestor-Http")]
        [return: Queue("car-data-ingestor")]
        public async Task<string> RunHttp([HttpTrigger(AuthorizationLevel.Function, "post", Route = "snapshot")] HttpRequest req)
        {
            using StreamReader reader = new(req.Body);
            return await reader.ReadToEndAsync();
        }

        [FunctionName("Ingestor-Queue")]
        public async Task RunQueue([QueueTrigger("car-data-ingestor", Connection = "QueueConnectionString")]string queueItem)
        {
            SnapshotPayload payload = default;
            try
            {
                payload = JsonSerializer.Deserialize<SnapshotPayload>(queueItem);
            } catch(Exception e)
            {
                logger.LogError(e, e.Message);
                return;
            }

            if(payload.DriveId == default)
            {
                logger.LogInformation($"Drive ID not supplied.");
                return;
            }

            var drive = await database.Drives.SingleOrDefaultAsync(x => x.Id == payload.DriveId);

            if(drive == default)
            {
                drive = new Drive();
                database.Drives.Add(drive);
            }

            var snapshots = payload.Snapshots.Select(x =>
            {
                return new DriveSnapshot
                {
                    CoolantTemp = x.CoolantTemp,
                    EngineLoad = x.EngineLoad,
                    FuelLevel = x.FuelLevel,
                    IntakeTemperature = x.IntakeTemperature,
                    RPM = x.RPM,
                    SequenceNumber = x.SequenceNumber,
                    Speed = x.Speed,
                };
            });

            foreach(var snapshot in snapshots)
            {
                drive.Snapshots.Add(snapshot);
            }

            await database.SaveChangesAsync();
        }
    }
}
