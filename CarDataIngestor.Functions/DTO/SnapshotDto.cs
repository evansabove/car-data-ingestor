using System;
using System.Text.Json.Serialization;

namespace CarDataIngestor
{
    public partial class Ingestor
    {
        public class SnapshotDto
        {
            [JsonPropertyName("DRIVE_ID")]
            public Guid DriveId { get; set; }

            [JsonPropertyName("SEQUENCE_NUMBER")]
            public int SequenceNumber { get; set; }

            [JsonPropertyName("COOLANT_TEMP")]
            public double CoolantTemp { get; set; }

            [JsonPropertyName("ENGINE_LOAD")]
            public double EngineLoad { get; set; }

            [JsonPropertyName("RPM")]
            public double RPM { get; set; }

            [JsonPropertyName("SPEED")]
            public double Speed { get; set; }

            [JsonPropertyName("INTAKE_TEMP")]
            public double IntakeTemperature { get; set; }

            [JsonPropertyName("FUEL_LEVEL")]
            public double FuelLevel { get; set; }
        }
    }
}
