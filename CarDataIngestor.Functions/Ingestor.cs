using CarDataIngestor.Data;
using CarDataIngestor.Data.Entities;
using Microsoft.Azure.WebJobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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
            //var test = new SnapshotPayload
            //{
            //    DriveId = Guid.NewGuid(),
            //    Snapshots = new List<SnapshotDto>
            //    {
            //        new SnapshotDto
            //        {
            //            CoolantTemp = 52,
            //            EngineLoad = 48,
            //            FuelLevel = 99,
            //            IntakeTemperature = 12,
            //            RPM = 1834,
            //            SequenceNumber = 1,
            //            Speed = 48

            //        }
            //    }
            //};

            //var testString = JsonSerializer.Serialize(test);

            var payload = JsonSerializer.Deserialize<SnapshotPayload>(queueItem);

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
