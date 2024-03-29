﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CarDataIngestor
{
    public class SnapshotPayload
    {
        [JsonPropertyName("DRIVE_ID")]
        public Guid DriveId { get; set; }

        [JsonPropertyName("SNAPSHOTS")]
        public IEnumerable<SnapshotDto> Snapshots { get; set; }
    }

    public class SnapshotDto
    {
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
