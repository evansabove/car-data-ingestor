using System;
using System.Collections.Generic;

namespace CarDataIngestor.Data.Entities
{
    public class Drive : KeyedEntity
    {
        public ICollection<DriveSnapshot> Snapshots { get; set; } = new List<DriveSnapshot>();
        public DateTime IngestedDate { get; set; }
    }
}
