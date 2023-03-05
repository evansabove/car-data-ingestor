using CarDataIngestor.Data;
using CarDataIngestor.Data.Entities;
using Microsoft.Azure.WebJobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace CarDataIngestor
{
    public partial class Ingestor
    {
        private readonly CarDataContext database;

        public Ingestor(CarDataContext database)
        {
            this.database = database;
        }

        [FunctionName("Ingestor")]
        public async Task Run([QueueTrigger("car-data-ingestor", Connection = "QueueConnectionString")]string queueItem, ILogger log)
        {
            var payload = JsonSerializer.Deserialize<SnapshotDto>(queueItem);

            if(payload.DriveId == default)
            {
                log.LogInformation($"Drive ID not supplied.");
                return;
            }

            var drive = await database.Drives.SingleOrDefaultAsync(x => x.Id == payload.DriveId);

            if(drive == default)
            {
                drive = new Drive();
                database.Drives.Add(drive);
            }

            drive.Snapshots.Add(new DriveSnapshot
            {
                CoolantTemp = payload.CoolantTemp,
                EngineLoad = payload.EngineLoad,
                FuelLevel = payload.FuelLevel,
                IntakeTemperature = payload.IntakeTemperature,
                RPM = payload.RPM,
                SequenceNumber = payload.SequenceNumber,
                Speed = payload.Speed,
            });

            await database.SaveChangesAsync();
        }
    }
}
