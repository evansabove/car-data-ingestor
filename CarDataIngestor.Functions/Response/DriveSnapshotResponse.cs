using System;

namespace CarDataIngestor.Response
{
    public class DriveSnapshotResponse
    {
        public Guid Id { get; set; }

        public int SequenceNumber { get; set; }
        public double CoolantTemp { get; set; }
        public double EngineLoad { get; set; }
        public double RPM { get; set; }
        public double Speed { get; set; }
        public double IntakeTemperature { get; set; }
        public double FuelLevel { get; set; }
    }
}
